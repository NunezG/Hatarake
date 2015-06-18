using UnityEngine;
using System.Collections;

public class BreakableFurnitureee : MonoBehaviour {
    public bool broken = false;
	private int resistance = 5;
    private int damage = 0;
    private Vector3 breakTarget;
   // public bool shaking = false;
    public int shakingDuration = 5;
    public float shakeMagnitude = 300000;
    public Vector3 initialPosition,initialScale;
    public Quaternion initialRotate;

    public Sprite normalSprite;
    public Sprite brokenSprite;

    void Start(){

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
       // if (GameManager.instance.boss.GetComponent<Boss>().getTarget() == breakTarget)
     //   {
        //  }
       //
       // ShakeMyBooty();
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

    public void Hit()
    {
        GameManager.instance.boss.GetComponent<Boss>().action();
        damage++;
        shakingDuration = 20;
        StartCoroutine(ShakeMyBooty());

        if (damage >= resistance)
        {
            transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = brokenSprite;

            if (transform.parent.GetComponentInChildren<ParticleSystem>()!= null)
            transform.parent.GetComponentInChildren<ParticleSystem>().Play();

            broken = true;
        }
    }

	public bool Repair()
	{
        damage -= Random.Range(0, resistance);

        if (damage <= 0)
        {
            transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
          
            if (transform.parent.GetComponentInChildren<ParticleSystem>() != null)
            transform.parent.GetComponentInChildren<ParticleSystem>().Stop();

            broken = false;
            return true;
        }
        return false;
	}

     void OnMouseDown() 
    {
        if (GameManager.instance.boss.GetComponent<Boss>().getTarget() == breakTarget)
        {
            Hit();
        }
        else
        {
            GameManager.instance.boss.GetComponent<Boss>().setTarget(breakTarget);
            GameManager.instance.boss.GetComponent<Boss>().faceTarget(transform.parent.FindChild("lookAt").position);
        }
    }
}
