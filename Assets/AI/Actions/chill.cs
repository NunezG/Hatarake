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
        fatigue = (int)ai.Body.gameObject.GetComponent<Employe>().data.fatigue;

		//Set de bool, sert a rien pour l'instant
        ai.WorkingMemory.SetItem("enDeplacement", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

        //Si l'objet n'est pas cassé
        if (target.transform.parent.GetComponentInChildren<BreakableFurniture>() == null || !target.transform.parent.GetComponentInChildren<BreakableFurniture>().broken)
        {
            //Reduction de la fatigue si existante
            if (fatigue > 0)
                fatigue = fatigue - Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;

            //Augmente la motivation
            motivation = motivation + Time.deltaTime * (int)ai.Body.gameObject.GetComponent<Employe>().data.effetRepos;

            //Si motivation Max, cesse de glander
            if (motivation >= (int)ai.Body.gameObject.GetComponent<Employe>().data.motivationMax)
            {
                ai.WorkingMemory.SetItem<bool>("wander", false);
                ai.WorkingMemory.SetItem("auTravail", true);
                //Libere la place (encore en test)
                return ActionResult.SUCCESS;
            }

            //Continue a glander
            return ActionResult.RUNNING;
        }

        else
        {
            //Motivation reduite si machine cassee (Fatigue augmente?)
            motivation = motivation - Time.deltaTime * ai.Body.GetComponent<Employe>().data.vitesseDemotivation * DemotivationSiCasse.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<float>(); ;
            return ActionResult.SUCCESS;
        }
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        //rends le resultat
        ai.WorkingMemory.SetItem("fatigue", fatigue);
        ai.Body.gameObject.GetComponent<Employe>().data.fatigue = fatigue;
        //ai.WorkingMemory.SetItem("motivation", motivation);
        ai.Body.gameObject.GetComponent<Employe>().data.motivation = motivation;

        if (!target.CompareTag("Box"))
            // ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Repos>().occupe = false;
            Employe.emptyChill.Add(target);

        ai.Body.GetComponent<Rigidbody>().constraints =RigidbodyConstraints.FreezePositionY ;

        base.Stop(ai);
    }
}