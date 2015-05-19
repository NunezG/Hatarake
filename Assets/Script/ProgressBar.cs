using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Texture progressForeground;
	public GameObject gameManager;
	public Texture waitScreen;

	void Start(){

		gameManager = GameObject.Find ("GameManager");

	}

    void OnGUI()
    {

		if (!gameManager.GetComponent<NavMesh> ().isNavMeshDone) {

			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);

		}
       // print("ONGUI ");
        DrawProgress(GetComponent<GameManager>().objectiveCompletion / 1000.0f);
       
    }



 
void DrawProgress(float progress )
{
    GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForeground);
}
}
