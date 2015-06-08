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
    public float time = 0;
    public float poutPoutAmplitude = 0.1f;
    public float poutPoutFrequence = 1f;
    void Start()
    {

    }

    void Update()
    {
        if (boss == null) {
            boss = GameObject.FindGameObjectWithTag("Boss");
        }
        else
        {
            //print("boss name : "+boss.name);
			DrawYellingOMeter();
        }
    }


    void OnGUI()
    {

        if (!NavMesh.isNavMeshDone)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
        }
        else if(GameManager.instance.workingIsActuallyUsefull)
        {
            DrawProgressObjective(GameManager.instance.objectiveCompletion / GameManager.instance.levelObjective);
            if (GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies() != 0)
                DrawNumberOfWorkingEmploye(GameManager.instance.GetComponent<CharacterManager>().GetNumberOfWorkingBoxies(), GameManager.instance.GetComponent<CharacterManager>().GetTotalNumberOfBoxies());
        }
        else
        {
        }
    
    }

	
    void DrawProgressObjective(float progress )
    {
        GUI.DrawTexture(new Rect(10, 10, 200, 30), progressBackground);
        GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForeground);
		   // if (progress > 1.0) Destroy (this);
    }

    void DrawYellingOMeter()
    {
		int valueQi = (int) ( (boss.GetComponent<Boss> ().yellingO_Meter / (float)boss.GetComponent<Boss> ().maxYellingO_Meter )*8 );
		if(valueQi > 8) valueQi = 8;
        if (valueQi == 8)
        {
            float scalePoutPout = (Mathf.Sin(time * poutPoutFrequence) + 1) * poutPoutAmplitude;
            qiBar.gameObject.transform.localScale = new Vector3(1 + scalePoutPout, 1 + scalePoutPout, 1 + scalePoutPout);
            time = time + Time.deltaTime;
        }
        else {
            time = 0;
            qiBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        //print("valueQi" + valueQi + ", ");
		qiBar.GetComponent<Image> ().sprite = qiBarSteps[valueQi];
		//Debug.Log (valueQi);
    }

    void DrawNumberOfWorkingEmploye(int working,int total)
    {
        for (int i = 0; i < working; i++)
        {
            GUI.DrawTexture(new Rect(10+20*i, 200, 10, 10), progressForeground);
        }
    }
}
