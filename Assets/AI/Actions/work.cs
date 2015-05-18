using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class work : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        
		//Set de bools, sert a rien pour l'instant
		ai.WorkingMemory.SetItem("working", true);
        ai.WorkingMemory.SetItem("enDeplacement", false);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
       //Reduction de la motivation
        ai.WorkingMemory.SetItem("motivation", ai.WorkingMemory.GetItem<int>("motivation") - ai.Body.GetComponent<Employe>().feignantise);

   		//Finis de travailler quand la motivation est 0
        if (ai.WorkingMemory.GetItem<int>("motivation") <= 0)
        {
			//Libere la place (encore en test)
            ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Box>().occupe = false;
            return ActionResult.SUCCESS;
        }

        GameManager gm =GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.objectiveCompletion = gm.objectiveCompletion+0.1f;
		//Continue a travailler
         return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("working", false);
        base.Stop(ai);
    }
}