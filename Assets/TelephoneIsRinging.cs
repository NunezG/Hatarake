using UnityEngine;
using System.Collections;

public class TelephoneIsRinging : MonoBehaviour {

    public AudioSource ringring;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.victory)
        {
            if (!ringring.isPlaying) ringring.Play();
        }
	
	}


    void OnTriggerEnter(Collider other)
    {
        //print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);
        if (GameManager.instance.victory)
        {
            if (other.tag == "Boss")
            {
                print(this.name + " TRIGGERED BY " + other.name + " BURN HIM !");
                //GameManager.instance.activateHiringRound();
                ringring.Stop();
                GameManager.instance.activateGloriousVictory();

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boss")
        {

        }

    }
}
