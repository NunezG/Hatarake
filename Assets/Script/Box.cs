﻿using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public bool assigne = false;
    //public bool occupe = false;

    public string[] animatorStates;

    public SignType signToEmitWork;
    public SignType signToEmitChill;

    public AudioSource soundToEmitWork;
    public AudioSource soundToEmitChill;

    public bool glande;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter(Collider other)
	{
		//print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);
		
		if (other.tag == "Employe" && other.GetComponent<Employe>().getBox()==this.gameObject) 
		{		
			//StartCoroutine(other.GetComponent<Employe>().Travaille());
			
		}


	}


	void OnTriggerExit(Collider other)
	{



	}

	void OnTriggerStay(Collider other)
	{

		//print("BOX COLLISION "+other.tag);




	}


}