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
	public bool auTravail;
	bool enDeplacement;// utile pour l’animation notamment
	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;

	public GameObject floor;
    public GameObject boss;

	public List<GameObject> chill;
	public List<GameObject> workingHelp;

	public RAIN.Memory.BasicMemory tMemory;
	private RAIN.Navigation.BasicNavigator tNav;

	public EmployeNames data;
	//Awake is always called before any Start functions
	void Awake()
	{
		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
		tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator;
	}

	// Use this for initialization
	void Start () 
	{
        boss= GameObject.Find("Boss");

		data.InitializeEmployee ();

		Repos[] chills = floor.GetComponentsInChildren<Repos> ();
		Box[] boxes = floor.GetComponentsInChildren<Box> ();

		foreach (Repos chi in chills) 
		{
			chill.Add(chi.gameObject);	
		}

		foreach (Box box in boxes) {
			if (box.CompareTag("WorkHelp"))
			{
				workingHelp.Add(box.gameObject);
			}
		}
	}

	void Update () 
	{
        GameObject target = tMemory.GetItem<GameObject>("myTarget");
        Vector3 distance = boss.transform.position - this.transform.position;
        if (distance.magnitude > 15)
        {

        }



        if (tMemory.GetItem<bool>("suicidaire"))
        {
            SignEmitter.Create(this.transform.position, SignType.Death);
        }

        else if (tMemory.GetItem<bool>("enDeplacement"))
        {
            if(target.CompareTag("Repos"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToGlande);
            }
            if (target.CompareTag("WorkHelp"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToWork);
            }
        }
        else if (tMemory.GetItem<bool>("chilling"))
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
            else if (target.name.Equals("ToiletTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Toilet);
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
	}

	public void setTaget (GameObject target)
	{
		if (target != null)
		    tMemory.SetItem("enDeplacement",true);
		else tMemory.SetItem("enDeplacement",false);
		    tMemory.SetItem("myTarget",target);
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
	public void Engueule () 
	{
		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
		data.fatigue += data.effetEngueulement;
        data.motivation += data.effetEngueulement;
        tMemory.SetItem("auTravail", true);
        auTravail  = true;

		if (data.fatigue >= data.fatigueMAX) {
			//suicidaire = true;		
			tMemory.SetItem("suicidaire",true);
		}
	}

	// Use this for initialization
	public void Suicide () 
	{
		Destroy (gameObject);
	}

}
