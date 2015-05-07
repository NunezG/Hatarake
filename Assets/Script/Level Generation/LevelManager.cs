using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Office officePrefab;
	
	private Office officeInstance;
	
	public void BeginGame () {
		officeInstance = Instantiate(officePrefab) as Office;
		officeInstance.init ();
	}
	
	private void RestartGame () {
		Destroy(officeInstance.gameObject);
		BeginGame();
	}
	private void Start () {
		//BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	public Office getOfficeInstance(){
		return officeInstance;
	}

}
