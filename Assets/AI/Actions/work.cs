using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class work : RAINAction
{
    float motivation;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        motivation = ai.Body.GetComponent<Employe>().data.motivation;
		//Set de bools, sert a rien pour l'instant
		ai.WorkingMemory.SetItem("working", true);
        ai.WorkingMemory.SetItem("enDeplacement", false);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
       //Reduction de la motivation
        motivation = motivation - Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation;
   		//Finis de travailler quand la motivation est 0
        if (motivation <= 0)
        {
			//Libere la place (encore en test)
            ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Box>().occupe = false;
            return ActionResult.SUCCESS;
        }

		//Continue a travailler
         return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        ai.WorkingMemory.SetItem("motivation", motivation);
        ai.Body.GetComponent<Employe>().data.motivation = motivation;

		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("working", false);
        base.Stop(ai);
    }
}