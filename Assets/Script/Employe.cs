﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation;
public class Employe : MonoBehaviour {

    public employeeID employeProfile;
	public GameObject boxDeTravail;// Box de l’employé
    public static bool suicideLock = false;
	/**
	 * true = au taff
	 * false = en train de glander (engueulable)
	 * si auTravail == true (true vers la photocop, vers le box) || false (true vers la salle de repos, vers les chiottes)
	 * si auTravail == true (false dans le box, à la photcop) || false (false à la salle de repos, au chiottes)
	**/
	bool enDeplacement;// utile pour l’animation notamment
	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;

	public GameObject floor;
    public static GameObject boss;

    public static List<GameObject> suicide;
	//public static List<GameObject> chill;
    public static List<GameObject> emptyChill;

	//public static List<GameObject> workingHelp;
    public static List<GameObject> emptyWorkingHelp;

    public RAIN.Memory.BasicMemory tMemory;
    public RAIN.Perception.BasicSenses tSens;

	public EmployeeData data;
	//Awake is always called before any Start functions

    public bool isAlreadyInRange, moveMemory, workingMemory,suicideMemory;

    //public AudioSource[] coffeeSound, facebookSound, workingSound;

    public AudioSource coffeeSound,keyboardSound,photocopierSound,depressionSound,gamingSound,facebookSound,vendingMachineSound;

    public AudioSource[] engueulageSound;

    public float seuilDepression = 0.8f;
    public bool depressif = false;

	void Awake()
	{
		AIRig aiRig = GetComponentInChildren<AIRig>();
        tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tSens = aiRig.AI.Senses as RAIN.Perception.BasicSenses;          
	}
    // Use this for initialization
    void Start()
    {
        isAlreadyInRange = false;
        suicideMemory = tMemory.GetItem<bool>("suicidaire");
        moveMemory = tMemory.GetItem<bool>("enDeplacement");
        workingMemory = tMemory.GetItem<bool>("auTravail");

        employeProfile = GameObject.Find("EmployeeProfile").GetComponent<employeeID>();

        data.InitializeEmployee();

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = this.GetComponent<Employe>().data.hairColor;
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = this.GetComponent<Employe>().data.topColor;
    }


	// mets les repères pour l'employé
	public void SetEmployeeLocations () 
	{
		if (boss == null)
		{
			boss = GameObject.FindGameObjectWithTag("Boss");
            emptyChill = new List<GameObject>();

            suicide = new List<GameObject>();
            Window[] suicidesWindows = floor.GetComponentsInChildren<Window>();
            foreach (Window window in suicidesWindows)
            {
                suicide.Add(window.gameObject);
				//emptyChill.Add(window.gameObject);
            }
			
            emptyWorkingHelp = new List<GameObject>();
            InteractWithEmployee[] boxes = floor.GetComponentsInChildren<InteractWithEmployee>();
            foreach (InteractWithEmployee box in boxes)
			{
                if (box.CompareTag("Repos"))
                {
                    emptyChill.Add(box.gameObject);
                }

				if (box.CompareTag("WorkHelp"))
				{
                    emptyWorkingHelp.Add(box.gameObject);
				}
			}
		} 	
	}

