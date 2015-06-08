using UnityEngine;
using System.Collections;

public class BreakableFurniture : MonoBehaviour {
    public bool broken = false;
	private int resistance = 5;
    private int damage = 0;
    private Vector3 breakTarget;


    void Start(){

        if (transform.parent.FindChild("breakPos") != null)
        {
            breakTarget = transform.parent.FindChild("breakPos").position;
        }
        else breakTarget = transform.parent.GetComponentInChildren<Box>().transform.position;

    }

	void Update () {
       // if (GameManager.instance.boss.GetComponent<Boss>().getTarget() == breakTarget)
     //   {

            


      //  }
       //
	}

    public void Hit()
    {
        GameManager.instance.boss.GetComponent<Boss>().action();
        damage++;

        if (damage >= resistance)
        {
            transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
            broken = true;
        }
    }

	public bool Repair()
	{
        damage -= Random.Range(0, resistance);

        if (damage <= 0)
        {
            transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
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

           // GameManager.instance.boss.transform.LookAt(transform.parent.FindChild("lookAt"));
        }

    }
}
