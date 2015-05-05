using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Minds;
using RAIN.Serialization;
using RAIN.Motion;

public class Boss : MonoBehaviour {


	//Vector2 position; //peut utiliser son transform


	float vitesseDep;

	//GameObject[] boxies;


	float jaugeEngueulage; //se remplit quand on appuie sur le boss.
	public int vitesseJauge = 2; //se remplit quand on appuie sur le boss.

	
	//float timer = 0;

	bool charge = false;

	Transform actionArea;
	private RAIN.Memory.BasicMemory tMemory;

	// Use this for initialization
	void Start () {
		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;


		actionArea = transform.GetChild (0);        
	
        //navComponent = this.transform.GetComponent <NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButton(0))
		{

			//Vector3 pos;


			if (charge)
			{
                if (!actionArea.gameObject.active)
                    actionArea.gameObject.SetActive(true);


				jaugeEngueulage += Time.deltaTime*vitesseJauge;
				
		
				
				actionArea.localScale = new Vector3(jaugeEngueulage,actionArea.localScale.y,jaugeEngueulage);
			}
			//else 
			//{
				//pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//pos.y = transform.position.y;

				//navComponent.SetDestination (pos);

//				AI.Motor.MoveTo (pos);

			//}



			//print ("mouseDown: "+ pos);

		}
		

	}

	//public GameObject setBoxies (GameObject[] lesBoxies)
	//{
	//	boxies = lesBoxies;
	//}




	void OnMouseDown () 
	{
		print ("START CHARGE ");
		charge = true;
		tMemory.SetItem("charge",true);

	}

	void OnMouseUp ()
	{
		charge = false;
		tMemory.SetItem("charge",false);

		//actionArea.localScale.z = jaugeEngueulage;




		if (jaugeEngueulage >= 2)
		{


			actionArea.GetComponent<jaugeEngueulage>().Engueule();


			print ("HATARAKE!! ");


		}
        actionArea.gameObject.SetActive(false);
		actionArea.localScale = new Vector3(0.8f,actionArea.localScale.y,0.8f);

		print ("END CHARGE ");

		//ResetTimer
		jaugeEngueulage = 0;

	}
	
}



