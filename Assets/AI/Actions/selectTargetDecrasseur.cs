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


        if (target != null)
        {
            if (target.transform.parent.Find("breakPos") == null)
            {
                if (!Employe.emptyChill.Contains(target))
                    Employe.emptyChill.Add(target);

            }
            Decrasseur.emptyRepair.Add(target);
        }

       // if (target != null && Employe.emptyChill != null && (target.CompareTag("Repos") && !Employe.emptyChill.Contains(target)))
          //  Employe.emptyChill.Add(target);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<bool>("hatarake"))
        {
            ai.Motor.DefaultSpeed = ai.WorkingMemory.GetItem<int>("scaredSpeed");
        }

          //*1/4 de chances de glander dehors
        if (Decrasseur.emptyRepair.Count != 0)
                {          
                    //*cherche une place vide pour glander
                    int pos = Random.Range(0, Decrasseur.emptyRepair.Count);
                   // Debug.Log("Employe.emptyChill.Count" + Employe.emptyChill.Count);

                   // if (Employe.emptyChill[pos].transform.parent.GetComponentInChildren<BreakableFurniture>() != null)
                  //  {

                    target = Decrasseur.emptyRepair[pos];

                    bool good = true;

                    foreach (Transform t in target.transform.parent)
                    {
                        if (t.CompareTag("Repos"))
                        {
                            if (!Employe.emptyChill.Contains(t.gameObject))
                            {
                                good = false;
                                return ActionResult.RUNNING;
                            }
                        }
                    }

                    if (target.transform.parent.Find("breakPos") == null && good) 
                    {
                        if (Employe.emptyChill.Contains(target))
                            Employe.emptyChill.Remove(target);
                        
                        else return ActionResult.RUNNING;
                    }
                    else
                    {
                       

                    }

                    Decrasseur.emptyRepair.RemoveAt(pos);

                        return ActionResult.SUCCESS;
                   // }
                }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
        ai.WorkingMemory.SetItem<GameObject>("myTarget", target);

        ai.WorkingMemory.SetItem<bool>("hatarake", false);

        Transform tr = target.transform.parent.transform.FindChild("lookAt");
        // GameObject parent = target.transform.parent.transform.FindChild("lookAt").gameObject;
        if (tr != null)
        {
            ai.WorkingMemory.SetItem<GameObject>("viewTarget", tr.gameObject);
        }
    }
}