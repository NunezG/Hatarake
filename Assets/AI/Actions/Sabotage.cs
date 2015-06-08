using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Sabotage : RAINAction
{
    Animator animator;


    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        animator = ai.Body.GetComponent<Animator>();

     
        animator.SetBool("doingStuff", true);

        

        
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if  (ai.WorkingMemory.GetItem<GameObject>("target") != null)
        ai.WorkingMemory.GetItem<GameObject>("target").GetComponentInChildren<BreakableFurniture>().Hit();

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}