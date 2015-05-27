using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public bool assigne = false;
    public bool occupe = false;
	public bool broken = false;
	private float tempBroken = 0;
	public float timeBroken = Random.Range(5,10);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (tag == "WorkHelp") {
			tempBroken += Time.deltaTime;

			if (tempBroken >= timeBroken/2) 
			{
				transform.parent.GetComponentInChildren<SpriteRenderer> ().color = new Color (0.5f, 0.1f, 0.1f);

			}
			

			if (tempBroken >= timeBroken) {
				transform.parent.GetComponentInChildren<SpriteRenderer> ().color = new Color (1.0f, 0.0f, 0.0f);
				broken = true;

			}
		}
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
