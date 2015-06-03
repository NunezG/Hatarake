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

    public override void Start(RAIN.Core.AI ai)
    {
        motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
        

		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        base.Start(ai);
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
                fatigue = fatigue - Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;
                //rends le resultat
               // ai.WorkingMemory.SetItem("fatigue", fatigue);
                ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            }

            //Augmente la motivation
            motivation = motivation + Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;
            ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;

            //Si motivation Max, cesse de glander
            if (motivation >= (int)ai.Body.gameObject.GetComponent<Employe>().data.motivationMax)
            {    
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
            fatigue += Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation * DemotivationSiCasse.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<float>();
            ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;
            ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
            return ActionResult.SUCCESS;
        }
    }

    public override void Stop(RAIN.Core.AI ai)
    {           
        //ai.WorkingMemory.SetItem("motivation", motivation);

        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        base.Stop(ai);
    }
}