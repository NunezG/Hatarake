using UnityEngine;
using System.Collections;

public class TelephoneIsRinging : MonoBehaviour {

    public AudioSource ringring;
    public AudioSource pickUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.ringingPhone)
        {
            if (!ringring.isPlaying) ringring.Play();
        }
	
	}

    public void PickUpPhone()
    {

    }

    void OnMouseDown() 
    {
        Vector3 distance =  GameManager.instance.boss.transform.position - this.transform.position;
        if (distance.magnitude < 10 && GameManager.instance.ringingPhone)
        {
            GameManager.instance.ringingPhone = false;
            ringring.Stop();
            pickUp.Play();
            if (!GameManager.instance.tutoIsOn)
            {/*
                GameManager.instance.hiringTime = true;
                GameManager.instance.CalculateNumberOfEmployeeToHire();*/

                GameManager.instance.cameraController.FollowEmployee(GameManager.instance.boss,1,0);
                GameManager.instance.activateNextMissionButton();
            }
            else
            {
                GameManager.instance.activateHiringRound();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {/*
        //print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);
        if (GameManager.instance.ringingPhone)
        {
            if (other.tag == "Boss")
            {
                print(this.name + " TRIGGERED BY " + other.name + " BURN HIM !");
                //GameManager.instance.activateHiringRound();
                GameManager.instance.ringingPhone = false;
                ringring.Stop();
                pickUp.Play();
                GameManager.instance.hiringTime=true ;

            }
        }*/
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Boss")
        {

        }

    }
}
