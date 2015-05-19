using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {


    public Texture progressBackground;
    public Texture progressForground;

     

    void OnGUI()
    {

       // print("ONGUI ");
        DrawProgress(GetComponent<GameManager>().objectiveCompletion / 1000.0f);
       
    }



 
void DrawProgress(float progress )
{
    GUI.DrawTexture(new Rect(10, 10, 200, 30), progressBackground);
    GUI.DrawTexture(new Rect(10, 10, 200 * progress, 30), progressForground);
}
}
