using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//public GameObject sceneObject;
	//public GameObject menu;
	private int level = 1;

	public float levelObjective;
    public float objectiveCompletion = 0;
    public float time = 0;

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
	void Start () {
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
            print("Level finished in :" + time);
            print("YAAAAAAAAAAAAATTTTTTTAAAAAAAAAAAA");
        }
	}

	private void OnLevelWasLoaded(int index)
	{
		level++;
		InitGame();
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
