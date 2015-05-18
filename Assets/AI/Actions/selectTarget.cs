using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class selectTarget : RAINAction
{
 //   public Expression target;
    GameObject target;

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        target = ai.WorkingMemory.GetItem<GameObject>("myTarget");
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (target.CompareTag("Repos") == true)
		{
            target.GetComponent<Repos>().occupe = false;
        }
        else if (target.CompareTag("WorkHelp") == true)
		{
            target.GetComponent<Box>().occupe = false;
		}

        if ( ai.WorkingMemory.GetItem<int>("motivation") <= 0)
        {
      //      int pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().chill.Length-1);

           foreach (GameObject go in ai.Body.gameObject.GetComponent<Employe>().chill)
           {
               if (go.GetComponent<Repos>().occupe == false)
               {
                   go.GetComponent<Repos>().occupe = true;
                   target = go;
                   return ActionResult.SUCCESS;
               }
           }
        }
        else 
        {
            int pos = Random.Range(0, 4);

            if (pos == 0)
            {
                foreach (GameObject go in ai.Body.gameObject.GetComponent<Employe>().workingHelp)
                {
                    if (go.GetComponent<Box>().occupe == false)
                    {
                        go.GetComponent<Box>().occupe = true;
                        target = go;
                        return ActionResult.SUCCESS;
                    }
                }
            } else
            {
               target = ai.Body.gameObject.GetComponent<Employe>().boxDeTravail;

                return ActionResult.SUCCESS;
            }
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        ai.WorkingMemory.SetItem("myTarget", target);
        base.Stop(ai);
    }
}