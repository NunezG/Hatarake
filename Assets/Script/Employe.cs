using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using RAIN.Core;
using RAIN.Navigation;

public class Employe : MonoBehaviour {
	
	//public bool suicidaire;
	public GameObject boxDeTravail;// Box de l’employé
   
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
    private RAIN.Navigation.BasicNavigator tNav;

	public EmployeNames data;
	//Awake is always called before any Start functions

    public bool isAlreadyInRange, moveMemory, workingMemory,suicideMemory;

	void Awake()
	{
		AIRig aiRig = GetComponentInChildren<AIRig>();
        tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator; 

         
	}

	// Use this for initialization
	void Start () 
	{
        isAlreadyInRange = false;
        suicideMemory = tMemory.GetItem<bool>("suicidaire");
        moveMemory = tMemory.GetItem<bool>("enDeplacement");
        workingMemory=tMemory.GetItem<bool>("auTravail");
        
        //setActiveSound(false, false, false);

		data.InitializeEmployee ();

		if (boss == null)
		{
			boss = GameObject.FindGameObjectWithTag("Boss");

			//chill = new List<GameObject>();
            emptyChill = new List<GameObject>();
			Repos[] chills = floor.GetComponentsInChildren<Repos>();
			foreach (Repos chi in chills)
			{
                emptyChill.Add(chi.gameObject);
			}

            suicide = new List<GameObject>();
            SuicideWindow[] suicidesWindows = floor.GetComponentsInChildren<SuicideWindow>();
            foreach (SuicideWindow window in suicidesWindows)
            {
                suicide.Add(window.gameObject);
            }
			
			//workingHelp = new List<GameObject>();
            emptyWorkingHelp = new List<GameObject>();
			Box[] boxes = floor.GetComponentsInChildren<Box>();
			foreach (Box box in boxes)
			{
				if (box.CompareTag("WorkHelp"))
				{
                    emptyWorkingHelp.Add(box.gameObject);
				}
			}
		}

      	
	}

	void Update () 
	{
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
        { // si il est dans le champ et qu'il change d'état
            
            if( isAlreadyInRange)emitActivitySign();

            GameObject target = tMemory.GetItem<GameObject>("myTarget");
            suicideMemory = tMemory.GetItem<bool>("suicidaire");
            moveMemory = tMemory.GetItem<bool>("enDeplacement");
            workingMemory = tMemory.GetItem<bool>("auTravail");

            if (workingMemory && !moveMemory && target.CompareTag("WorkHelp"))
            {
                //print("work");
                this.setActiveSound(false, false, true);
            }
            else if (workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                //play clavier PC
                this.setActiveSound(false, true, false);
            }
            else if (!workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                //play facebook
                this.setActiveSound(false, true, false);
            }
            else if (!workingMemory && !moveMemory && (target.name.Equals("CoffeeTrigger") || target.name.Equals("DrinkTrigger")))
            {
                //play drink
                this.setActiveSound(true, false, false);
            }
            else if (!workingMemory && !moveMemory && target.name.Equals("TVTrigger"))
            {
                //play tv
                this.setActiveSound(false, false, false);
            }
            else
            {
                this.setActiveSound(false, false, false);
            }
        }
 
	}
    public void setActiveSound(bool coffee, bool keyboard, bool photocopier)
    {
        /*
        this.gameObject.transform.Find("soundCoffee").gameObject.SetActive(coffee);
        this.gameObject.transform.Find("soundKeyboard").gameObject.SetActive(keyboard);
        this.gameObject.transform.Find("soundPhotocopier").gameObject.SetActive(photocopier);*/
        if (coffee) this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Play();
        else this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Stop();
        if (keyboard) this.gameObject.transform.Find("soundKeyboard").gameObject.GetComponent<AudioSource>().Play();
        else this.gameObject.transform.Find("soundKeyboard").gameObject.GetComponent<AudioSource>().Stop();
        if (photocopier) this.gameObject.transform.Find("soundPhotocopier").gameObject.GetComponent<AudioSource>().Play();
        else this.gameObject.transform.Find("soundPhotocopier").gameObject.GetComponent<AudioSource>().Stop();

        //if (this.gameObject.transform.Find("soundPhotocopier").gameObject.GetComponent<AudioSource>().isPlaying) print("photocopier playing");
        //if (this.gameObject.transform.Find("soundKeyboard").gameObject.GetComponent<AudioSource>().isPlaying) print("keyboard playing");
        //if (this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().isPlaying) print("playing coffee");
    }


    public void emitActivitySign()
    {
        //print("emitactivity");
        GameObject target = tMemory.GetItem<GameObject>("myTarget");
        if (tMemory.GetItem<bool>("suicidaire"))
        {
            SignEmitter.Create(this.transform.position, SignType.Death);
        }
        else if (tMemory.GetItem<bool>("enDeplacement"))
        {
            if (target.CompareTag("Repos"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToGlande);
            }
            if (target.CompareTag("WorkHelp") || target.CompareTag("Box"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToWork);
            }
        }
       
        else if (tMemory.GetItem<bool>("auTravail"))
        {
            if (target.name.Equals("PhotocopierTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Photocopier);
            }
            else if (target.name.Equals("WorkBoxTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Work);
            }
        }
        else
        {
            if (target.name.Equals("CoffeeTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Coffee);
            }
            else if (target.name.Equals("ToiletTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Toilet);
            }
            else if (target.name.Equals("DrinkTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Drink);
            }
            else if (target.name.Equals("WorkBoxTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Facebook);
            }
            else if (target.name.Equals("TVTrigger") || target.name.Equals("TVTrigger 1") || target.name.Equals("TVTrigger 2"))
            {
                SignEmitter.Create(this.transform.position, SignType.Tv);
            }
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


	// Use this for initialization
	public void Engueule (){
		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
		data.fatigue += data.effetEngueulement;
        data.motivation += data.effetEngueulement;
       // tMemory.SetItem("hatarake", true);
        tMemory.SetItem("auTravail", true);
        
		if (data.fatigue >= data.fatigueMAX) {
			//suicidaire = true;		
			tMemory.SetItem("suicidaire",true);
		}
	}

	// Use this for initialization
	public void Suicide (){

        //tMemory.GetItem("suicidaire"); 
        GameObject window = tMemory.GetItem<GameObject>("myTarget");
        window.transform.Find("tache").gameObject.SetActive(true);
        window.transform.Find("brokenWindow").gameObject.GetComponent<ParticleSystem>().Play();
		//Destroy (this.gameObject);
        this.gameObject.SetActive(false);
	}

}
