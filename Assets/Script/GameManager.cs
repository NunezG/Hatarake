using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//public GameObject sceneObject;
	//public GameObject menu;
    public CameraController cameraController;
    public GameObject canvaEmbauche;
    public GameObject victoryButton;
    public GameObject boss=null;
    private GameObject[] tempHiringBoxiesBuffer = new GameObject[3];
	public float levelObjective;
    public float objectiveCompletion = 0;
    public Text clock;
    public float time = 0;
    public bool ongoingHiring = false;


   public int nbEmployeeToHire = 3;
   public int nbEmployeeLeftToHire;
   public bool hiringTime = false;

   public bool workingIsActuallyUsefull = false;
   public bool ringingPhone = false;

   public bool profileOnClickIsOn;
    //---------------------------
   public bool tutoIsOn;
   public bool tutoMoveLock=true, tutoHatarakeLock=true;
   public bool cameraLookingForCoffee, fetchingCoffee, cameraLookingForElevator, goingToElevator,
                cameraLookingForPhone, goingToPhone, freshMeatHired, cameraLookingForFreshMeat,
                goingToLookProfile, profileLookedAt, cameraLookingAtSlacker,
                goingToHatarakeSlacker, employeeHataraked, cameraFollowingEmployeeHataraked;

    public Button tutoFirstButton, tutoCoffeeButton, tutoDeliciousCoffeeButton,
       tutoLookingForElevatorButton, tutoNobodyHereButton, tutoWtfButton,
       tutoHiringTimeButton, tutoFreshMeatButton, tutoExplicationProfileButton, tutoGoingToGlandeButton,
       tutoHatarakeExplicationButton, tutoSuccessfulHatarakingButton;

   public GameObject coffeeTable, elevator, phone;
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
       // StartCoroutine(gameObject.GetComponent<NavMesh>().GenerateNavmesh());
        if (tutoIsOn)
        {
            //bossLock(true, true);
            tutoFirstButton.gameObject.SetActive(true);
            profileOnClickIsOn = false;
        }
        else
        {
            workingIsActuallyUsefull = true;
            profileOnClickIsOn = true;
        }
        canvaEmbauche.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        if (coffeeTable == null) coffeeTable = GameObject.Find("CoffeeTrigger");
        if (elevator == null) elevator = GameObject.Find("Elevator Cell 4, 0");
        if (phone == null) phone = GameObject.Find("phoneCollider");

        if (tutoIsOn)
        {
            if (cameraLookingForCoffee)
            {
                //print("camera looking for coffee");
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {

                    //print("camera looked for coffee, back to boss");
                    cameraLookingForCoffee = false;
                    //TUTO NEXT 
                    fetchingCoffee = true;

                }
            }

            if (fetchingCoffee)
            {
                //print("fetching coffee");
                Vector3 distance = boss.transform.position - coffeeTable.transform.position;
                if (distance.magnitude < 5)
                {
                    //print("coffee fetched");
                    fetchingCoffee = false;
                    bossLock(true, true);
                    tutoDeliciousCoffeeButton.gameObject.SetActive(true);

                }
            }
            if (cameraLookingForElevator)
            {
                //print("camera looking for elevator");
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    //print("camera looked for elevator, back to boss");
                    bossLock(false, true);
                    cameraLookingForElevator = false;
                    //TUTO NEXT 
                    goingToElevator = true;

                }
            }
            if (goingToElevator)
            {
                //print("going to elevator");
                Vector3 distance = boss.transform.position - elevator.transform.position;
                if (distance.magnitude < 5)
                {
                    //print("at elevator");
                    goingToElevator = false;
                    tutoNobodyHereButton.gameObject.SetActive(true);
                    bossLock(true, true);
                }
            }

            if (cameraLookingForPhone)
            {
                //print("looking for phone");
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    //print("looked for phone, back to boss");
                    cameraLookingForPhone = false;
                    //TUTO NEXT 
                    tutoHiringTimeButton.gameObject.SetActive(true);

                }
            }
            if (freshMeatHired)
            {
                //print("fresh meat hired");
                freshMeatHired = false;
                cameraController.dampTime = 0;
                cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 100);

                cameraLookingForFreshMeat = true;
            }

            if (cameraLookingForFreshMeat)
            {
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    cameraLookingForFreshMeat = false;
                    //TUTO NEXT 
                    tutoFreshMeatButton.gameObject.SetActive(true);
                    bossLock(true, true);
                }
            }
            if (profileLookedAt)
            {
                profileLookedAt = false;
                tutoExplicationProfileButton.gameObject.SetActive(true);
                bossLock(true, true);
            }
            if (cameraLookingAtSlacker)
            {
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    cameraLookingAtSlacker = false;
                    //TUTO NEXT 
                    tutoGoingToGlandeButton.gameObject.SetActive(false);
                    tutoHatarakeExplicationButton.gameObject.SetActive(true);
                    bossLock(true, true);
                }
            }

            if (employeeHataraked)
            {
                employeeHataraked = false;
                cameraController.dampTime = 0;
                cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 100);
                cameraFollowingEmployeeHataraked = true;
            }
            if (cameraFollowingEmployeeHataraked)
            {
                //print("looking for phone");
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    //print("looked for phone, back to boss");
                    cameraFollowingEmployeeHataraked = false;
                    //TUTO NEXT 
                    tutoSuccessfulHatarakingButton.gameObject.SetActive(true);
                    bossLock(true, true);

                }
            }
        }
        else
        {
            if (cameraController.dampTime != 0) cameraController.dampTime = 0;
            int minutes = (int)(time / 60);
            int secondes = (int)(time - 60 * minutes);
            int centisecondes = (int)((time - 60 * minutes - secondes) * 100);
            string strMinutes = "";
            string strSecondes = "";
            string strCentiSecondes = "";
            if (minutes < 10)
                strMinutes = "0" + minutes;
            else
                strMinutes = "" + minutes;
            if (secondes < 10)
                strSecondes = "0" + secondes;
            else
                strSecondes = "" + secondes;
            if (centisecondes < 10)
                strCentiSecondes = "0" + centisecondes;
            else
                strCentiSecondes = "" + centisecondes;

            clock.text = strMinutes + "\'" + strSecondes + "\"" + strCentiSecondes;
            if (objectiveCompletion < levelObjective)
            {
                if (workingIsActuallyUsefull) time = time + Time.deltaTime;
            }
            else if (boss != null)
            {
                victoryButton.SetActive(true);

                victoryButton.GetComponentInChildren<Text>().text = "VICTORY ! \n We triumph once again, objective completed in \n" + strMinutes + ":" + strSecondes + ":" + strCentiSecondes;
                boss.GetComponent<Boss>().moveLocked = true;
                boss.GetComponent<Boss>().hatarakeLocked = true;
                workingIsActuallyUsefull = false;
                ringingPhone = true;
            }


            if (hiringTime && !ongoingHiring && nbEmployeeLeftToHire != 0)
            {
                activateHiringRound();
            }
            else if (hiringTime && nbEmployeeLeftToHire == 0) // on a finit d'embaucher pour le nouvelle objectif
            {
                hiringTime = false;
                workingIsActuallyUsefull = true;
                time = 0;
            }


        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            //gameObject.GetComponent<CharacterManager>().SpawnOneBoxieInElevator(1);
            //if (!ongoingHiring) activateHiringRound();
        }
	}

    public void bossLock(bool move, bool hatarake)
    {
        //print("boss locking, move :" +move+" ,hatarake : "+hatarake );
        tutoMoveLock= boss.GetComponent<Boss>().moveLocked = move;
        tutoHatarakeLock  = boss.GetComponent<Boss>().hatarakeLocked = hatarake;
    }
    public void BossLockMove(bool move)
    {
        boss.GetComponent<Boss>().moveLocked = move;
    }

    //<------------------------Method Tuto

    public void TutoCoffeeButtonClick()
    {
        cameraController.FollowEmployee(coffeeTable, 100);
        cameraLookingForCoffee = true;
        bossLock(false, true);
    }

    public void TutoLookingForElevatorClick()
    {
        cameraController.FollowEmployee(elevator, 100);
        cameraLookingForElevator = true;
        //while (cameraController.onOtherTarget) { }

    }

    public void TutoNobodyHereClick()
    {
        ringingPhone = true;

        //panneau ???
        // camera vers telephone
    }
    public void TutoWtfClick()
    {
        cameraController.FollowEmployee(phone, 100);
        cameraLookingForPhone = true;
    }
    public void TutoHiringTimeClick()
    {
        goingToPhone = true;
        bossLock(false, true);
    }


    public void TutoGoClickProfileClick()
    {
        profileOnClickIsOn = true;
        goingToLookProfile = true;
        bossLock(false, true);
    }

    public void TutoExplicationProfileClick()
    {
        //vider motivation employer + follow avec cam
        this.gameObject.GetComponent<CharacterManager>().boxies[0].GetComponent<Employe>().TotalDemotivation();
        cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 200);
        cameraLookingAtSlacker = true;
    }

    public void TutoExplicationHatarakeClick()
    {
        goingToHatarakeSlacker = true;
        boss.GetComponent<Boss>().yellingO_Meter = 50;
        bossLock(false, false);
    }

    public void TutoEmployeeHataraked()
    {
        goingToHatarakeSlacker = false;
        employeeHataraked = true;
    }

    public void TutoNextObjectiveExplicationClick(){
        workingIsActuallyUsefull = true;
        tutoIsOn = false;
        bossLock(false, false);
    }

    // Methode Tuto --------------------------->

    public void activateHiringRound()
    {
        if (!ongoingHiring)
        {
            bossLock(true, true);
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

    public void terminateHiringRound(int j)
    {
        bossLock(false, false);
        if (tutoIsOn) freshMeatHired = true;

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
    public float objectiveIncreaseFactor = 2;
    public void nextObjective()
    {
        boss.GetComponent<Boss>().moveLocked = false;
        boss.GetComponent<Boss>().hatarakeLocked = false;


            if (levelObjective == 0) levelObjective = 50;
            levelObjective = levelObjective * objectiveIncreaseFactor;
            objectiveCompletion = 0;
            time = 0;
        //hiringTime = true;
        //nbEmployeeToHire++;

    }

    public void CalculateNumberOfEmployeeToHire()
    {
        int freeBoxes = 20 - this.GetComponent<CharacterManager>().GetTotalNumberOfBoxies();
        if (freeBoxes >= nbEmployeeToHire) nbEmployeeLeftToHire = nbEmployeeToHire;
        else nbEmployeeLeftToHire = freeBoxes;

    }



    public void SetBoss(GameObject boss)
    {
        this.boss = boss;
    }
	public void InitGame()
	{

		gameObject.GetComponent<LevelManager>().BeginGame();
        
        // NavMesh ready ???
        StartCoroutine(navMeshCheck());
		
		//menu.SetActive (false);
		//GameObject.Instantiate (sceneObject);
	}

	IEnumerator navMeshCheck()
	{		
		//NavMesh navMesh = GetComponent<NavMesh> ();

        while (!NavMesh.isNavMeshDone) 
		{
			yield return new WaitForSeconds(1);
		}
        Destroy(this.GetComponent<AudioListener>());
		gameObject.GetComponent<CharacterManager>().Spawn();
	}

    public void launchHiring()
    {

    }
}