	void Update () 
	{
        if (data.fatigue >= data.fatigueMAX && !suicideLock)
        {
            suicideLock = true;
            tMemory.SetItem<bool>("suicidaire", true);

            GameManager.instance.GetComponent<GameManager>().victoryLocked = true;
            employeProfile.setJProfile(0, this.gameObject);
            GameManager.instance.cameraController.FollowEmployee(this.gameObject, 1000,0f);

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
            tSens.DisableSensor("Visual Sensor");
            //tSens.GetSensor("Visual Sensor").IsActive = false;
        }


        Vector3 distance = boss.transform.position - this.transform.position;

        if (distance.magnitude < 15 && !isAlreadyInRange) // si il vient d'entrer dans le champ de vision du boss
        {
            emitActivitySign();
            isAlreadyInRange = true;

            suicideMemory = tMemory.GetItem<bool>("suicidaire");
            moveMemory = tMemory.GetItem<bool>("enDeplacement");
            workingMemory = tMemory.GetItem<bool>("auTravail");
        }
        else if (distance.magnitude >= 15 && isAlreadyInRange) // si il en sort
        {
            isAlreadyInRange = false;
        }else if((suicideMemory  != tMemory.GetItem<bool>("suicidaire") ||
            moveMemory != tMemory.GetItem<bool>("enDeplacement") || workingMemory != tMemory.GetItem<bool>("auTravail")) )
        { // si il change d'état
            
            if( isAlreadyInRange)emitActivitySign();

            GameObject target = tMemory.GetItem<GameObject>("myTarget");
            suicideMemory = tMemory.GetItem<bool>("suicidaire");
            moveMemory = tMemory.GetItem<bool>("enDeplacement");
            workingMemory = tMemory.GetItem<bool>("auTravail");

            //print("ratio : "+(float)(data.fatigue) / (float)(data.fatigueMAX));
            if (((float)(data.fatigue) / (float)(data.fatigueMAX)) > seuilDepression ) 
            {
                //play photocopier;
                if (!depressif)
                {
                    depressif = true;
                    print(this.name + " viens de rentrer en dépression");
                    this.setActiveSound(false, false, false, false, true, false, false, false, false);
                }
            }

            else if (workingMemory && !moveMemory && target.CompareTag("WorkHelp"))
            {
                depressif = false;
                //print("photocopier");
                //play photocopier;
                this.setActiveSound(false, false, false, true,false,false,false,false,false);
            }
            else if (workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                depressif = false;
                //play clavier PC
                //print("clavier");
                this.setActiveSound(false, false, true, false, false, false, false, false, false);
            }
            else if (!workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                depressif = false;
                //play facebook
                //print("facebook");
                this.setActiveSound(false, false, false, false, false, true, false, false, false);
            }
            else if (!workingMemory && !moveMemory && (target.name.Equals("CoffeeTrigger") || target.name.Equals("CoffeeTrigger 1")))
            {
                depressif = false;
                //play coffee
                //print("coffee");
                this.setActiveSound(false, false, false, false, false, false, true, false, false);
            }
            else if (!workingMemory && !moveMemory && target.name.Equals("DrinkTrigger"))
            {
                depressif = false;
                //play vending machine
                //print("vending machine");
                this.setActiveSound(false, false, false, false, false, false, false, true, false);
            }
            else if (!workingMemory && !moveMemory && (target.name.Equals("TVTrigger") || target.name.Equals("TVTrigger 1") ))
            {
                depressif = false;
                //play gaming
                //print("gaming");
                this.setActiveSound(false, false, false, false, false, false, false, false, true);
            }
            else
            {
                depressif = false;
                //print("nothing");
                this.setActiveSound(false, false, false, false, false, false, false, false, false);
            }
        }
 
	}
    
    public void setActiveSound(bool engueulade,bool suicide, bool keyboard, bool photocopier, bool depression, bool facebook,bool coffee, bool vendingMachine, bool gaming)
    {
        /*
        this.gameObject.transform.Find("soundCoffee").gameObject.SetActive(coffee);
        this.gameObject.transform.Find("soundKeyboard").gameObject.SetActive(keyboard);
        this.gameObject.transform.Find("soundPhotocopier").gameObject.SetActive(photocopier);*/
        
        //if (coffee) this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Play();
       // else this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Stop();
        if (engueulade)
        {
            int index = Random.Range(0, engueulageSound.Length);
            engueulageSound[index].Play();
        }
        else
        {
            for (int i = 0; i < engueulageSound.Length; i++)
            {
                engueulageSound[i].Stop();
            }

        }

        if (keyboard && !keyboardSound.isPlaying) keyboardSound.Play();
        else keyboardSound.Stop();      

        if (photocopier && !photocopierSound.isPlaying) photocopierSound.Play();
        else photocopierSound.Stop();

        if (depression && !depressionSound.isPlaying) depressionSound.Play();
        //else depressionSound.Stop();

        if (facebook && !facebookSound.isPlaying) facebookSound.Play();
        else facebookSound.Stop();

        if (coffee && !coffeeSound.isPlaying) coffeeSound.Play();
        else coffeeSound.Stop();

        if (vendingMachine && !vendingMachineSound.isPlaying) vendingMachineSound.Play();
        else vendingMachineSound.Stop();

        if (gaming && !gamingSound.isPlaying) gamingSound.Play();
        else gamingSound.Stop();
    }


