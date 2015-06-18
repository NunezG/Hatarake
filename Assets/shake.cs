using UnityEngine;
using System.Collections;

public class shake : MonoBehaviour {
    public int shakingDuration = 50;
    public float shakeMagnitude = 2;
    public Quaternion initialRotate;
    public Vector3 initialPosition, initialScale;

    public bool shaking;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialRotate = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Shakes the camera for a certain amount of time
   public  IEnumerator ShakeMyBooty()
    {
        int rotation = 1;
        int rotationTemp = 0;

        while (shakingDuration > 0)
        {
            if (rotationTemp == shakeMagnitude)
            {
                this.transform.localRotation = initialRotate;
                rotationTemp = 0;
                rotation = -rotation;
            }

            transform.Rotate(0, 0, rotation);
            shakingDuration--;

            rotationTemp++;
            yield return true;
        }

        this.transform.position = initialPosition;
        this.transform.localScale = initialScale;
        this.transform.localRotation = initialRotate;
    }
}
