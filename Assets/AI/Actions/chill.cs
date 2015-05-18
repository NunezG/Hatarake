using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class chill : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
			//Reduction de la fatigue si existante
            if (ai.WorkingMemory.GetItem<int>("fatigue") > 0)
                ai.WorkingMemory.SetItem("fatigue", ai.WorkingMemory.GetItem<int>("fatigue") - (int)ai.Body.gameObject.GetComponent<Employe>().vitesseFatigue);
          
			//Augmente la motivation
        	ai.WorkingMemory.SetItem("motivation", ai.WorkingMemory.GetItem<int>("motivation") + ai.Body.gameObject.GetComponent<Employe>().data.effetRepos);
           
			//Si motivation Max, cesse de glander
            if (ai.WorkingMemory.GetItem<int>("motivation") >= ai.WorkingMemory.GetItem<int>("motivationMax"))
            {	
				//Libere la place (encore en test)
                ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Repos>().occupe = false;
                 return ActionResult.SUCCESS;
            }

			//Continue a glander
            return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}