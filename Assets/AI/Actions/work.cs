using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;


[RAINAction]
public class work : RAINAction
{
    EmployeeData employe;
	GameObject target;
     Animator animator;

	public override void Start(RAIN.Core.AI ai)
    {
        animator = ai.Body.GetComponent<Animator>();
        //Commence a travailler
        base.Start(ai);
        employe = ai.Body.GetComponent<Employe>().data;
		//Set de bools, sert a rien pour l'instant
		//ai.WorkingMemory.SetItem("working", true);
        
        //bouge plus
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	    
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        if (target != null)
        {
            target.GetComponent<InteractWithEmployee>().glande = false;

            for (int i = 0; i < target.GetComponent<InteractWithEmployee>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<InteractWithEmployee>().animatorStates[i], true);
            }
        }
       // else ai.Body.transform.FindChild("top").GetComponent<SpriteRenderer>().sprite =  new Sprite();
	}

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

			//Reduction de la motivation
            employe.motivation -= Time.deltaTime * employe.vitesseDemotivation;
            //ai.Body.GetComponent<Employe>().data.motivation = motivation;

			//Finis de travailler quand la motivation est 0
            if (employe.motivation <= 0)
            {
                employe.motivation = 0;
				ai.WorkingMemory.SetItem ("auTravail", false);
				return ActionResult.SUCCESS;
			}

            GameManager.instance.employeeWork(Time.deltaTime, employe.vitesseTravail);
			//Continue a travailler
			return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        
        //Remets la motivation
       // ai.WorkingMemory.SetItem("motivation", motivation);

        //libère l'espace
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("normalSpeed");

        if (target != null)
        {
            if (target.CompareTag("WorkHelp"))
            {
                //target.GetComponent<Box>().occupe = false;
                Employe.emptyWorkingHelp.Add(target);
            }

            for (int i = 0; i < target.GetComponent<InteractWithEmployee>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<InteractWithEmployee>().animatorStates[i], false);
            }
        }
		base.Stop(ai);
    }
}