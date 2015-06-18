using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {

    GameObject EncartsLogos;

    public float fadeSpeed = 0.2f;          // Speed that the screen fades to and from black.
    public bool sceneEnding = false;      // Whether or not the scene is still fading in.
    Color tempColor;
    public MovieTexture waitScreen;
    AudioSource audio;
    float temp = 0;
    float startFading;
   
    void Awake()
    {
        startFading = waitScreen.duration / 10.0f;
    }
    
	// Use this for initialization
	void Start () {
        waitScreen.Play();
        GetComponentInChildren<AudioSource>().Play();
        tempColor = GUI.color;
       // Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // If the scene is starting...       

        //EncartsLogos.SetActive(true);

       // new WaitForSeconds(10);

       // StartCoroutine(fondu(Color.white, 10));
	}

    void OnGUI()
    {
        if (temp >= startFading)
            sceneEnding = true;

        if (sceneEnding)
            // ... call the StartScene function.
            EndScene();


        if (waitScreen.isPlaying)
        {

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
           // GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);

        }
        temp += Time.deltaTime;
    }


    void StartScene()
    {
        // Fade the texture to clear.
       // FadeToClear();

        // If the texture is almost clear...
        if (GUI.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
         //   GUI.color = Color.clear;
           // GUI.enabled = false;

            // The scene is no longer starting.
           // sceneStarting = false;
        }
    }

    public void EndScene()
    {
        // Make sure the texture is enabled.
       // GUI.enabled = true;

        // Start fading towards black.
        FadeToClear();


        if (GUI.color.a <= 0.05f)
        {
           // Time.timeScale = 1;

        }


        // If the screen is almost black...
      //  if (GUI.color.a >= 0.95f)
            // ... reload the level.
           // Application.LoadLevel(0);
    }


    void FadeToClear()
    {
        // Set the texture so that it is the the size of the screen and covers it.
       // GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), new Texture());
        // Lerp the colour of the texture between itself and transparent.

        tempColor = Color.Lerp(tempColor, Color.clear, fadeSpeed * Time.deltaTime);

        GUI.color = tempColor;

        

    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        GUI.color = Color.Lerp(GUI.color, Color.black, fadeSpeed * Time.deltaTime);
    }



/*

    IEnumerator fondu(Color couleur, int temps)
    {
        while (GUI.color != couleur)
        {
          //  GUI.color.a = alphaLevel;
        }
        
        yield return null;
    }

 */ 
}
