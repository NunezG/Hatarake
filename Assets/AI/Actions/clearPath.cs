using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation;


[RAINAction]
public class clearPath : RAINAction
{
    bool tempDeviation = false;
    int countDeviation;
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
               tempTarget = ai.Body.transform.position + ai.Body.transform.TransformDirection(Vector3.left) * 4 + ai.Body.transform.TransformDirection(Vector3.forward);
           else tempTarget = ai.Body.transform.position + ai.Body.transform.TransformDirection(Vector3.right) * 4 + ai.Body.transform.TransformDirection(Vector3.forward);

           ai.WorkingMemory.SetItem("noPath", true);

            ai.Motor.MoveTo(tempTarget);

            tempDeviation = true;
            countDeviation = 10;
            return ActionResult.RUNNING;
        }
        else if (tempDeviation)
        {
            ai.Motor.MoveTo(tempTarget);
            countDeviation--;
            if (countDeviation == 0)
            {
                //  ai.Navigator.CurrentPath = null;

                ai.WorkingMemory.SetItem("noPath", false);

                tempDeviation = !tempDeviation;
                return ActionResult.SUCCESS;
            }
            else
                return ActionResult.RUNNING;
        }

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}