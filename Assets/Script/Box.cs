using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public bool occupe = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter(Collider other)
	{
		//print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);


	}

	void OnTriggerStay(Collider other)
	{

		//print("BOX COLLISION "+other.tag);



		if (other.tag == "Employe" && other.GetComponent<Employe>().getBox()==this.gameObject) 
		{



			//print("START WORKING");



			//other.GetComponentInChildren<Employe>().auTravail = true;

			other.GetComponent<Employe>().Travaille();
				//gameObject.start () as Employe;
						
		}


	}


}
