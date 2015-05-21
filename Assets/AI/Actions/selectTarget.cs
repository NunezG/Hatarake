using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class selectTarget : RAINAction
{
 // public Expression target;
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
        //*Si motivation a 0 commence a glander
        if (ai.Body.GetComponent<Employe>().data.motivation <= 0)
        {
              ai.WorkingMemory.SetItem("auTravail", false);
              ai.WorkingMemory.SetItem("glande", true);
              //SignEmitter.Create(1, ai.Body.transform.position, SignType.Glande);

            //*cherche une place vide pour glander
           foreach (GameObject go in ai.Body.GetComponent<Employe>().chill)
           {
               lock (_queueLock)
               {
                   if (go.GetComponent<Repos>().occupe == false)
                   {
                       go.GetComponent<Repos>().occupe = true;                  
                       //Sign.Create(1, ai.Body.transform.position, SignType.Glande);
                       //SignEmitter.Create(1, ai.Body.transform.position, SignType.GoingToGlande);
                       target = go;
                       return ActionResult.SUCCESS;
                   }
               }
           }

           //*Si pas de place il retourne a son Box pour se connecter sur facebook
           target = ai.Body.GetComponent<Employe>().boxDeTravail;
           return ActionResult.SUCCESS;


        }
        else 
        {
            //*Si motivation cherche une place vide pour bosser
            ai.WorkingMemory.SetItem("glande", false);

            int pos = Random.Range(0, 4);

            //*1/4 de chances de bosser sur une photocopieuse
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
                            //SignEmitter.Create(1, ai.Body.transform.position, SignType.Work);
                            go.GetComponent<Box>().occupe = true;
                            target = go;
                            return ActionResult.SUCCESS;
                        }    
                    }
                }
            } else
            {
                //*3/4 de chances de bosser dans son Box
                ai.WorkingMemory.SetItem("auTravail", true);

                //Sign.Create(1, ai.Body.transform.position, SignType.Work);
                //SignEmitter.Create(1, ai.Body.transform.position, SignType.Work);
                target = ai.Body.GetComponent<Employe>().boxDeTravail;

                return ActionResult.SUCCESS;
            }
        }

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        //*set des variables après succès
        ai.WorkingMemory.SetItem("enDeplacement", true);
        ai.WorkingMemory.SetItem("myTarget", target);
        base.Stop(ai);
    }
}