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
			if (box.CompareTag("WorkHelp"))
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
