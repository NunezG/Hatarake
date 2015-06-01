using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//public GameObject sceneObject;
	//public GameObject menu;
    public GameObject canvaEmbauche;
    public GameObject tutoButton,victoryButton;
    private GameObject boss;
    private GameObject tutoCoffee, tutoElevator, tutoBossDesk;
    private GameObject[] tempHiringBoxiesBuffer = new GameObject[3];
	public int tutoStep = 0;
	public float levelObjective;
    public float objectiveCompletion = 0;
    public float time = 0;
    public bool tutoIsOn = true, ongoingHiring = false;

    public bool lookingForCoffee, lookingForElevator, telephoneIsRinging,
                hiring, profile,waitingForObjectiveCompletion,hatarakeTime;//boolean pour tuto

   public int nbEmployeeToHire = 1;
   public int nbEmployeeLeftToHire;
   bool hiringTime = false;

   public bool workingIsActuallyUsefull = true;

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
            tutoButton.SetActive(false);
        }
        StartCoroutine(gameObject.GetComponent<NavMesh>().GenerateNavmesh());
        // NavMesh ready ???
        StartCoroutine(navMeshCheck());
        canvaEmbauche.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

		if (objectiveCompletion < levelObjective) { 
            time = time + Time.deltaTime; 
        }
		else{
            victoryButton.SetActive(true);
            victoryButton.GetComponentInChildren<Text>().text = "VICTORY ! \n We triumph once again : " + levelObjective*time +" funbucks obtained!";
            boss.GetComponent<Boss>().moveLocked = true;
            boss.GetComponent<Boss>().hatarakeLocked = true;
            workingIsActuallyUsefull = false;
            //levelObjective = 10000;
            objectiveCompletion=time = 0;
        }
        if (tutoIsOn)
        {
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
        }

        if (hiringTime && !ongoingHiring && nbEmployeeLeftToHire!=0)
        {
            //nombre d'employe a embaucher
            //temps à attendre entre chaque embauche
            activateHiringRound();
        }
        else if (hiringTime && nbEmployeeLeftToHire == 0) // on a finit d'embaucher pour le nouvelle objectif
        {
            hiringTime = false;
            workingIsActuallyUsefull = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            //gameObject.GetComponent<CharacterManager>().SpawnOneBoxieInElevator(1);
            if (!ongoingHiring)activateHiringRound();
        }
	}

    public void activateHiringRound()
    {
        if (!ongoingHiring)
        {
            boss.GetComponent<Boss>().moveLocked = true;
            boss.GetComponent<Boss>().hatarakeLocked = true;
            ongoingHiring = true;
            canvaEmbauche.SetActive(true);
            tempHiringBoxiesBuffer[0] = this.GetComponent<CharacterManager>().GenerateOneBoxieForHire();
            canvaEmbauche.GetComponent<employeeID>().setJProfile(0, tempHiringBoxiesBuffer[0]);
            tempHiringBoxiesBuffer[1] = this.GetComponent<CharacterManager>().GenerateOneBoxieForHire();
            canvaEmbauche.GetComponent<employeeID>().setJProfile(1, tempHiringBoxiesBuffer[1]);
            tempHiringBoxiesBuffer[2] = this.GetComponent<CharacterManager>().GenerateOneBoxieForHire();
            canvaEmbauche.GetComponent<employeeID>().setJProfile(2, tempHiringBoxiesBuffer[2]);
        }
    }

    public void employeeWork(float deltaTime, float workSpeed)
    {
        if(workingIsActuallyUsefull)
            objectiveCompletion = objectiveCompletion + (deltaTime * workSpeed); 
    }

    public bool tutoHiringBool = false;
    public void terminateHiringRound(int j)
    {
        if (!tutoHiringBool)
        {
            tutoHiringBool = true;

        }
        canvaEmbauche.GetComponent<employeeID>().nullifyAllProfile(); // get rid of all the profile
        GameObject boxieToHire = tempHiringBoxiesBuffer[j];
        for (int i = 0; i < tempHiringBoxiesBuffer.Length; i++)
        {
            if (i != j)
            {
                this.GetComponent<CharacterManager>().sendBoxieToHell(tempHiringBoxiesBuffer[i]);// destroy the now useless other temporary boxies
            }
            tempHiringBoxiesBuffer[i] = null; // empty the temporary boxies buffer
        }
        canvaEmbauche.SetActive(false);

        this.GetComponent<CharacterManager>().AddBoxieFromeHire(boxieToHire);//add boxie de to the proper floor
        ongoingHiring = false;
        boss.GetComponent<Boss>().moveLocked = false;
        boss.GetComponent<Boss>().hatarakeLocked = false;
        nbEmployeeLeftToHire--;
    }

    public void nextObjective()
    {
        boss.GetComponent<Boss>().moveLocked = false;
        boss.GetComponent<Boss>().hatarakeLocked = false;

        if (tutoIsOn) nextTutoStep();
        else
        {
            levelObjective = levelObjective * 1.2f;
            objectiveCompletion = 0;
            time = 0;
        }
        hiringTime = true;
        nbEmployeeToHire++;
        nbEmployeeLeftToHire = nbEmployeeToHire;

    }

    public void nextTutoStep()
    {
        if (tutoIsOn)
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
                this.GetComponent<CharacterManager>().SpawnOneBoxieInElevator(0);
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = true;
                tutoButton.GetComponentInChildren<Text>().text = "Embauche pékin numéro 1";
            }
            else if (tutoStep == 8)//on vient de cliquer sur embauche boxie n1, maintenant on attends que l'objectif se remplisse
            {
                this.GetComponent<CharacterManager>().SpawnOneBoxieInElevator(0);
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = false;
                tutoButton.SetActive(false);
            }
            else if (tutoStep == 9)//on vient de cliquer sur le panneau de victoire
            {
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = true;
                tutoButton.SetActive(true);
                tutoButton.GetComponentInChildren<Text>().text = "Va cliquer profil";
            }
            else if (tutoStep == 10)//on vient de cliquer sur le panneau va cliquer profil, il faut qu'on aille cliquer sur un profil
            {
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = false;
                tutoButton.SetActive(false);
                profile = true;
            }
            else if (tutoStep == 11)//on vient de sortir d'un profil
            {
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = true;
                tutoButton.SetActive(true);
                tutoButton.GetComponentInChildren<Text>().text = "Embauche de X pekins";
            }
            else if (tutoStep == 12)//on vient de finir l'embauche
            {
                tutoStep++;
                tutoButton.GetComponentInChildren<Text>().text = "Va gueuler sur quelqu'un";
            }
            else if (tutoStep == 13)//on vient de cliquer sur le panneau va gueuler
            {
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = false;
                boss.GetComponent<Boss>().hatarakeLocked = false;
                tutoButton.SetActive(false);
            }
            else if (tutoStep == 14)//on vient de gueuler sur quelqu'un, fin du tuto
            {
                tutoStep++;
                boss.GetComponent<Boss>().moveLocked = true;
                tutoButton.SetActive(true);
                tutoButton.GetComponentInChildren<Text>().text = "TUTO FINI !!! ";
            }
            else if (tutoStep == 15)//on vient de cliquer sur tuto fini
            {
                tutoStep++;
                tutoIsOn = false;
                boss.GetComponent<Boss>().moveLocked = false;
                boss.GetComponent<Boss>().hatarakeLocked = false;
                tutoButton.SetActive(false);
            }
        }
    }

    bool boolLock = false;
    public void tutoOutOfProfile()
    {
        if (!boolLock)
        {
            boolLock = true;
            nextTutoStep();
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

    public void launchHiring()
    {

    }
}
