using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class chill : RAINAction
{

  //  public Expression myExpression;
  // public  Vector2 testVect;


    public override void Start(RAIN.Core.AI ai)
    {
       // testVect = myExpression.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<Vector2>();

      //  Debug.Log("CHIL START  ");

        ai.WorkingMemory.SetItem("enDeplacement", false);
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        // setTaget(null);

      //  Debug.Log("PUTAIN DE CHILL  ");


        
       //  while (ai.WorkingMemory.GetItem<int>("motivation") < ai.WorkingMemory.GetItem<int>("motivationMax"))
      //  {
            if (ai.WorkingMemory.GetItem<int>("fatigue") > 0)
                ai.WorkingMemory.SetItem("fatigue", ai.WorkingMemory.GetItem<int>("fatigue") - (int)ai.Body.gameObject.GetComponent<Employe>().vitesseFatigue);
          
        ai.WorkingMemory.SetItem("motivation", ai.WorkingMemory.GetItem<int>("motivation") + ai.Body.gameObject.GetComponent<Employe>().effetRepos);
            
           // yield return new WaitForSeconds(1.0f / vitesseTravail);
     //   }
       // print("CHANGE TARGET: " + boxDeTravail);
       // ai.WorkingMemory.SetItem("enDeplacement", true);

         //   Debug.Log("PUTAIN DE MLIDDLE DE CHILLLLLL  ");


            if (ai.WorkingMemory.GetItem<int>("motivation") >= ai.WorkingMemory.GetItem<int>("motivationMax"))
            {

                 ai.WorkingMemory.GetItem<GameObject>("myTarget").GetComponent<Repos>().occupe = false;

                 return ActionResult.SUCCESS;
            }


        //    Debug.Log("PUTAIN DE FAILLLLLLLLL  ");



            return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {



        base.Stop(ai);
    }
}