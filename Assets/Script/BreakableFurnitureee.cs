using UnityEngine;
using System.Collections;

public class BreakableFurnitureee : BreakableFurniture
{
 
    void Start(){
       // shakeMagnitude = 1;
        if (transform.parent.FindChild("breakPos") != null)
        {
            breakTarget = transform.parent.FindChild("breakPos").position;
        }
        else breakTarget = transform.parent.GetComponentInChildren<InteractWithEmployee>().transform.position;


        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialRotate =transform.localRotation;
    }

	void Update () {

	}

    //Shakes the camera for a certain amount of time
    IEnumerator ShakeMyBooty()
    {
        int rotation = 1;
        int rotationTemp = 0;

        while(shakingDuration > 0)
        {
            if (rotationTemp == shakeMagnitude )
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

  
    public void Break()
    {
        damage = resistance;
        broken = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = brokenSprite;
    }
	

   
}
