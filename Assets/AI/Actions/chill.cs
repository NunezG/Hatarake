using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class chill : RAINAction
{
    float motivation;
    float fatigue;

    public override void Start(RAIN.Core.AI ai)
    {
        motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
        fatigue = (int)ai.Body.gameObject.GetComponent<Employe>().data.fatigue;

		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {      
			//Reduction de la fatigue si existante
            if (fatigue  > 0)
                fatigue = fatigue - Time.deltaTime*(int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;
          
			//Augmente la motivation
           motivation = motivation + Time.deltaTime*(int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;
           
			//Si motivation Max, cesse de glander
           if (motivation >= (int)ai.Body.gameObject.GetComponent<Employe>().data.motivationMax)
            {	
				//Libere la place (encore en test)
                 return ActionResult.SUCCESS;
            }

			//Continue a glander
            return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        ai.WorkingMemory.SetItem("fatigue", fatigue);
        ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
        ai.WorkingMemory.SetItem("motivation", motivation);
        ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;

        if (!ai.WorkingMemory.GetItem<GameObject>("myTarget").CompareTag("Box"))
        ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Repos>().occupe = false;

        ai.Body.GetComponent<Rigidbody>().constraints =RigidbodyConstraints.FreezePositionY ;

        base.Stop(ai);
    }
}