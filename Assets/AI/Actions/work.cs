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
     Animator animator;

	public override void Start(RAIN.Core.AI ai)
    {
        animator = ai.Body.GetComponent<Animator>();
        //Commence a travailler
        base.Start(ai);
        motivation = ai.Body.GetComponent<Employe>().data.motivation;
		//Set de bools, sert a rien pour l'instant
		//ai.WorkingMemory.SetItem("working", true);
        
        //bouge plus
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
		target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        if (target == ai.Body.GetComponent<Employe>().boxDeTravail)
        {
            animator.SetBool("typing", true);
            animator.SetBool("sit", true);
        }
       // else ai.Body.transform.FindChild("top").GetComponent<SpriteRenderer>().sprite =  new Sprite();
	}

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

			//Reduction de la motivation
			motivation -= Time.deltaTime * ai.Body.GetComponent<Employe> ().data.vitesseDemotivation;
            ai.Body.GetComponent<Employe>().data.motivation = motivation;

			//Finis de travailler quand la motivation est 0
			if (motivation <= 0) {
                ai.Body.GetComponent<Employe>().data.motivation = 0;
				ai.WorkingMemory.SetItem ("auTravail", false);
				return ActionResult.SUCCESS;
			}

            GameManager.instance.employeeWork( Time.deltaTime , ai.Body.GetComponent<Employe>().data.vitesseTravail );
			//Continue a travailler
			return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        
        //Remets la motivation
       // ai.WorkingMemory.SetItem("motivation", motivation);

        //libère l'espace
        if (target.CompareTag("WorkHelp"))
        {
            //target.GetComponent<Box>().occupe = false;
            Employe.emptyWorkingHelp.Add(target);
        }
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

		ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("normalSpeed");
        animator.SetBool("typing", false);
        animator.SetBool("sit", false);
		base.Stop(ai);
    }
}