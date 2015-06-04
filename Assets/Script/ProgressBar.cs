using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Texture progressForeground;
    public Texture progressBackground;
    public Texture waitScreen;
	public GameObject qiBar;
	public Sprite[] qiBarSteps;
    public GameObject boss = null;

    void Start()
    {

    }

    void Update()
    {
        if (boss == null) boss = GameObject.FindGameObjectWithTag("Boss");
        else
        {
			DrawYellingOMeter();
        }

    }


    void OnGUI()
    {

		if (!NavMesh.isNavMeshDone) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
		}

        DrawProgressObjective(GameManager.instance.objectiveCompletion / GameManager.instance.levelObjective);
        if (GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies() != 0)
            DrawNumberOfWorkingEmploye(GameManager.instance.GetComponent<CharacterManager>().GetNumberOfWorkingBoxies(), GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies());
       
    
    }

	
    void DrawProgressObjective(float progress )
    {
        GUI.DrawTexture(new Rect(10, 10, 200, 30), progressBackground);
        GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForeground);
		   // if (progress > 1.0) Destroy (this);
    }

    void DrawYellingOMeter()
    {
		int valueQi = (int)(boss.GetComponent<Boss> ().yellingO_Meter / (float)boss.GetComponent<Boss> ().maxYellingO_Meter * 9.0f);
		if(valueQi > 8) valueQi = 8;
		qiBar.GetComponent<Image> ().sprite = qiBarSteps[valueQi];
		Debug.Log (valueQi);
    }

    void DrawNumberOfWorkingEmploye(int working,int total)
    {
        for (int i = 0; i < working; i++)
        {
            GUI.DrawTexture(new Rect(10+20*i, 200, 10, 10), progressForeground);
        }
    }
}
