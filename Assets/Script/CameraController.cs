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
     public bool endFollowByShaking=false;
     public float focusTimeToStartShaking;

	 public float dampTime = 0.15f;
     public float followDampTime;

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
            if (target != GameManager.instance.boss)
            {
                GameManager.instance.boss.GetComponent<Boss>().moveLocked = true;
                GameManager.instance.boss.GetComponent<Boss>().hatarakeLocked = true;
				onOtherTarget = true;
				focusTimer-=Time.deltaTime;
				fixedCamera = true;
				pointer.SetActive(true);
                dampTime = followDampTime;
                if (endFollowByShaking && !shaking && focusTimer <= focusTimeToStartShaking)
                {
                    shaking = true;
                }

				if (focusTimer < 0.0f) {
                    shaking = false;
                    endFollowByShaking = false;
					focusTimer = 100.0f;
					target = null;
					onOtherTarget = false;
					//fixedCamera = false;
					dampTime = 0.2f;
				}
			}

				//Camera is follwing Boss
			else{
                if (!GameManager.instance.tutoMoveLock && GameManager.instance.boss.GetComponent<Boss>().moveLocked)
                {
                    //print("hahahhah");
                    GameManager.instance.boss.GetComponent<Boss>().moveLocked = false;
                }
                if (!GameManager.instance.tutoHatarakeLock && GameManager.instance.boss.GetComponent<Boss>().hatarakeLocked)
                {

                    //print("hahahhah bis");
                    GameManager.instance.boss.GetComponent<Boss>().hatarakeLocked = false;
                }

				if(pointer.activeInHierarchy)pointer.SetActive(false);

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
	public void FollowEmployee(GameObject employee, float fT,float followDampTime)
	{
        //print("name  :"+employee.name);
		target = employee;
		focusTimer = fT;
		fixedCamera = true;
        this.followDampTime = followDampTime;

	}

    public void FollowObjectAndShakeAtTheEnd(GameObject gObject, float fT, float focusTimeToStartShaking,float followDampTime)
    {

        //print("name  :"+employee.name);
        target = gObject;
        focusTimer = fT;
        fixedCamera = true;
        endFollowByShaking = true;
        this.focusTimeToStartShaking = focusTimeToStartShaking;
        this.followDampTime = followDampTime;
    }

    public void BackToTheBoss()
    {
        focusTimer = 100.0f;
        target = null;
        onOtherTarget = false;
        //fixedCamera = false;
    }

 }

