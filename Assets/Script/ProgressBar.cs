using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public Texture progressForeground;
    public Texture progressBackground;
    public Texture waitScreen;
    public Texture ki;
    public RectTransform valueKi;
    float unitaryX, unitaryWidth;
    public GameObject boss = null;

    void Start()
    {
        unitaryX = valueKi.anchoredPosition.x;
        unitaryWidth = valueKi.rect.width;

    }

    void Update()
    {
        if (boss == null) boss = GameObject.FindGameObjectWithTag("Boss");
        else
        {

            valueKi.sizeDelta = new Vector2(unitaryWidth * ((float)boss.GetComponent<Boss>().yellingO_Meter/2), valueKi.rect.height);
            valueKi.anchoredPosition = new Vector2(valueKi.sizeDelta.x/2, valueKi.anchoredPosition.y);
        }

    }


    void OnGUI()
    {

		if (!NavMesh.isNavMeshDone) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
		}
        if (boss != null)
        {
            //DrawYellingOMeter((float)(boss.GetComponent<Boss>().yellingO_Meter) / (float)(boss.GetComponent<Boss>().maxYellingO_Meter));
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
    void DrawYellingOMeter(float value)
    {
        //print("value : " + value);
        GUI.DrawTexture(new Rect(10, 100, 200 * value, 30), ki);
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
