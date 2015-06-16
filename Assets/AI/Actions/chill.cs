using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class chill : RAINAction
{
    EmployeeData employe;
    // float fatigue;
    GameObject target;
    //public Expression DemotivationSiCasse = new Expression();
    Animator animator;



    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        employe = ai.Body.gameObject.GetComponent<Employe>().data;
        animator = ai.Body.GetComponent<Animator>();


		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
     
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        if (ai.WorkingMemory.GetItem<bool>("wander"))
        {
            animator.SetBool("cellphone", true);

        }
        else
        {
            target.GetComponent<InteractWithEmployee>().glande = true;

            for (int i = 0; i < target.GetComponent<InteractWithEmployee>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<InteractWithEmployee>().animatorStates[i], true);
            }
        }


/*


        animator.SetBool("sit", true);

        if (target.name == "ToiletTrigger" || target.transform.parent.name == "TV")
        {
           // animator.SetBool("typing", true);
            animator.SetBool("sit", true);
        }
        if (target.transform.parent.name == "TV")
        {
            animator.SetBool("playing", true);

        }
        if (target.name == "DrinkTrigger" || target.name == "CoffeeTrigger")
        {
            animator.SetBool("drinking", true);

        }

      */
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

        //Si l'objet n'est pas cassé
        if (ai.WorkingMemory.GetItem<bool>("wander") || target.transform.parent.GetComponentInChildren<BreakableFurniture>() == null || !target.transform.parent.GetComponentInChildren<BreakableFurniture>().broken)
        {
            //fatigue = (int)ai.Body.gameObject.GetComponent<Employe>().data.fatigue;
            //Reduction de la fatigue si existante
            if (employe.fatigue > 0)
            {
                employe.fatigue -= Time.deltaTime * (int)employe.effetRepos;
                //rends le resultat
               // ai.WorkingMemory.SetItem("fatigue", fatigue);
                if (employe.fatigue < 0)
                    employe.fatigue = 0;
                //ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            }

            //Augmente la motivation
            employe.motivation += Time.deltaTime * (int)employe.effetRepos;               
            //ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;

            //Si motivation Max, cesse de glander
            if (employe.motivation >= (int)employe.motivationMax)
            {
                employe.motivation = employe.motivationMax;
                ai.WorkingMemory.SetItem("auTravail", true);
                //Libere la place (encore en test)
                return ActionResult.SUCCESS;
            }

            //Continue a glander
            return ActionResult.RUNNING;
        }

        else
        {
            //Motivation augmente si machine cassee, Fatigue augmente         
            employe.motivation += employe.effetEngueulement * employe.fatigueSiCasse;
            employe.fatigue +=  employe.effetEngueulement * employe.fatigueSiCasse;

            //Au travail!
            if (employe.motivation >= (int)employe.motivationMax)
            {
                employe.motivation = employe.motivationMax;      
            }
            ai.WorkingMemory.SetItem("auTravail", true);

            //ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;
           // ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            return ActionResult.SUCCESS;
        }
    }

    public override void Stop(RAIN.Core.AI ai)
    {           
        //ai.WorkingMemory.SetItem("motivation", motivation);
        if (ai.WorkingMemory.GetItem<bool>("wander"))
        {
            animator.SetBool("cellphone", false);

        }
        else
        {
            for (int i = 0; i < target.GetComponent<InteractWithEmployee>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<InteractWithEmployee>().animatorStates[i], false);


            }
            target.GetComponent<InteractWithEmployee>().glande = false;

        }
        
        /*
        
        animator.SetBool("sit", false);
        animator.SetBool("playing", false);
        animator.SetBool("drinking", false);
        animator.SetBool("cellphone", false);
        */
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
       

        base.Stop(ai);
    }
}