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
       float motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
        ai.WorkingMemory.SetItem("motivation", motivation);
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

        if (ai.Body.GetComponent<Employe>().data.motivation <= 0)
        {
              ai.WorkingMemory.SetItem("auTravail", false);
      //      int pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().chill.Length-1);

           foreach (GameObject go in ai.Body.GetComponent<Employe>().chill)
           {
               if (go.GetComponent<Repos>().occupe == false)
               {
                   Sign.Create(1, ai.Body.transform.position, SignType.Glande);

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
                foreach (GameObject go in ai.Body.GetComponent<Employe>().workingHelp)
                {
                    if (go.GetComponent<Box>().occupe == false)
                    {
                        Sign.Create(1, ai.Body.transform.position, SignType.Work);

                        go.GetComponent<Box>().occupe = true;
                        target = go;
                        return ActionResult.SUCCESS;
                    }
                }
            } else
            {
                Sign.Create(1, ai.Body.transform.position, SignType.Work);

               target = ai.Body.GetComponent<Employe>().boxDeTravail;

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