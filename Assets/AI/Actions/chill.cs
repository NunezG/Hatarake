using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class chill : RAINAction
{

    public Expression myExpression;
   public  Vector2 testVect;


    public override void Start(RAIN.Core.AI ai)
    {
        testVect = myExpression.Evaluate(ai.DeltaTime, ai.WorkingMemory).GetValue<Vector2>();
        ai.WorkingMemory.SetItem("enDeplacement", false);
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        // setTaget(null);
      



       //  while (ai.WorkingMemory.GetItem<int>("motivation") < ai.WorkingMemory.GetItem<int>("motivationMax"))
      //  {
            if (ai.WorkingMemory.GetItem<int>("fatigue") > 0)
                ai.WorkingMemory.SetItem<int>("fatigue", ai.WorkingMemory.GetItem<int>("fatigue") - ai.WorkingMemory.GetItem<int>("vitesseFatigue"));
            ai.WorkingMemory.SetItem<int>("motivation", ai.WorkingMemory.GetItem<int>("motivation") - ai.WorkingMemory.GetItem<int>("effetRepos"));
            
           // yield return new WaitForSeconds(1.0f / vitesseTravail);
     //   }
       // print("CHANGE TARGET: " + boxDeTravail);
       // ai.WorkingMemory.SetItem("enDeplacement", true);



            if (ai.WorkingMemory.GetItem<int>("motivation") == ai.WorkingMemory.GetItem<int>("motivationMax"))
                return ActionResult.SUCCESS;
            else return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {



        base.Stop(ai);
    }
}