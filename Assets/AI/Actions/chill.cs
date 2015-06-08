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
    GameObject target;
    public Expression DemotivationSiCasse = new Expression();
    Animator animator;



    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
        animator = ai.Body.GetComponent<Animator>();


		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
     
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        if (target.tag == "Corridor")
        {
            animator.SetBool("cellphone", true);

        }
        else
        {
            target.GetComponent<Box>().glande = true;

            for (int i = 0; i < target.GetComponent<Box>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<Box>().animatorStates[i], true);
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
            fatigue = (int)ai.Body.gameObject.GetComponent<Employe>().data.fatigue;
            //Reduction de la fatigue si existante
            if (fatigue > 0)
            {
                fatigue -= Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;
                //rends le resultat
               // ai.WorkingMemory.SetItem("fatigue", fatigue);
                if (fatigue < 0)
                    fatigue = 0;
                ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            }

            //Augmente la motivation
            motivation += Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;               
            ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;

            //Si motivation Max, cesse de glander
            if (motivation >= (int)ai.Body.gameObject.GetComponent<Employe>().data.motivationMax)
            {
                ai.Body.gameObject.GetComponent<Employe>().data.motivation = ai.Body.GetComponent<Employe>().data.motivationMax;
                ai.WorkingMemory.SetItem("auTravail", true);
                //Libere la place (encore en test)
                return ActionResult.SUCCESS;
            }

            //Continue a glander
            return ActionResult.RUNNING;
        }

        else
        {
            //Motivation reduite si machine cassee, Fatigue augmente         
            motivation -= Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation * DemotivationSiCasse.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<float>();
            if (motivation < 0)
                motivation = 0;

            fatigue += Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation * DemotivationSiCasse.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<float>();

            ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;
            ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            return ActionResult.SUCCESS;
        }
    }

    public override void Stop(RAIN.Core.AI ai)
    {           
        //ai.WorkingMemory.SetItem("motivation", motivation);
        if (target.tag == "Corridor")
        {
            animator.SetBool("cellphone", false);

        }
        else
        {
            for (int i = 0; i < target.GetComponent<Box>().animatorStates.Length; i++)
            {
                animator.SetBool(target.GetComponent<Box>().animatorStates[i], false);


            }
            target.GetComponent<Box>().glande = false;

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