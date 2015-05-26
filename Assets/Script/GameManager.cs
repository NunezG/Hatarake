using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//public GameObject sceneObject;
	//public GameObject menu;
    public GameObject tutoButton;
    private GameObject boss;
    private GameObject tutoCoffee, tutoElevator, tutoBossDesk;
	public int tutoStep = 0;
	public float levelObjective;
    public float objectiveCompletion = 0;
    public float time = 0;
    public bool tutoIsOn = true;

    public bool lookingForCoffee, lookingForElevator, telephoneIsRinging, hiring, profile,waitingForObjectiveCompletion,hatarakeTime;//boolean pour tuto

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)			
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		
		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	// Use this for initialization
    void Start()
    {
        if (!tutoIsOn)
        {
            GameObject.Find("Vousetesici").SetActive(false);
        }
        StartCoroutine(gameObject.GetComponent<NavMesh>().GenerateNavmesh());
        // NavMesh ready ???
        StartCoroutine(navMeshCheck());
	}
	
	// Update is called once per frame
	void Update () {

		if (objectiveCompletion < levelObjective) { 
            time = time + Time.deltaTime; 
        }
		else{
            if (tutoIsOn) nextTutoStep();
            //print("Level finished in :" + time);
            //print("YAAAAAAAAAAAAATTTTTTTAAAAAAAAAAAA");
        }

        if (lookingForCoffee)
        {
            if (tutoCoffee == null) tutoCoffee = GameObject.Find("CoffeeTrigger");
            Vector3 distance = boss.transform.position - tutoCoffee.transform.position;
            if (distance.magnitude <= 5)
            {
                nextTutoStep();
            }

        }
        else if (lookingForElevator)
        {
            if (tutoElevator == null) tutoElevator = GameObject.Find("Elevator Cell 4, 0");
            Vector3 distance = boss.transform.position - tutoElevator.transform.position;
            if (distance.magnitude <= 5)
            {
                nextTutoStep();
            }
        }
        else if (telephoneIsRinging)
        {
            if (tutoBossDesk == null) tutoBossDesk = GameObject.Find("spawnBoss");
            Vector3 distance = boss.transform.position - tutoBossDesk.transform.position;
            if (distance.magnitude <= 5)
            {
                nextTutoStep();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            gameObject.GetComponent<CharacterManager>().spawnOneBoxieInElevator(0);
        }
	}

    public void nextTutoStep()
    {
        if (tutoStep == 0)// on vient de cliquer sur le bouton vous etes ici, on affiche va checher café
        {
            tutoStep++;
            tutoButton.GetComponentInChildren<Text>().text = "Va chercher café";
        }
        else if (tutoStep == 1)//on vient de cliquer sur va chercher cafe, donc on se met en mode recherche de cafe
        {
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = false;
            lookingForCoffee = true;
            tutoButton.SetActive(false);
        }
        else if (tutoStep == 2)//on vient de trouver la machine a cafe, on affiche va chercher ascenseur
        {
            lookingForCoffee = false;
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = true;
            tutoButton.SetActive(true);
            tutoButton.GetComponentInChildren<Text>().text = "Va chercher ascenseur";
        }
        else if (tutoStep == 3)//on vient de cliquer sur le bouton va chercher ascenseur, on se met en mode recherche d'ascenseur
        {
            tutoStep++;
            lookingForElevator = true;
            boss.GetComponent<Boss>().moveLocked = false;
            tutoButton.SetActive(false);
        }
        else if (tutoStep == 4)//on vient de trouver l'ascenseur, on affiche va chercher telephone
        {
            lookingForElevator = false;
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = true;
            tutoButton.SetActive(true);
            tutoButton.GetComponentInChildren<Text>().text = "Va chercher téléphone";
        }
        else if (tutoStep == 5)//on vient de cliquer sur va cherche téléphone, on se met en mode recherche de téléphone
        {
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = false;
            tutoButton.SetActive(false);
            telephoneIsRinging = true;
        }
        else if (tutoStep == 6)//on vient de trouver le téléphone, on affiche l'embauche
        {
            telephoneIsRinging = false;
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = true;
            tutoButton.SetActive(true);
            tutoButton.GetComponentInChildren<Text>().text = "Embauche pékin numéro 0";
        }
        else if (tutoStep == 7)//on vient de cliquer sur embauche boxie n0, on affiche la seconde embauche
        {
            this.GetComponent<CharacterManager>().spawnOneBoxieInElevator(0);
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = true;
            tutoButton.GetComponentInChildren<Text>().text = "Embauche pékin numéro 1";
        }
        else if (tutoStep == 8)//on vient de cliquer sur embauche boxie n1, maintenant on attends que l'objectif se remplisse
        {
            this.GetComponent<CharacterManager>().spawnOneBoxieInElevator(0);
            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = false;
            tutoButton.SetActive(false);
        }
        else if (tutoStep == 9)
        {/*

            tutoStep++;
            boss.GetComponent<Boss>().moveLocked = true;
            tutoButton.SetActive(true);
            tutoButton.GetComponentInChildren<Text>().text = "Embauche pékin numéro 1";
            this.GetComponent<CharacterManager>().spawnOneBoxieInElevator(0);*/
        }
    }



    public void SetBoss(GameObject boss)
    {
        this.boss = boss;
    }
	public void InitGame()
	{

		gameObject.GetComponent<LevelManager>().BeginGame();
		
		
		//menu.SetActive (false);
		//GameObject.Instantiate (sceneObject);
	}

	IEnumerator navMeshCheck()
	{		
		NavMesh navMesh = GetComponent<NavMesh> ();

		while (!navMesh.isNavMeshDone) 
		{
			yield return new WaitForSeconds(1);
		}
		gameObject.GetComponent<CharacterManager>().Spawn();
	}
}
