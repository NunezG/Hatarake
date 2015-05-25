using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class selectTarget : RAINAction
{
 // public Expression target;
    GameObject target;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
       float motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
	}
	
	public override ActionResult Execute(RAIN.Core.AI ai)
    {   
        if (ai.WorkingMemory.GetItem<bool>("suicidaire"))
        {
            int rdmIndex= UnityEngine.Random.Range(0, Employe.suicide.Count);

            target = Employe.suicide[rdmIndex];
            return ActionResult.SUCCESS;

        }
        else if (!ai.WorkingMemory.GetItem<bool>("auTravail"))
        {
             int pos = Random.Range(0, 4);

            //*1/4 de chances de bosser sur une photocopieuse
             if (pos == 0 && Employe.emptyChill.Count != 0)
             {
                 //*cherche une place vide pour glander
                 pos = Random.Range(0, Employe.emptyChill.Count);

                 target = Employe.emptyChill[pos];
                 Employe.emptyChill.RemoveAt(pos);
                 return ActionResult.SUCCESS;

             }
           //*Si pas de place il retourne a son Box pour se connecter sur facebook
           target = ai.Body.GetComponent<Employe>().boxDeTravail;
           return ActionResult.SUCCESS;
        }
        else 
        {
            //*Si motivation cherche une place vide pour bosser
            int pos = Random.Range(0, 4);

            //*1/4 de chances de bosser sur une photocopieuse
			if (!ai.WorkingMemory.GetItem<bool>("hatarake") && pos == 0)
            {
                if (Employe.emptyWorkingHelp.Count != 0)
                {
                    pos = Random.Range(0, Employe.emptyWorkingHelp.Count);

                    target = Employe.emptyWorkingHelp[pos];
                    Employe.emptyWorkingHelp.RemoveAt(pos);
                    return ActionResult.SUCCESS;
                }
            } else
            {
				if (ai.WorkingMemory.GetItem<bool>("hatarake"))
				{
					ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("scaredSpeed");
				}
                //*3/4 de chances de bosser dans son Box
                target = ai.Body.GetComponent<Employe>().boxDeTravail;

                return ActionResult.SUCCESS;
            }
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
		ai.WorkingMemory.SetItem<bool>("hatarake", false);
		//ai.WorkingMemory.SetItem<bool>("suicidaire", false);
        //*set des variables après succès
        if (target != ai.WorkingMemory.GetItem<GameObject>("myTarget"))
        {
            ai.WorkingMemory.SetItem("enDeplacement", true);
            ai.WorkingMemory.SetItem("myTarget", target);
        }
        base.Stop(ai);
    }
}