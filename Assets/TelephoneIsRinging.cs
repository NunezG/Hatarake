using UnityEngine;
using System.Collections;

public class TelephoneIsRinging : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other)
    {
        //print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);

        if (other.tag == "Boss")
        {
            print(this.name +" TRIGGERED BY " + other.name +" BURN HIM !");
            GameManager.instance.activateHiringRound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boss")
        {

        }

    }
}
