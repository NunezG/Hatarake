using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    GameObject EncartsLogos;

    public float fadeSpeed = 0.4f;          // Speed that the screen fades to and from black.
    public bool sceneEnding = false;      // Whether or not the scene is still fading in.
    Color tempColor;
    public MovieTexture waitScreen;
    AudioSource audio;
    float temp = 0;
    public float startFading = 20;
    public bool sceneStarting = false;      // Whether or not the scene is still fading in.
    GameObject spawn;

    void Awake()
    {
        spawn = GameObject.Find("spawnBoss");
    }
    
	// Use this for initialization
	void Start () {
       
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

    public void startButton()
    {
        GameManager.instance.boss.transform.Find("hatarake_strong").GetComponent<AudioSource>().Play();
        
        StartCoroutine(GameObject.Find("MenuPanel").GetComponentInChildren<shake>().ShakeMyBooty());
        sceneStarting = true;
    }

    void OnGUI()
    {
        if (sceneStarting && GameObject.Find("MenuPanel").GetComponentInChildren<shake>().shakingDuration == 0 && GameObject.Find("MenuPanel").GetComponentInChildren<shake>().shakingDuration == 0 && !GameManager.instance.boss.transform.Find("hatarake_strong").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("MenuPanel").SetActive(false);
            StartScene();
            GameManager.instance.InitGame();
            sceneStarting = false;
        }

       // if (sceneStarting)
            // ... call the StartScene function.
        //    StartScene();

        if (waitScreen.isPlaying)
        {
            if (sceneEnding)
                // ... call the StartScene function.
                EndScene();

            else if (Input.GetMouseButton(0) || temp >= startFading)
            {
                sceneEnding = true;
            }

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen, ScaleMode.StretchToFill, false);
            // GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), waitScreen);
            temp += Time.deltaTime;
        }
    }

    public void StartScene()
    {

        Debug.Log("starrrt");

        // Fade the texture to clear.
       // FadeToClear();

        // If the texture is almost clear...
       // if (GUI.color.a <= 0.05f)
       // {
            // ... set the colour to clear and disable the GUITexture.
           // GUI.color = Color.clear;
           // GUI.enabled = false;
            waitScreen.Play();
       

            // The scene is no longer starting.
          //  sceneStarting = false;
            
       // }
    }

    public void EndScene()
    {
        // Make sure the texture is enabled.
       // GUI.enabled = true;

        // Start fading towards black.
        FadeToClear();


        if (GUI.color.a <= 0.05f)
        {
            GameManager.instance.tutoFirstButton.GetComponent<Button>().interactable = true;
            waitScreen.Stop();
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
