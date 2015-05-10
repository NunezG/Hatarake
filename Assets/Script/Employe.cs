using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation;

public class Employe : MonoBehaviour {
	
	//public bool suicidaire;
	public float feignantise = 10;// variable conditionnant le temps entre chaque pause (constante définit lors de la génération aléatoire d’employés, valeur unique pour le premier proto);
	public float motivation = 50;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;
	public float fatigue= 0;// variable similaire à la vie, diminue qd il se fait engueuler, augmente lors de ses pauses. si == fatigueMAX -> suicidaire = true;
	public float fatigueMAX = 100;  
	private GameObject boxDeTravail;// Box de l’employé


	public float vitesseTravail = 5;
	public float vitesseFatigue = 10;
	public float effetRepos = 10;
	public float effetEngueulement = 200;




	/**
	 * true = au taff
	 * false = en train de glander (engueulable)
	 * si auTravail == true (true vers la photocop, vers le box) || false (true vers la salle de repos, vers les chiottes)
	 * si auTravail == true (false dans le box, à la photcop) || false (false à la salle de repos, au chiottes)
	**/
	public bool auTravail;
	bool enDeplacement;// utile pour l’animation notamment
	float vitesseDeTravail = 50;// vitesse de production (plus grand si l’employé est bosseur => valeur unique par défaut pour le premier proto).
	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;

	public GameObject[] chill;
	private RAIN.Memory.BasicMemory tMemory;

	private RAIN.Navigation.BasicNavigator tNav;

	private Vector3 oldPostion;

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
		chill = GameObject.FindGameObjectsWithTag("Repos");
      
	}

	void Update () 
	{


		tMemory.SetItem ("deltaPos", oldPostion - transform.position);

		//tNav.CurrentGraph.ClearNodes ();
		//tNav.RestartPathfindingSearch();

		//print("PATH LENGHT" + tNav.CurrentPath.GetLength());



		//oldPostion = transform.position;



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
	public IEnumerator Travaille () 
	{
		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.

		auTravail  = true;
        setTaget(null);	
		while (motivation > 20) 
		{
			//print("MOTIV: "+ motivation);
			//fatigue += vitesseFatigue;
			motivation -= feignantise;
			yield return new WaitForSeconds((float)(1.0f / vitesseTravail));
		}

		int index = Random.Range(0, chill.Length);
        print("CHANGE TARGET: " + chill[index]);

		setTaget(chill[index]);	

	}

	// Use this for initialization
	public IEnumerator Repos ()
    {
        setTaget(null);	
		while (motivation < 50) 
		{
			if (fatigue > 0 )
				fatigue -= vitesseFatigue;
			motivation += effetRepos;
			yield return new WaitForSeconds(1.0f / vitesseTravail);
		}
        print("CHANGE TARGET: " + boxDeTravail);

			setTaget(boxDeTravail);													
	}



	// Use this for initialization
	public void Engueule () 
	{



		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
		fatigue += effetEngueulement;
		auTravail  = true;
		print ("ENGUEULE EMPLOYE :" +fatigue );

		if (fatigue >= fatigueMAX) {
			//suicidaire = true;		
			print("SUICIDAIRE!!!!!!!");

			tMemory.SetItem("suicidaire",true);
		}
	}


	
	// Use this for initialization
	public void Suicide () 
	{
		Destroy (gameObject);
	}

}
