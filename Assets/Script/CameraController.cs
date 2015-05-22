using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
      
     
     private Vector3 velocity = Vector3.zero;
     private GameObject target;
	 private bool bossCreated;
	 private float timer = 0;

	 public bool cameraIsToMove;
	 public bool fixedCamera;

	 public float dampTime = 0.15f;

	 public bool shaking;
	 public float shakeMagnitude;
	 public float shakeTimer;
	 public float upDownMargin;
	 public float leftRightMargin;

     

     void Update () 
     {
		// Follow Boss, if created
		if (bossCreated) {

			//Camera is not fixed on Target (normal Boss Mode)
			if (!fixedCamera) {

				//If Boss is within screen borders
				if (Camera.main.WorldToScreenPoint (target.transform.position).x < Screen.width * leftRightMargin ||
					Camera.main.WorldToScreenPoint (target.transform.position).x > Screen.width * (1 - leftRightMargin) ||
				    Camera.main.WorldToScreenPoint (target.transform.position).y < Screen.height * upDownMargin ||
					Camera.main.WorldToScreenPoint (target.transform.position).y > Screen.height * (1 - upDownMargin)) {

					cameraIsToMove = true;
				}
			}

			//Camera is fixed on Target (cinematic effects : suicideCam, elevator focus etc.)
			else {
				cameraIsToMove = true;
			}

			//Follow the target if it's needed
			if (cameraIsToMove) {
				FollowTargetTillOnIt ();
			}

			//Shake Your Booty, yeahhh !!!
			if(shaking && timer < shakeTimer){
				ShakeMyBooty();
				timer++;
			}
			else{
				shaking = false;
				timer = 0.0f;
			}
		}
		
		else{
		   // Looking for Boss GameObject
		   target = GameObject.FindGameObjectWithTag ("Boss");
		   if (target!=null) bossCreated = true;
		}
     
     }

	public void FollowTargetTillOnIt()
	{

        Vector3 delta = target.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, GetComponent<Camera>().WorldToViewportPoint(target.transform.position).z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		    
		if ((int)delta.magnitude == 0) {
			cameraIsToMove = false;
		}
	}

	public void ShakeMyBooty()
	{
		transform.position = new Vector3(transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f,
		                                 transform.position.y,
		                                 transform.position.z + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f);

	}

 }

