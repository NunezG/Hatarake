using UnityEngine;
using System.Collections;

public class BreakableFurniture : MonoBehaviour {
    public bool broken = false;
	private int resistance = 5;
    private int damage = 0;

	void Update () {

        if (damage == resistance) {
			transform.parent.GetComponentInChildren<SpriteRenderer> ().color = new Color (1.0f, 0.0f, 0.0f);
			broken = true;
		}

        else
		{
			transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
			broken = false;
		}
	}

    public void Hit()
    {
		if(damage < 5)
        	damage++;
    }

	public void Repair()
	{
		damage = 0;
	}

}
