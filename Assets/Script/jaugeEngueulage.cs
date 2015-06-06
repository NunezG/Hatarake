using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class jaugeEngueulage : MonoBehaviour {

	private List<GameObject> employes = new List<GameObject>();
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

    public List<GameObject> getEmployesJauge()
    {
        return employes;
    }

    public void clearEmployesJauge()
    {
        employes.Clear(); 
    }



	
	void OnTriggerStay(Collider other)
	{
		
		//print("BOX COLLISION "+other.tag);
		
		
		
		
	}

}
