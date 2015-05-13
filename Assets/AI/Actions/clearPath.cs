using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class clearPath : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
	//	Debug.Log("CLEARNEDE" );


		if (ai.Navigator.CurrentPath != null) {
	//		Debug.Log ("CLEARNEDEEEEEEEEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSSSSSSS" + ai.Navigator.CurrentPath.IsValid);
//			Debug.Log ("CLEARNEDEEEEEEE22222" + ai.Navigator.CurrentPath.IsPartial);

			//ai.Reset (ai.Navigator.CurrentPath.);

			ai.Navigator.CurrentPath = null;


		}
		//ai.Navigator.Start ();
		//tNav.RestartPathfindingSearch();

        return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}