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

		if (!NavMesh.isNavMeshDone) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
		}
        if (boss != null)
        {
            DrawYellingOMeter((float)(boss.GetComponent<Boss>().yellingO_Meter) / (float)(boss.GetComponent<Boss>().maxYellingO_Meter));
        }
        DrawProgressObjective(GameManager.instance.objectiveCompletion / GameManager.instance.levelObjective);
        if (GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies() != 0)
            DrawNumberOfWorkingEmploye(GameManager.instance.GetComponent<CharacterManager>().GetNumberOfWorkingBoxies(), GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies());
       
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
    void DrawNumberOfWorkingEmploye(int working,int total)
    {
        for (int i = 0; i < working; i++)
        {
            GUI.DrawTexture(new Rect(10+20*i, 200, 10, 10), progressForeground);
        }
    }
}
