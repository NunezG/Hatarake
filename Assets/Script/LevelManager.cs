using UnityEngine;
using System.Collections;
//using RAIN.Minds;
using RAIN.Core;

public class LevelManager : MonoBehaviour {
		
	public GameObject boxiePrefab;
	public int nombreBoxies = 10;
	GameObject[] boxies;
	int nombreDecrasseurs;
	int nombreMarketeux;
	float production; //(production actuel)
	float productionParSec; //(somme de chaque vitesseDeTravail des employés)
	float objectifNiveau; //(production à atteindre pour atteindre le niveau suivant)

	
	public Office officePrefab;
	
	private Office officeInstance;
	
	private void BeginGame () {
		officeInstance = Instantiate(officePrefab) as Office;
		officeInstance.init ();
	}
	
	private void RestartGame () {
		Destroy(officeInstance.gameObject);
		BeginGame();
	}
	private void Start () {

		BeginGame();


		StartCoroutine(gameObject.GetComponent<NavMesh>().GenerateNavmesh());
		// NavMesh ready ???
		StartCoroutine(navMeshCheck());
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}
	
	public Office getOfficeInstance(){
		return officeInstance;
	}



	
	IEnumerator navMeshCheck()
	{		
		NavMesh navMesh = GetComponent<NavMesh> ();

		yield return new WaitForSeconds(1);
		if (navMesh.isNavMeshDone)
		{
			//Debug.Log("NavMesh FINISHED: " + navMesh.isNavMeshDone);
			Spawn();
		}
		else { StartCoroutine(navMeshCheck()); }
		
	}


	private void Spawn()
	{
		boxies = new GameObject[nombreBoxies];

		// create Player
		for (int i =0; i<nombreBoxies; i++) {
			//Debug.Log("BOBOBOXIE");
			boxies [i] = (GameObject)Instantiate (boxiePrefab);
			boxies [i].transform.position = officePrefab.transform.position;
			//boxies [i].transform.rotation = officePrefab.transform.rotation;
			//Debug.Log(boxies [i].GetComponent<Collider>().bounds.extents.y);

			boxies [i].transform.Translate(Random.Range(0,4),boxies [i].GetComponent<Collider>().bounds.extents.y,Random.Range(0,3));

			//boxies [i].gameObject.GetComponentInChildren<AIRig>().AI.Motor.DefaultSpeed = 10;
		}
		//BTAsset bTree = ScriptableObject.CreateInstance<BTAsset>();
		
		// TODO Speed ???

		
		// target
		//player.gameObject.GetComponentInChildren<AIRig>().AI.Motor.MoveTarget.VariableTarget = "NavTarget";
		//player.gameObject.GetComponentInChildren<AIRig>().AI.Motor.MoveTarget.VectorTarget = new Vector3(0, 0, 0);
		//player.gameObject.GetComponentInChildren<AIRig>().AI.Motor.MoveTo(new Vector3(20, 1, 5));
		//player.gameObject.GetComponentInChildren<AIRig>().AI.Motor.Move();
		//player.gameObject.GetComponentInChildren<AIRig>().AI.Motor.MoveTarget.VectorTarget = player.gameObject.GetComponentInChildren<AIRig>().WorkingMemory.GetItem<Vector3>("NavTarget");
		
		// Create the asset
		
		// Assign the tree xml, mine was exported from another tree and saved within my project
	/*	BTAsset tCustomAsset = ScriptableObject.CreateInstance<BTAsset>();
		TextAsset xml = Resources.Load("btrees/test2") as TextAsset;
		tCustomAsset.SetTreeData(xml.text);
		
		tCustomAsset.SetTreeBindings(new string[0] { });
		((BasicMind)player.gameObject.GetComponentInChildren<AIRig>().AI.Mind).SetBehavior(tCustomAsset, new List<BTAssetBinding>());
	*/	
		
		
	}


}
