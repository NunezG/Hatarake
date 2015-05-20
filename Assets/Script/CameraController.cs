using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
      
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;

     private Transform target;
	 public bool bossCreated;

	 public bool shaking;
	 public float shakeMagnitude;
	 public float upDownMargin;
	 public float leftRightMargin;


     void Update () 
     {
		// Follow Boss, if created
		if (bossCreated) {

			FollowTarget ();

		} else {

			// Looking for Boss GameObject
			try{

				target = GameObject.FindGameObjectWithTag ("Boss").transform;
			    bossCreated = true;

			} catch {}
		}

		//Shaking Effect if enabled
		if(shaking)
			ShakeMyBooty ();
     
     }

	public void FollowTarget()
	{
		if(true) // condition à compléter : si Boss en dehors du Rect(leftRightMargin, upDownMargin, width - 2*leftRightMargin, height - 2*upDownMargin)
			{
				Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, GetComponent<Camera>().WorldToViewportPoint(target.position).z));
				Vector3 destination = transform.position + delta;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		    }
	}

	public void ShakeMyBooty()
	{
		transform.position = new Vector3(transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f,
		                                 transform.position.y + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f,
		                                 transform.position.z + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f);

	}

 }

