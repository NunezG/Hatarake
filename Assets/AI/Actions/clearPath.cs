using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation;


[RAINAction]
public class clearPath : RAINAction
{
   // bool tempDeviation = false;
   // int countDeviation;
    Vector3 tempTarget;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
       RaycastHit hit;

       if ((ai.Navigator.CurrentGraph != null && !ai.Navigator.OnGraph(ai.Body.transform.position, 3)) || (Physics.Raycast(ai.Body.transform.position, ai.Body.transform.TransformDirection(Vector3.forward), out hit, 2.0f) && (hit.transform.tag == "Employe" || hit.transform.tag == "Boss")))
        {
            int randDir = Random.Range(0,2);
           if (randDir == 0)
               tempTarget = ai.Body.transform.position + ai.Body.transform.TransformDirection(Vector3.left) * 3 + ai.Body.transform.TransformDirection(Vector3.forward);
           else tempTarget = ai.Body.transform.position + ai.Body.transform.TransformDirection(Vector3.right) * 3 + ai.Body.transform.TransformDirection(Vector3.forward);

           if (CheckPositionOnNavMesh(tempTarget, ai))
          {
               ai.WorkingMemory.SetItem("tempTarget", tempTarget);
               ai.WorkingMemory.SetItem("noPath", true);
           }

       //    else Debug.Log("NO GOOD TARGET");

           // ai.Motor.MoveTo(tempTarget);

            //tempDeviation = true;
           // countDeviation = 10;
            return ActionResult.FAILURE;
        }
       /* else if (tempDeviation)
        {
           // ai.WorkingMemory.SetItem("tempTarget", tempTarget); 

           // ai.Motor.MoveTo(tempTarget);
            countDeviation--;
            if (countDeviation == 0)
            {
                //  ai.Navigator.CurrentPath = null;

                ai.WorkingMemory.SetItem("noPath", false);

                tempDeviation = !tempDeviation;
                return ActionResult.FAILURE;
            }
            else
                return ActionResult.RUNNING;
        }
        */
       //ai.WorkingMemory.SetItem("noPath", false);
       return ActionResult.SUCCESS;
    }


    private bool CheckPositionOnNavMesh(Vector3 loc, AI ai)
    {
        RAIN.Navigation.Pathfinding.RAINPath myPath = null;
        if (ai.Navigator.GetPathTo(loc, 10, true, out myPath))
            return true;

        return false;
    }


    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}