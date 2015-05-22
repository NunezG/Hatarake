using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Texture progressForeground;
	public Texture waitScreen;


    void OnGUI()
    {

		if (!GameObject.Find ("GameManager").GetComponent<NavMesh> ().isNavMeshDone) {

			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);

		}

		DrawProgress(GetComponent<GameManager>().objectiveCompletion / GetComponent<GameManager>().levelObjective);
       
    }

	
void DrawProgress(float progress )
{
    GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForeground);
}
}
