using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FollowScript : MonoBehaviour {

    public GameObject target;
    public Image image;
    public Color startColor,middleColor,endColor;
    public SolarClockManager solarClock;
	// Use this for initialization
    void Start()
    {
        this.transform.position = target.transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;
        if (solarClock.time < solarClock.endOfTime / 2)
        {
            image.color = Color.Lerp(startColor, middleColor, solarClock.time / (solarClock.endOfTime / 2));
        }
        else if (solarClock.time > solarClock.endOfTime / 2)
        {
            image.color = Color.Lerp(middleColor, endColor, (solarClock.time - solarClock.endOfTime / 2) / (solarClock.endOfTime / 2));
        }
	}
}