    void OnMouseDown() 
    {
        if(GameManager.instance.profileOnClickIsOn){
            employeProfile.setJProfile(0,this.gameObject);
            
            if(GameManager.instance.tutoIsOn && GameManager.instance.goingToLookProfile){
                GameManager.instance.profileLookedAt = true;
            }
        }
    }


    public void emitActivitySign()
    {
        //print("emitactivity");
        GameObject target = tMemory.GetItem<GameObject>("myTarget");
        if (target == null) { 
            //print("TARGET NULL MAYDAY MAYDAY");
            return; 
        }
        if (tMemory.GetItem<bool>("suicidaire"))
        {
            SignEmitter.Create(this.transform.position, SignType.Death,1);
        }
        else if (tMemory.GetItem<bool>("wander"))
        {
            SignEmitter.Create(this.transform.position, SignType.Cellphone,1);
        }
        else if (tMemory.GetItem<bool>("enDeplacement"))
        {
            if (target.CompareTag("Repos"))
            {
                //SignEmitter.Create(this.transform.position, SignType.GoingToGlande);
            }
            if (target.CompareTag("WorkHelp") || target.CompareTag("Box"))
            {
                //SignEmitter.Create(this.transform.position, SignType.GoingToWork);
            }
        }
       
        else if (tMemory.GetItem<bool>("auTravail"))
        {
            SignEmitter.Create(this.transform.position, target.GetComponent<InteractWithEmployee>().signToEmitWork, 1);       
       }
       else
        {
            SignEmitter.Create(this.transform.position, target.GetComponent<InteractWithEmployee>().signToEmitChill, 1);      
        }
    }


	public void setBox (GameObject box)
	{
		boxDeTravail = box;
	}

	public GameObject getBox ()
	{
		return boxDeTravail;
	}


	//public void auTravail ()
	//{
	//	auTravail = true;
	//}
    public void TotalDemotivation()
    {
        data.motivation = data.motivation - data.motivation;
    }

	// Use this for initialization
	public void Engueule (){
		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
		data.fatigue += data.effetEngueulement;
        
        data.motivation += data.effetEngueulement;
        if (data.motivation > data.motivationMax)
            data.motivation = data.motivationMax;

		//if (data.fatigue < data.fatigueMAX) {
			//suicidaire = true;	
			tMemory.SetItem<bool>("hatarake", true);
			tMemory.SetItem<bool>("auTravail", true);
         //   tMemory.SetItem<bool>("wander", false);
            this.setActiveSound(true, false, false, false, false, false, false, false, false);

		//}	
	}

	// Use this for initialization
	public void Suicide (){
        GameManager.instance.GetComponent<GameManager>().victoryLocked = false;
        GameObject window = tMemory.GetItem<GameObject>("myTarget");
        //window.transform.Find("tache").gameObject.SetActive(true);
        //window.GetComponentInChildren<ParticleSystem>().Play();
        GameManager.instance.cameraController.FollowObjectAndShakeAtTheEnd(window, 2f,0.3f,0);
        GameManager.instance.GetComponent<CharacterManager>().sendBoxieToHell(this.gameObject);
        GameManager.instance.GetComponent<GameManager>().nbSuicides++;
        window.GetComponent<Window>().playSuicide(data.isMale, this.GetComponent<Employe>().data.hairColor, this.GetComponent<Employe>().data.topColor);
        this.gameObject.SetActive(false);
        suicideLock = false;
	}

}
