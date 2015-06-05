using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Repair : RAINAction
{

    GameObject target;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

        if (target.transform.parent.GetComponentInChildren<BreakableFurniture>().broken)
        {
            target.transform.parent.GetComponentInChildren<BreakableFurniture>().Repair();

        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
        ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("normalSpeed");

    }
}