using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Repair : RAINAction
{

    GameObject target;
    Animator animator;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        animator = ai.Body.GetComponent<Animator>();
        animator.SetBool("action", true);

        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (target.transform.parent.GetComponentInChildren<BreakableFurniture>().broken)
        {
           if (target.transform.parent.GetComponentInChildren<BreakableFurniture>().Repair())
           {

               return ActionResult.SUCCESS;

           }else return ActionResult.RUNNING;
        }else   
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
        ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("normalSpeed");
        animator.SetBool("action", false);
        ai.Body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

    }
}