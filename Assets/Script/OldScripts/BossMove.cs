using UnityEngine;
using RAIN.Core;
using RAIN.Minds;
using RAIN.Serialization;
//using System.Collections;
using RAIN.Navigation.Targets;
//using System.Collections;


[RAINSerializableClass]
public class BossMove : RAINMind
{

	float vitesseDep;
	float jaugeEngueulage; //se remplit quand on appuie sur le boss.
	Vector3 pos;
    
    //float timer = 0;
	//bool charge = false;
	//Transform actionArea;

    //[RAINSerializableField]
	//private GameObject boxDeTravail;

	//[RAINSerializableField]
	//private GameObject chill;
	
	//[RAINSerializableField]
	//private Transform _target;
	RAIN.Memory.BasicMemory tMemory;
	bool charge;
    Collider[] colliders;

	// Use this for initialization
	public override void Start()
	{
		tMemory = AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tMemory.RemoveItem("lookTarget");

	}



//	[RAINSerializableField]
	//private float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public override void Think()
	{
		charge = (bool)tMemory.GetItem("charge");

       
		//Vector3 pos;
		if (!charge)
		{


			if(Input.GetMouseButton(0))
			{

			//	timer += Time.deltaTime;
				
				
				
			//	actionArea.localScale = new Vector3(timer,actionArea.localScale.y,timer);
		//	}
		//	else 
		//	{
				pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//pos.y = transform.position.y;
                pos.y = 2;
				//navComponent.SetDestination (pos);

                colliders = Physics.OverlapSphere(pos, 1f /* Radius */);
                Debug.Log("pospospospospospospospospos: " + pos);

                Debug.Log("COLLIDERRRSRSRSRSRRS: " + colliders.Length);

                if (colliders != null && colliders.Length > 0){
                Debug.Log("COLLIDER: " + colliders[0].name);
                Debug.Log("COLLIDER colliders[0].tag: " + colliders[0].tag);

                }
              
			}

            if (pos != null && pos != AI.Body.transform.position && (colliders == null || (colliders != null && (colliders.Length == 0 || colliders[0].tag == "Nav"))))
            {
                //targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.MountPoint = target.transform;
                //targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.TargetName = "NavTarget";

                //AI.Motor.MoveTo (targ.transform.GetChild (0).position);
                //	AI.Motor.MoveTo (targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.Position);
                AI.Motor.MoveTo(pos);
            }

            if (pos == AI.Body.transform.position)
            {
                Debug.Log("RESET DE POSITIONNNN: " + pos);
               // pos = null;

            }

			//print ("mouseDown: "+ pos);
			
		}

	
		//Debug.Log ("boxxboxxboxxboxx: "+ boxx.name);

		//print ("dd");

		// create target
		//GameObject target = (GameObject)Instantiate(Resources.Load("prefab/target2"));
		//target.transform.position = new Vector3(22, 1, 6);

		//_target = targ.transform.GetChild (0);

	//	Debug.Log ("_target: "+ _target.name);


	}

}