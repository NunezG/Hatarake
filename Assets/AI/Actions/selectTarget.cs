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
    private static object _queueLock;

    public override void Start(RAIN.Core.AI ai)
    {
        if (_queueLock == null)
        {
           _queueLock = new object();
        }

        base.Start(ai);
       float motivation = (int)ai.Body.gameObject.GetComponent<Employe>().data.motivation;
        ai.WorkingMemory.SetItem("motivation", motivation);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {   
        if (ai.Body.GetComponent<Employe>().data.motivation <= 0)
        {
              ai.WorkingMemory.SetItem("auTravail", false);

           foreach (GameObject go in ai.Body.GetComponent<Employe>().chill)
           {

               lock (_queueLock)
               {
                   if (go.GetComponent<Repos>().occupe == false)
                   {
                       go.GetComponent<Repos>().occupe = true;

                       //Sign.Create(1, ai.Body.transform.position, SignType.Glande);
                       SignEmitter.Create(1, ai.Body.transform.position, SignType.Glande);
                       go.GetComponent<Repos>().occupe = true;
                       target = go;
                       return ActionResult.SUCCESS;
                   }
        
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
                    lock (_queueLock)
                    {
                        if (go.GetComponent<Box>().occupe == false)
                        {
                            go.GetComponent<Box>().occupe = true;
                                        ai.WorkingMemory.SetItem("auTravail", true);

                            //Sign.Create(1, ai.Body.transform.position, SignType.Work);
                            SignEmitter.Create(1, ai.Body.transform.position, SignType.Work);
                            go.GetComponent<Box>().occupe = true;
                            target = go;
                            return ActionResult.SUCCESS;
                        }
                       
                    }
                }
            } else
            {
                //Sign.Create(1, ai.Body.transform.position, SignType.Work);
                SignEmitter.Create(1, ai.Body.transform.position, SignType.Work);
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