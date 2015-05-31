using UnityEngine;
using System.Collections;

public class BreakableFurniture : MonoBehaviour {
    public bool broken = false;
    public float resistance = Random.Range(50, 100);
    private float tempBroken = 0;
  //  public float timeBroken = Random.Range(5, 10);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

            tempBroken += Time.deltaTime;

            if (tempBroken >= resistance / 2)
            {
                transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.1f, 0.1f);
            }

            if (tempBroken >= resistance)
            {
                Break();
            }
        
	}

    public void Hit(float force)
    {
        tempBroken += force;
        if (tempBroken >= resistance)
        {
            Break();
        }
    }


    public void Break()
    {
        transform.parent.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
        broken = true;
    }

}
