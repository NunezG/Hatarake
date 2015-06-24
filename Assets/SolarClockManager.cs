using UnityEngine;
using System.Collections;

public class SolarClockManager : MonoBehaviour {

    public float time=0;
    public float endOfTime;
    public float angle;
    public float startingAngle, endingAngle, cycleAngularSize;
    public bool solarClockLock = false;
    public RectTransform rectTransform;
    Quaternion initRotation;
	// Use this for initialization
	void Start () {
        rectTransform = this.GetComponent<RectTransform>();
        angle = -startingAngle;
        cycleAngularSize = endingAngle - startingAngle;
        initRotation = rectTransform.rotation;

        rectTransform.Rotate(0, 0, angle);
        //rectTransform.rotation = new Quaternion(0, 0, 1, angle);
	}
	
	// Update is called once per frame
	void Update () {


        if (GameManager.instance.startSolarClock && !solarClockLock)
        {
            rectTransform.rotation = initRotation;

            time = time + Time.deltaTime;
            angle = angle - (Time.deltaTime / endOfTime) * cycleAngularSize;
            //rectTransform.rotation = new Quaternion(0, 0, 1, angle);

            rectTransform.Rotate(0,0,angle);

        }

        if (time >= endOfTime) { 
            solarClockLock = true;
            GameManager.instance.endOfDemo = true;
        }



	}
}
