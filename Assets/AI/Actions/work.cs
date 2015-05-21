using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class work : RAINAction
{
    float motivation;
    GameManager gm;
    public override void Start(RAIN.Core.AI ai)
    {
        //Commence a travailler
        base.Start(ai);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        motivation = ai.Body.GetComponent<Employe>().data.motivation;
		//Set de bools, sert a rien pour l'instant
		//ai.WorkingMemory.SetItem("working", true);
        
        //bouge plus
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ;
        

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
       //Reduction de la motivation
        motivation = motivation - Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation;
   		
        //Finis de travailler quand la motivation est 0
        if (motivation <= 0)
        {
            return ActionResult.SUCCESS;
        }

        gm.objectiveCompletion = gm.objectiveCompletion + Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseTravail;
		//Continue a travailler
         return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        GameObject target;
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        //Remets la motivation
        ai.WorkingMemory.SetItem("motivation", motivation);
        ai.Body.GetComponent<Employe>().data.motivation = motivation;

        //libère l'espace
        if (target.CompareTag("WorkHelp"))
        {
            target.GetComponent<Box>().occupe = false;
        }
        ai.Body.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezePositionY ;

        base.Stop(ai);
    }
}