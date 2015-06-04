using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
      
     
     private Vector3 velocity = Vector3.zero;
     public GameObject target;
	 private bool bossCreated;
	 private float timer = 0;

	 public GameObject pointer;

	 public bool cameraIsToMove;
	 public bool fixedCamera;

	 public float dampTime = 0.15f;

	 public bool shaking;
	 public float shakeMagnitude;
	 public float shakeTimer;

	 public bool onOtherTarget;
	 public float focusTimer;

	 public float upDownMargin;
	 public float leftRightMargin;

     void Start(){

		pointer = GameObject.Find ("notifArrow");

	}

     void Update () 
     {
		// Follow Target, once Boss is created
		if (target != null && bossCreated) {

            if (target == null) print("plonkiplonka");
			//Nullify target is on suicided employee
			if(!target.activeInHierarchy){
				target = null;
			}

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
            if (cameraIsToMove)
            {
                if (target != null) FollowTargetTillOnIt ();
			}

			//Shake Your Booty, yeahhh !!!
			ShakeMyBooty ();

			//If the target changes, triggers the timer, get back to Boss when finished
			if (target != GameObject.Find ("Boss(Clone)")) {
				onOtherTarget = true;
				focusTimer--;
				fixedCamera = true;
				pointer.SetActive(true);
				dampTime = 0.0f;


				if (focusTimer < 0.0f) {
					focusTimer = 100.0f;
					target = null;
					onOtherTarget = false;
					fixedCamera = false;
					dampTime = 0.2f;
				}
			}

				//Camera is follwing Boss
			else{

				pointer.SetActive(false);

			}
		}
		//target is null, lookin for Boss
		else {
			target = GameManager.instance.boss;
			if (target != null)
				bossCreated = true;
		}
	}
     

	public void FollowTargetTillOnIt()
	{

        Vector3 delta = target.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, GetComponent<Camera>().WorldToViewportPoint(target.transform.position).z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		    
		// Stop the camera when close enough
		if ((int)delta.magnitude == 0) {
			cameraIsToMove = false;
		}
	}
	
	//Shakes the camera for a certain amount of time
	public void ShakeMyBooty()
	{
		if(shaking && timer < shakeTimer){
			transform.position = new Vector3(transform.position.x + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f,
			                                 transform.position.y,
			                                 transform.position.z + Random.Range(-shakeMagnitude, shakeMagnitude)*0.1f);
			timer++;
		}

		else {
			shaking = false;
			timer = 0.0f;
		}
	}

	//changes focus and focusTimer
	public void FollowEmployee(GameObject employee, float fT)
	{

		target = employee;
		focusTimer = fT;
		fixedCamera = false;
	}

    public void BackToTheBoss()
    {
        focusTimer = 100.0f;
        target = null;
        onOtherTarget = false;
        fixedCamera = false;
    }

 }

