using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class work : RAINAction
{
    float motivation;
	GameObject target;


	public override void Start(RAIN.Core.AI ai)
    {
        //Commence a travailler
        base.Start(ai);
        motivation = ai.Body.GetComponent<Employe>().data.motivation;
		//Set de bools, sert a rien pour l'instant
		//ai.WorkingMemory.SetItem("working", true);
        
        //bouge plus
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ;
		target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

	}

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (!target.GetComponent<Box>().broken) {
			//Reduction de la motivation
			motivation = motivation - Time.deltaTime * ai.Body.GetComponent<Employe> ().data.vitesseDemotivation;
   		
			//Finis de travailler quand la motivation est 0
			if (motivation <= 0) {
				ai.WorkingMemory.SetItem ("auTravail", false);
				return ActionResult.SUCCESS;
			}

            GameManager.instance.employeeWork( Time.deltaTime , ai.Body.GetComponent<Employe>().data.vitesseTravail );
			//Continue a travailler
			return ActionResult.RUNNING;
		} else {
			motivation = motivation - Time.deltaTime * ai.Body.GetComponent<Employe> ().data.vitesseDemotivation * 20;
			return ActionResult.SUCCESS;
		}
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        



        //Remets la motivation
       // ai.WorkingMemory.SetItem("motivation", motivation);
        ai.Body.GetComponent<Employe>().data.motivation = motivation;

        //libère l'espace
        if (target.CompareTag("WorkHelp"))
        {
            //target.GetComponent<Box>().occupe = false;
            Employe.emptyWorkingHelp.Add(target);
        }
        ai.Body.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezePositionY ;

		ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("normalSpeed");

		base.Stop(ai);
    }
}