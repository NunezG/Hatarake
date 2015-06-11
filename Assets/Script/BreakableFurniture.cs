using UnityEngine;
using System.Collections;

public class BreakableFurniture : MonoBehaviour {
    public bool broken = false;
	private int resistance = 5;
    private int damage = 0;
    private Vector3 breakTarget;
    public bool shaking = false;
    public int shakingDuration = 5;
    public float shakeMagnitude = 1;
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


        this.initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.initialScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        this.initialRotate = new Quaternion(this.transform.localRotation.x, this.transform.localRotation.y, this.transform.localRotation.z, this.transform.localRotation.w);

    }

	void Update () {
       // if (GameManager.instance.boss.GetComponent<Boss>().getTarget() == breakTarget)
     //   {
        //  }
       //
        ShakeMyBooty();


	}
    //Shakes the camera for a certain amount of time
    public void ShakeMyBooty()
    {
        if (shaking && shakingDuration>0)
        {
            transform.Rotate(0, 0, Random.Range(-shakeMagnitude, shakeMagnitude) * 1);
            shakingDuration--;
        }

        else
        {
            this.transform.position = initialPosition;
            this.transform.localScale = initialScale;
            this.transform.localRotation = initialRotate;
            shaking = false;
        }
    }
    public void Hit()
    {
        GameManager.instance.boss.GetComponent<Boss>().action();
        damage++;
        shaking = true;
        shakingDuration = 20;
        if (damage >= resistance)
        {
            transform.parent.GetComponentInChildren<SpriteRenderer>().sprite = brokenSprite;
           // transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f);

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
           // transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
          
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

            // tMemory.SetItem("sabotage", true);
            //  tMemory.SetItem("enDeplacement", true);
            // tMemory.SetItem<GameObject>("target", colliders[0].gameObject);
        }
        else
        {
            GameManager.instance.boss.GetComponent<Boss>().setTarget(breakTarget);
            GameManager.instance.boss.GetComponent<Boss>().faceTarget(transform.parent.FindChild("lookAt").position);
        }

    }
}
