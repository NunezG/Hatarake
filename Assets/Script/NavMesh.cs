using UnityEngine;
using System.Collections;
using RAIN.Navigation.NavMesh;

public class NavMesh : MonoBehaviour {

	private int _threadCount = 4;
	public GameObject navMesh;

	public bool isNavMeshDone = false;

	NavMeshRig tRig;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (1)) {

			// create NavMesh
			//tRig = gameObject.GetComponentInChildren<RAIN.Navigation.NavMesh.NavMeshRig>();


			//redoMesh();
		//	StartCoroutine(GenerateNavmesh());
					
		}

	}



	// This will regenerate the navigation mesh when called
	public IEnumerator GenerateNavmesh()
	{
		navMesh = Instantiate (navMesh);
	//transform.position = 
		LevelManager level = gameObject.GetComponent<LevelManager>();
		navMesh.transform.position = level.officePrefab.transform.position;

		tRig = navMesh.GetComponent<RAIN.Navigation.NavMesh.NavMeshRig>();

		if(tRig == null) Debug.Log("EROROROROROR" );

		// Unregister any navigation mesh we may already have (probably none if you are using this)
		tRig.NavMesh.UnregisterNavigationGraph();
		tRig.NavMesh.Size = 80;
		float startTime = Time.time;
		tRig.NavMesh.StartCreatingContours(tRig, _threadCount);
		while (tRig.NavMesh.Creating)
		{
			tRig.NavMesh.CreateContours();
			
			yield return new WaitForSeconds(1);
		}
		isNavMeshDone = true;
		float endTime = Time.time;
		Debug.Log("NavMesh generated in " + (endTime - startTime) + "s");
		tRig.NavMesh.RegisterNavigationGraph();
		tRig.Awake();
		
	}










	/*



	void redoMesh()
	{
		navMesh = Instantiate (navMesh);
		//var gameObject = GameObject.FindGameObjectWithTag("NavMesh");
		
		if(navMesh != null)
		{
			var rig = navMesh.GetComponent<RAIN.Navigation.NavMesh.NavMeshRig>();
			
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


*/

}
