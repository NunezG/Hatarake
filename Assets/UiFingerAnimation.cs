using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiFingerAnimation : MonoBehaviour {

    public Sprite[] sprites;
    int currentIndex = 0;
    public float time=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if (time > 0.5)
        {
            swapSprite();
            time = 0;
        }
	
	}

    public void swapSprite(){
        if (currentIndex == 0) currentIndex = 1;
        else currentIndex = 0;
        this.gameObject.GetComponent<Image>().sprite = sprites[currentIndex];
    }
}
