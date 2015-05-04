using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class jaugeEngueulage : MonoBehaviour {

	List<GameObject> employes = new List<GameObject>();
	//GameObject employes;

	// Use this for initialization
	void Start () {
		//employes = new Array
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	void OnTriggerEnter(Collider other)
	{
		//print("SOnTriggerEnterOnTriggerEnterOnTriggerEnter"+other.name);

		if (other.tag == "Employe") 
		{		
			
			employes.Add(other.gameObject);
			//other.GetComponent<Employe>().Engueule();
			
		}
		
		
	}
	
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Employe") 
		{		
			
			employes.Remove(other.gameObject);

			
		}
		
		
	}

	public void Engueule()
	{
		print ("ENGUEULE: " + employes.Count);

		foreach(GameObject emp in employes)
		{

			emp.GetComponent<Employe>().Engueule();

		}
		employes.Clear ();

		//other.GetComponent<Employe>().Engueule();


	}

	
	void OnTriggerStay(Collider other)
	{
		
		//print("BOX COLLISION "+other.tag);
		
		
		
		
	}

}
