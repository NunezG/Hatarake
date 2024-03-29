﻿using UnityEngine;
using RAIN.Core;
using RAIN.Minds;
using RAIN.Serialization;
//using System.Collections;
using RAIN.Navigation.Targets;

[RAINSerializableClass]
public class RAINMove : RAINMind
{
	//[RAINSerializableField]
	//private GameObject boxDeTravail;

	//[RAINSerializableField]
	//private GameObject chill;
	
	//[RAINSerializableField]
	//private Transform _target;


//	[RAINSerializableField]
	//private float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public override void Think()
	{

		RAIN.Memory.BasicMemory tMemory = AI.WorkingMemory as RAIN.Memory.BasicMemory;		
		GameObject targ = tMemory.GetItem("myTarget") as GameObject;

		//Debug.Log ("boxxboxxboxxboxx: "+ boxx.name);

		//print ("dd");

		// create target
		//GameObject target = (GameObject)Instantiate(Resources.Load("prefab/target2"));
		//target.transform.position = new Vector3(22, 1, 6);

		//_target = targ.transform.GetChild (0);

	//	Debug.Log ("_target: "+ _target.name);

		if (targ != null) {
			//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.MountPoint = target.transform;
			//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.TargetName = "NavTarget";

			//AI.Motor.MoveTo (targ.transform.GetChild (0).position);
		//	AI.Motor.MoveTo (targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.Position);
			AI.Motor.MoveTo (targ.transform.position);
		}
	}

}