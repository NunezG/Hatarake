using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//public GameObject sceneObject;
	//public GameObject menu;
    public CameraController cameraController;
    public employeeID employeeProfile;
    public GameObject canvaEmbauche;
    public GameObject victoryButton;
    public Button newMissionButton;
    public Button EndOfDemoButton;
    public GameObject boss=null;
    private GameObject[] tempHiringBoxiesBuffer = new GameObject[3];
	public float levelObjective;
    public float objectiveCompletion = 0;
    public Text clock;
    public float time = 0;
    public bool ongoingHiring = false;
    public AudioSource gongOfVictorySound;

   public int nbEmployeeToHire = 3;
   public int nbEmployeeLeftToHire;
   public bool hiringTime = false;

   public bool workingIsActuallyUsefull = false;
   public bool ringingPhone = false;

   public bool profileOnClickIsOn;
    //---------------------------
   public ArrowIndicator tutoArrow;
   public bool tutoIsOn;
   public bool tutoMoveLock=true, tutoHatarakeLock=true;
   public bool cameraLookingForCoffee, fetchingCoffee, cameraLookingForElevator, goingToElevator,
                cameraLookingForPhone, goingToPhone, freshMeatHired, cameraLookingForFreshMeat,
                goingToLookProfile, profileLookedAt, cameraLookingAtSlacker,
                goingToHatarakeSlacker, employeeHataraked, cameraFollowingEmployeeHataraked,
                tutoBreakIsNotComplete = true,
                timeToStartBreakingShit, cameraLookingForShitToBreak, goingToBreakShit,
                shitBroken,
                decrasseurJustArrived,cameraLookingAtDecrasseur;

    public Button tutoFirstButton, tutoCoffeeButton, tutoDeliciousCoffeeButton,
       tutoLookingForElevatorButton, tutoNobodyHereButton, tutoWtfButton,
       tutoHiringTimeButton, tutoFreshMeatButton, tutoExplicationProfileButton, tutoGoingToGlandeButton,
       tutoHatarakeExplicationButton, tutoSuccessfulHatarakingButton,
       tutoTooMuchGlande,tutoGoBreakShitButton, tutoSweetDestruction, 
       tutoDecrasseurButton;

   public GameObject coffeeTable,coffeeBreak, elevator, phone;
	//Awake is always called before any Start functions


    //----------------GUI
   public float solarClock = 0;
   public bool startSolarClock = false;
   public GameObject GUIClock, GUIQiBar;
   public bool displayProgressionBar;
    //--------------------------
   public bool endOfDemo = false;


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
		//InitGame();
	}

	// Use this for initialization
    void Start()
    {
       // StartCoroutine(gameObject.GetComponent<NavMesh>().GenerateNavmesh());
       
        canvaEmbauche.SetActive(false);

	}
    public bool victoryLocked = false;
    public bool ringingLocked = false;
	// Update is called once per frame
	void Update () {

        if (coffeeTable == null) coffeeTable = GameObject.Find("CoffeeTrigger");
        if (coffeeBreak == null) coffeeBreak = GameObject.Find("tableCafe");
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
                    tutoArrow.gameObject.SetActive(true);
                    tutoArrow.target = coffeeTable;
                }
            }

            if (fetchingCoffee)
            {
                //print("fetching coffee");
                Vector3 distance = boss.transform.position - coffeeTable.transform.position;
                if (distance.magnitude < 5)
                {
                    //print("coffee fetched");
                    tutoArrow.gameObject.SetActive(false);
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

                    tutoArrow.gameObject.SetActive(true);
                    tutoArrow.target = elevator;
                }
            }
            if (goingToElevator)
            {
                //print("going to elevator");
                Vector3 distance = boss.transform.position - elevator.transform.position;
                if (distance.magnitude < 5)
                {
                    tutoArrow.gameObject.SetActive(false);
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
                cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 100,0);

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
                    tutoArrow.gameObject.SetActive(true);
                    tutoArrow.target = this.gameObject.GetComponent<CharacterManager>().boxies[0];
                }
            }
            if (profileLookedAt)
            {
                profileLookedAt = false;
                tutoExplicationProfileButton.gameObject.SetActive(true);
                bossLock(true, true);
                goingToLookProfile = false;
            }
            if (cameraLookingAtSlacker)
            {
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    cameraLookingAtSlacker = false;
                    //TUTO NEXT 
                    tutoGoingToGlandeButton.gameObject.SetActive(false);
                    tutoHatarakeExplicationButton.gameObject.SetActive(true);
                    GUIQiBar.SetActive(true);
                    bossLock(true, true);
                }
            }

            if (employeeHataraked)
            {
                tutoArrow.gameObject.SetActive(false);
                employeeHataraked = false;
                cameraController.dampTime = 0;
                cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 100,0);
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

            if (timeToStartBreakingShit)
            {
                timeToStartBreakingShit = false;
                cameraController.FollowEmployee(coffeeTable, 100, 0.2f);
                tutoTooMuchGlande.gameObject.SetActive(true);
                bossLock(true, true);
                cameraLookingForShitToBreak = true;
            }
            if (cameraLookingForShitToBreak)
            {
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    //print("looked for phone, back to boss");
                    cameraLookingForShitToBreak = false;
                    //TUTO NEXT 
                    goingToBreakShit = true;
                }
            }
            if (goingToBreakShit)
            {
                if (boss.GetComponent<Boss>().moveLocked)
                {
                    tutoArrow.gameObject.SetActive(true);
                    tutoArrow.target = coffeeTable;
                    coffeeBreak.GetComponent<BreakableFurniture>().FullRepair();
                    boss.GetComponent<Boss>().moveLocked = false;
                }
            }
            if (shitBroken)
            {

                tutoArrow.gameObject.SetActive(false);
                shitBroken = false;
                goingToBreakShit = false;
                tutoSweetDestruction.gameObject.SetActive(true);
                bossLock(true, true);
            }

            if (decrasseurJustArrived)
            {
                print("decrasseurJustArrived");
                decrasseurJustArrived = false;
                tutoDecrasseurButton.gameObject.SetActive(true);
                cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().decrasseurs[0], 100, 0);
                bossLock(true, true);
                cameraLookingAtDecrasseur = true;
            }
            if (cameraLookingAtDecrasseur)
            {
                if (!cameraController.cameraIsToMove && cameraController.target == GameManager.instance.boss)
                {
                    cameraLookingAtDecrasseur = false;
                    bossLock(true, true);
                }
            }

        }
        else
        {
            //if (cameraController.dampTime != 0) cameraController.dampTime = 0;

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

            clock.text = strMinutes + "\'" + strSecondes + "\'\'" + strCentiSecondes;
            if (objectiveCompletion < levelObjective)
            {
                if (workingIsActuallyUsefull && boss!=null) time = time + Time.deltaTime;
            }
            else if (boss != null)
            {

                if (endOfDemo)
                {
                    EndOfDemoButton.gameObject.SetActive(true);
                    bossLock(true, true);
                    workingIsActuallyUsefull = false;
                }
                else if (this.GetComponent<CharacterManager>().GetTotalNumberOfBoxies() == 7 && tutoBreakIsNotComplete)
                {
                    timeToStartBreakingShit = true;
                    tutoIsOn = true;
                }
                else
                {
                    if (!victoryLocked)
                    {
                        boss.GetComponent<Boss>().gongOfVictory.Play();
                        print("GOOOOOOOOOOOOOONG");
                        bossLock(true, true);
                        victoryButton.SetActive(true);
                        victoryButton.GetComponentInChildren<Text>().text = "YATTTTA ! \n Objectif atteint en \n" + strMinutes + "\'\'" + strSecondes + "\'" + strCentiSecondes;
                        boss.GetComponent<Boss>().moveLocked = true;
                        boss.GetComponent<Boss>().hatarakeLocked = true;
                    }
                    workingIsActuallyUsefull = false;


                }
            }


            if (hiringTime && !ongoingHiring && nbEmployeeLeftToHire != 0)
            {
                activateHiringRound();
                bossLock(true, true);
            }
            else if (hiringTime && nbEmployeeLeftToHire == 0) // on a finit d'embaucher pour le nouvelle objectif
            {
                Employe.suicideLock = false;
                hiringTime = false;
                workingIsActuallyUsefull = true;
                time = 0;

                bossLock(false, false);
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
    public void BossLockHatarake(bool hatarake)
    {
        boss.GetComponent<Boss>().hatarakeLocked = hatarake;
    }
    public void DemoEndOnClick()
    {
        Application.Quit();
    }

    //<------------------------Method Tuto



    public void TutoCoffeeButtonClick()
    {
        cameraController.FollowEmployee(coffeeTable, 100,0.2f);
        cameraLookingForCoffee = true;
        bossLock(false, true);
    }

    public void TutoLookingForElevatorClick()
    {
        cameraController.FollowEmployee(elevator, 100,0.2f);
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
        cameraController.FollowEmployee(phone, 100,0.2f);
        cameraLookingForPhone = true;
    }
    public void TutoHiringTimeClick()
    {
        tutoArrow.gameObject.SetActive(true);
        tutoArrow.target = phone;
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
        cameraController.FollowEmployee(this.gameObject.GetComponent<CharacterManager>().boxies[0], 200,0);
        cameraLookingAtSlacker = true;
    }

    public void TutoExplicationHatarakeClick()
    {
        goingToHatarakeSlacker = true;
        boss.GetComponent<Boss>().yellingO_Meter = boss.GetComponent<Boss>().maxYellingO_Meter;
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
        GUIClock.SetActive(true);
        displayProgressionBar = true;
    }

    public void TutoWellBreakOtherShitLaterOnClick()
    {
        tutoIsOn = false;
        tutoBreakIsNotComplete = false;
        bossLock(false, false);

    }
    public void TutoDecrasseurOnClick()
    {
        if (!cameraLookingAtDecrasseur)
        {
            tutoIsOn = false;
            bossLock(false, false);
            tutoDecrasseurButton.gameObject.SetActive(false);
        }

    }

    // Methode Tuto --------------------------->
    public void activateNextMissionButton()
    {
        bossLock(true, true);
        newMissionButton.gameObject.SetActive(true);
        newMissionButton.GetComponentInChildren<Text>().text = JobText.GenerateRandomJob();
    }
    public void NextMissionButtonOnClick()
    {
        CalculateNumberOfEmployeeToHire();
        hiringTime = true;
    }

    public void activateHiringRound()
    {
        //CalculateNumberOfEmployeeToHire();

        if (tutoIsOn) nbEmployeeLeftToHire = 1;
        if (!ongoingHiring && nbEmployeeLeftToHire>0)
        {
            print("nbEmployeeLeftToHire :" + nbEmployeeLeftToHire);
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
    public float objectiveIncreaseFactor = 3;
    public void nextObjective()
    {
        bossLock(false, false);

        ringingPhone = true;
        tutoArrow.gameObject.SetActive(true);
        tutoArrow.target = phone;

            if (levelObjective == 10) levelObjective = 50;
            levelObjective = levelObjective * objectiveIncreaseFactor;
            objectiveCompletion = 0;
            time = 0;
            //workingIsActuallyUsefull = true;
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
        if (tutoIsOn)
        {
            //bossLock(true, true);
            tutoFirstButton.gameObject.SetActive(true);
            tutoFirstButton.GetComponent<Button>().interactable = false;
            profileOnClickIsOn = false;
        }
        else
        {
            workingIsActuallyUsefull = true;
            profileOnClickIsOn = true;
        }


        gameObject.GetComponent<LevelManager>().RestartGame();
        
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


}
