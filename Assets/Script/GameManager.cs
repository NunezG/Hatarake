using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class GameManager : MonoBehaviour {










	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

	public GameObject sceneObject;

	public GameObject menu;
	private int level = 1;


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
	//	InitGame();
	}

	void redoMesh()
	{
		var gameObject = GameObject.FindGameObjectWithTag("NavMesh");
		
		if(gameObject != null)
		{
			var rig = gameObject.GetComponentInChildren<RAIN.Navigation.NavMesh.NavMeshRig>();
			
			if(rig != null)
			{
				var rigMesh = rig.NavMesh;
				
				if(rigMesh != null)
				{
					Debug.Log("Got the mesh " + rigMesh.GraphName);//Navigation graph name
				}
				
				rigMesh.UnregisterNavigationGraph();
				
				rigMesh.StartCreatingContours(rig, 1);//This will create a new mesh path graph based on the navigation rig settings
				//rigMesh.StartCreatingContours(new Vector3(0,0,0), new Vector3(3f,1f,3f), 1);//Create new scale and position. You can access other settings on the rig to change as well.
				while (rigMesh.Creating)
				{
					Debug.Log("Is creating…");
					rigMesh.CreateContours();
					System.Threading.Thread.Sleep(10);
				}
				//rigMesh.CreateAllContours
				
				rigMesh.RegisterNavigationGraph();
			}
		}


	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButton (1)) {




			redoMesh ();


		}



	}


	private void OnLevelWasLoaded(int index)
	{
		level++;
		InitGame();
	}


	public void InitGame()
	{
		menu.SetActive (false);
		GameObject.Instantiate (sceneObject);
	}

}
