using UnityEngine;
using System.Collections;

public class TelephoneIsRinging : MonoBehaviour {

    public AudioSource ringring;
    public AudioSource pickUp;
    public GameObject onPickUpTarget,targetToFace;
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
            GameManager.instance.tutoArrow.gameObject.SetActive(false);
            if (!GameManager.instance.tutoIsOn)
            {/*
                GameManager.instance.hiringTime = true;
                GameManager.instance.CalculateNumberOfEmployeeToHire();*/
                Employe.suicideLock = true;
                GameManager.instance.tutoArrow.gameObject.SetActive(false);
                GameManager.instance.cameraController.FollowEmployee(GameManager.instance.boss,1.5f,0);
                GameManager.instance.activateNextMissionButton();
            }
            else
            {
                GameManager.instance.activateHiringRound();
            }
        }

        GameManager.instance.boss.GetComponent<Boss>().setTarget(onPickUpTarget.transform.position);
        GameManager.instance.boss.GetComponent<Boss>().faceTarget(targetToFace.transform.position);
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
