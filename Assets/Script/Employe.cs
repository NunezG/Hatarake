using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using RAIN.Core;
using RAIN.Navigation;

public class Employe : MonoBehaviour {
	
	//public bool suicidaire;
	public float feignantise = 10;// variable conditionnant le temps entre chaque pause (constante définit lors de la génération aléatoire d’employés, valeur unique pour le premier proto);
	public float motivation = 500;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
    public float motivationMax = 500;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public float fatigue= 0;// variable similaire à la vie, diminue qd il se fait engueuler, augmente lors de ses pauses. si == fatigueMAX -> suicidaire = true;
	public float fatigueMAX = 100;  
	public GameObject boxDeTravail;// Box de l’employé

	//public float vitesseTravail = 5;
	public float vitesseFatigue = 10;
	//public int effetRepos = 10;
	public float effetEngueulement = 200;

	/**
	 * true = au taff
	 * false = en train de glander (engueulable)
	 * si auTravail == true (true vers la photocop, vers le box) || false (true vers la salle de repos, vers les chiottes)
	 * si auTravail == true (false dans le box, à la photcop) || false (false à la salle de repos, au chiottes)
	**/
	public bool auTravail;
	bool enDeplacement;// utile pour l’animation notamment
	//float vitesseDeTravail = 50;// vitesse de production (plus grand si l’employé est bosseur => valeur unique par défaut pour le premier proto).
	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;

	public GameObject floor;

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
		data.InitializeEmployee ();

		Repos[] chills = floor.GetComponentsInChildren<Repos> ();
		Box[] boxes = floor.GetComponentsInChildren<Box> ();

		foreach (Repos chi in chills) 
		{
			chill.Add(chi.gameObject);	
		}

		foreach (Box box in boxes) {
			if (box.CompareTag("Box") && box.assigne == false)
			{
				workingHelp.Add(box.gameObject);
			}
		}
	}

	void Update () 
	{
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
		fatigue += effetEngueulement;
		auTravail  = true;

		if (fatigue >= fatigueMAX) {
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
