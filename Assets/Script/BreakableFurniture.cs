using UnityEngine;
using System.Collections;

public class BreakableFurniture : MonoBehaviour {
    public bool broken = false;
	private int resistance = 5;
    private int damage = 0;

	void Update () {

       
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

	public void Repair()
	{
		damage = 0;
        transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        broken = false;
	}

     void OnMouseDown() 
    {

        if ( GameManager.instance.boss.GetComponent<Boss>().getTarget() == transform.parent.GetComponentInChildren<Repos>().transform.position)
                {
                    
                        Hit();

                       // tMemory.SetItem("sabotage", true);
                      //  tMemory.SetItem("enDeplacement", true);
                       // tMemory.SetItem<GameObject>("target", colliders[0].gameObject);
                }
        else GameManager.instance.boss.GetComponent<Boss>().setTarget(transform.parent.GetComponentInChildren<Repos>().transform.position);
    }

}
