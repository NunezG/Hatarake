using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Navigation;


[RAINAction]
public class clearPath : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
                     RaycastHit hit;
                   //  Debug.Log("CLEQRRRRRRRRRRRR: " );



       // if (ai.Navigator.CurrentGraph == null )
     //   {
         //   Debug.Log("NO HQY GRAPH PARA : " + ai.Body.transform.name);

          //  ai.Navigator.CurrentPath = null;
         //   return ActionResult.SUCCESS;
        //}

       if ((Physics.Raycast(ai.Body.transform.position, ai.Body.transform.TransformDirection(Vector3.forward), out hit, 50.0f) && hit.transform.tag == "Employe"))

        {

            Debug.Log("HITTT: ");
            ai.Motor.MoveTo(ai.Body.transform.TransformDirection(Vector3.back) * 50);
            //ai.Motor.UpdateMotionTransforms();

            return ActionResult.RUNNING;

        }

        else if (ai.Navigator.CurrentGraph != null  &&  ai.Navigator.OnGraph(ai.Body.transform.position,3))  
        {
            //ai.Navigator.CurrentPath = null;

            return ActionResult.SUCCESS;


        }


        else // || (Physics.Raycast(ai.Body.transform.position, ai.Body.transform.TransformDirection(Vector3.forward), out hit, 50.0f) && hit.transform.tag == "Employe"))
        {


          //  ai.Motor.MoveTarget = new RAIN.Motion.MoveLookTarget(ai.Body.transform.TransformDirection(Vector3.back));

           // ai.Motor.MoveTarget.set = ai.Body.transform.TransformDirection(Vector3.back) * 50;




 //if(!ai.Motor.IsAtMoveTarget)
 //{
// ai.Motor.Move();
 //return ActionResult.RUNNING;
 //}

           // if (ai.Navigator.CurrentPath != null)
           // {

           // ai.Body.transform.TransformDirection (Vector3.forward);

          //  var fwd = ai.Body.transform.TransformDirection (Vector3.back);

            

           // Vector3 rayDirection = ai.Body.transform.forward- ai.Body.transform.position;
           // if (ai.Navigator.CurrentGraph != null)
                  //       Debug.Log("CHOQUE : " + ai.Navigator.CurrentGraph.Size);

                         
                   //      if (ai.Navigator.CurrentGraph.IsPointOnGraph(ai.Body.transform.position))
             //  Debug.Log("CHOQUE : " + (hit.transform.position - ai.Body.transform.position));

           // if (hit.transform.tag == "Employe")
          //  {
         //Debug.Log("PROOUTTTT: "+ai.Body.transform.position);
        // return true;
//}else{
     //    Debug.Log("Can not see player");
         //return false;
 //    }

      //   ai.Navigator.waypo
            //if (ai.Navigator.CurrentPath != null)
        //   Debug.Log("BOXXXIEIEIIE "+    ai.Body.transform.name);

         //  Debug.Log("POSSSSSSSSSSS "+    ai.Body.transform.position);
           
            
            
            // ai.Motor.MoveTo(ai.Body.transform.TransformDirection(Vector3.back) * 50);
           // ai.Motor.UpdateMotionTransforms();
            ai.Navigator.CurrentPath = null;

           if (ai.Navigator.CurrentPath != null )
           {
             //  Debug.Log("NOPAZPAPAPAPPA: ");

             //  Debug.Log("FUKCKCKCKC " + ai.Navigator.CurrentPath.GetWaypointPosition(ai.Navigator.CurrentPath.GetClosestWaypoint(ai.Body.transform.position)));

             //  ai.Motor.MoveTo(new Vector3(ai.Navigator.CurrentPath.GetWaypointPosition(ai.Navigator.CurrentPath.GetClosestWaypoint(ai.Body.transform.position)).x,ai.Body.transform.position.y , ai.Navigator.CurrentPath.GetWaypointPosition(ai.Navigator.CurrentPath.GetClosestWaypoint(ai.Body.transform.position)).z));
              

                  // ai.Navigator.CurrentPath.GetWaypointPosition(ai.Navigator.CurrentPath.GetClosestWaypoint(ai.Body.transform.position));
               //  ai.Motor.MoveTo(ai.Navigator.CurrentPath.GetPositionOnPath(3000));
           }
           else
               if (!ai.Navigator.OnGraph(ai.Body.transform.position, 2))
               {

                   ai.Navigator.CurrentPath = null;
                   return ActionResult.SUCCESS;

               
               }



               else{

                   ai.Navigator.CurrentPath = null;
                   return ActionResult.SUCCESS;


           //   Debug.Log("NOPATHS ");
           }
           // ai.Motor.MoveTo(ai.Body.gameObject.GetComponent<Employe>().boxDeTravail.transform.position);

            int possa = Random.Range(0,10);
            int possb = Random.Range(0, 10);

               // if (ai.Navigator.OnGraph(ai.Body.transform.position,2))    
           //   ai.Motor.MoveTo(ai.Body.transform.TransformDirection(Vector3.back));


           // ai.Body.gameObject.GetComponent<Rigidbody>().MovePosition(ai.Body.transform.TransformDirection(Vector3.back) * 50);


         //  ai.Navigator.CurrentPath.GetPositionOnPath(30);
          //  ai.WorkingMemory.SetItem<GameObject>("myTarget", ai.Body.gameObject.GetComponent<Employe>().boxDeTravail);


       //  ai.Motor.MoveTo(ai.Body.transform.TransformDirection(Vector3.back) *5000);
              //  ai.Navigator.NextWaypoint
           // }
           // ai.Body.gameObject.GetComponent<Rigidbody>().AddForce(ai.Body.transform.TransformDirection(Vector3.back) * 5000);
       ///  ai.Navigator.CurrentPath = null;
            ai.Navigator.CurrentPath = null;

                return ActionResult.RUNNING;

         //  ai.Body.gameObject.GetComponent<Rigidbody>().AddForce(fwd);
       //  ai.Body.gameObject.GetComponent<Rigidbody>().AddForce(ai.Body.transform.TransformDirection(Vector3.back) * 5000);

                     }




          
		//ai.Navigator.Start ();
		//tNav.RestartPathfindingSearch();

    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}