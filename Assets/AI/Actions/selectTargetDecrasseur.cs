using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class selectTargetDecrasseur : RAINAction
{
    GameObject target;


    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");

        if (target != null && Employe.emptyChill != null && (target.CompareTag("Repos") && !Employe.emptyChill.Contains(target)))
            Employe.emptyChill.Add(target);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

          //*1/4 de chances de glander dehors
                 if (Employe.emptyChill.Count != 0)
                {          
                    //*cherche une place vide pour glander
                    int pos = Random.Range(0, Employe.emptyChill.Count);
                   // Debug.Log("Employe.emptyChill.Count" + Employe.emptyChill.Count);

                    if (Employe.emptyChill[pos].transform.parent.GetComponentInChildren<BreakableFurniture>() != null)
                    {
                        target = Employe.emptyChill[pos];
                        Employe.emptyChill.RemoveAt(pos);
                        return ActionResult.SUCCESS;

                    }
                }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
        ai.WorkingMemory.SetItem<GameObject>("myTarget", target);
    }
}