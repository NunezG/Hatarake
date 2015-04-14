using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public float smoothing = 0.6f;

	public float testZoomValue = -2.0f;

	public Camera cam;

	public Vector2 testEndPoint= new Vector2(0.9f, 0.9f);
	

	//public Transform target;
	public Vector2 Target
	{
		get{ return target; }set
		{
			target = value;
			
			StopCoroutine("Movement");
			StartCoroutine("Movement", target);
		}
	}

	public float Zoomer
	{
		get{ return zoomValue; }set
		{
			zoomValue = value;
			
			StopCoroutine("Zoom");
			StartCoroutine("Zoom",zoomValue );
		}
	}


	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();

		//TEST 
		Target = testEndPoint;
		Zoomer = testZoomValue;
		
	}

	private Vector2 target;
	private float zoomValue;

	
	IEnumerator Movement (Vector2 target)
	{
		Vector3 endP = new Vector3(target.x, target.y, transform.position.z);


		while(Vector3.Distance(transform.position, endP) > 0.05f)
		{
			transform.position = Vector3.Lerp(transform.position, endP, smoothing * Time.deltaTime);
			
			yield return null;
		}

	}

	IEnumerator Zoom (float value)
	{

		//Pour changer Z il serait necessaire de mettre la camera en perspective
		//Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z + value);

		float newSize;

		if (value < 0) 
		{
			newSize = cam.orthographicSize / -value;
		
		} else newSize = cam.orthographicSize * value;


		while(Mathf.Abs(cam.orthographicSize- newSize) > 0.05f)
		{

			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize,Time.deltaTime*smoothing);
			
			//transform.position = Vector3.Lerp(transform.position, newPosition , smoothing * Time.deltaTime);
			
			yield return null;
		}
	}
}
