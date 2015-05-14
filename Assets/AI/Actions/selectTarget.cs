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

    public override void Start(RAIN.Core.AI ai)
    {
       // Debug.Log("STARRRRRRRRRRRT: " );

        base.Start(ai);
      //  Debug.Log("ENDDDDSTA: ");

    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
     // Debug.Log("EXECCCC: " );
		if(ai.WorkingMemory.GetItem<GameObject>("myTarget").CompareTag("Repos") == true)
		{
		//	Debug.Log ("SALLE DE REPOS OCCUPEEEEE");
			ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Repos>().occupe = false;
		}else if(ai.WorkingMemory.GetItem<GameObject>("myTarget").CompareTag("WorkHelp") == true)
		{
		//	Debug.Log ("PHOTOCOPIEUSESEEEEEEEE OCCUPEEEEE");
			ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Box>().occupe = false;
		}


        if ( ai.WorkingMemory.GetItem<int>("motivation") <= 0)
        {
          // Debug.Log("VARRRRRRRRRIABLEEEEEEEEEEEEEEEEEEEEEEEE: " + ai.Body.gameObject.GetComponent<Employe>().chill.Length);


      //      int pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().chill.Length-1);

       //     while (ai.Body.gameObject.GetComponent<Employe>().chill[pos].GetComponent<Repos>().occupe == true)
        //    {
         //      Debug.Log("POSSSSSSSSS: " + pos);

          //      pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().chill.Length-1);



         //   }

           foreach (GameObject go in ai.Body.gameObject.GetComponent<Employe>().chill)
           {
               if (go.GetComponent<Repos>().occupe == false)
               {



                  // Debug.Log("CHILLLLLLL: " + go.name);
                   go.GetComponent<Repos>().occupe = true;
                   ai.WorkingMemory.SetItem("myTarget", go);
                   return ActionResult.SUCCESS;
               }
           }


          //  Debug.Log("FINALPOSSSSSSSSSSSSSSSSSSSSPOSSSSSSSSS: " + pos);

           // Debug.Log("PASSSSSSSSEZEEEEEE   FINALPOSSSSSSSSSSSSSSSSSSSSPOSSSSSSSSS1: " + ai.Body.gameObject.GetComponent<Employe>().chill[pos].GetComponent<Repos>().occupe);

          //  ai.Body.gameObject.GetComponent<Employe>().chill[pos].GetComponent<Repos>().occupe = true;

         //   Debug.Log("PASSSSSSSSEZEEEEEE   FINALPOSSSSSSSSSSSSSSSSSSSSPOSSSSSSSSS: " + ai.Body.gameObject.GetComponent<Employe>().chill[pos].GetComponent<Repos>().occupe);

         //   ai.WorkingMemory.SetItem("myTarget", ai.Body.gameObject.GetComponent<Employe>().chill[pos]);

          //  Debug.Log("RETETETETETETETETETETETEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEDEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE ");

         

        }
        else 
        {
          //  Debug.Log("WORKIEEEEEEEEEEEEEEE /: " + ai.Body.gameObject.GetComponent<Employe>().workingHelp.Length);

            int pos = Random.Range(0, 4);

            if (pos == 0)
            {
                foreach (GameObject go in ai.Body.gameObject.GetComponent<Employe>().workingHelp)
                {
                    if (go.GetComponent<Box>().occupe == false)
                    {
                        go.GetComponent<Box>().occupe = true;
                        ai.WorkingMemory.SetItem("myTarget", go);
                        return ActionResult.SUCCESS;
                    }
                }

                // pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().workingHelp.Length-1);
                //  Debug.Log("WHILEEEERERERERERE:  " + pos);

                // while (ai.Body.gameObject.GetComponent<Employe>().workingHelp[pos].GetComponent<Box>().occupe == true)
                //      pos = Random.Range(0, ai.Body.gameObject.GetComponent<Employe>().workingHelp.Length-1);


                //  Debug.Log("WORKIEEEEEEEEEEEEEEETARGEEYTTYTl:  " + pos);
                // ai.WorkingMemory.SetItem("myTarget", ai.Body.gameObject.GetComponent<Employe>().workingHelp[pos]);
                // ai.Body.gameObject.GetComponent<Employe>().workingHelp[pos].GetComponent<Box>().occupe = true;
            }

            else
            {
                ai.WorkingMemory.SetItem("myTarget", ai.Body.gameObject.GetComponent<Employe>().boxDeTravail);


                return ActionResult.SUCCESS;
            }
        }

      // Debug.Log("FUCKING FAILLLLLLLLLLLLLL ");

        return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
       // Debug.Log("FUCKING STOPPPPPPPPPPPPPPP ");

        base.Stop(ai);
    }
}