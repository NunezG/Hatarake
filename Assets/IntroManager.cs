using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {

    GameObject EncartsLogos;

    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.

    void Awake()
    {
       
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();



        //EncartsLogos.SetActive(true);

       // new WaitForSeconds(10);

       // StartCoroutine(fondu(Color.white, 10));
	}

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

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
        GUI.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (GUI.color.a >= 0.95f)
            // ... reload the level.
            Application.LoadLevel(0);
    }


    void FadeToClear()
    {
        // Set the texture so that it is the the size of the screen and covers it.
       // GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), new Texture());
        // Lerp the colour of the texture between itself and transparent.
        GUI.color = Color.Lerp(GUI.color, Color.red, fadeSpeed * Time.deltaTime);
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
