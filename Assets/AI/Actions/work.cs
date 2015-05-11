using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class work : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);

        //Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
        ai.WorkingMemory.SetItem("working", true);
        // auTravail = true;
        //setTaget(null);	
        //setTaget(chill[index]);
        ai.WorkingMemory.SetItem("enDeplacement", false);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {

  

    //    while (ai.WorkingMemory.GetItem<int>("motivation") > 0)
     //   {
            //print("MOTIV: "+ motivation);
            //fatigue += vitesseFatigue;
        int mot = ai.WorkingMemory.GetItem<int>("motivation") - ai.WorkingMemory.GetItem<int>("feignantise");
            ai.WorkingMemory.SetItem("motivation", mot);
           // yield return new WaitForSeconds((float)(1.0f / vitesseTravail));
      //  }

     //   int index = Random.Range(0, chill.Length);
       // print("CHANGE TARGET: " + chill[index]);
      

       // ai.WorkingMemory.SetItem("enDeplacement", true);

        //setTaget(chill[index]);
       // ai.WorkingMemory.SetItem("chillTarget", chill[index].transform.position);

            if (ai.WorkingMemory.GetItem<int>("motivation") == 0)
                return ActionResult.SUCCESS;
            else return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        ai.WorkingMemory.SetItem("working", false);
        base.Stop(ai);
    }
}