using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Texture progressForeground;
	public Texture waitScreen;

    public GameObject boss=null;

    void Update()
    {
        if (boss == null) boss = GameObject.FindGameObjectWithTag("Boss");
    }


    void OnGUI()
    {

		if (!GameObject.Find ("GameManager").GetComponent<NavMesh> ().isNavMeshDone) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
		}
        if (boss != null)
        {
            DrawYellingOMeter((float)(boss.GetComponent<Boss>().yellingO_Meter) / (float)(boss.GetComponent<Boss>().maxYellingO_Meter));
        }
        DrawProgressObjective(GameManager.instance.objectiveCompletion / GameManager.instance.levelObjective);
       
    }

	
    void DrawProgressObjective(float progress )
    {
        GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForeground);
		   // if (progress > 1.0) Destroy (this);
    }
    void DrawYellingOMeter(float value)
    {
        //print("value : " + value);
        GUI.DrawTexture(new Rect(10, 100, 200 * value, 30), progressForeground);
        // if (progress > 1.0) Destroy (this);
    }
}
