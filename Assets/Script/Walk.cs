using UnityEngine;
using System.Collections;


public class Walk : MonoBehaviour {

	public float smoothing = 0.6f;

	public Vector2 testEndPoint= new Vector2(0.9f, 0.9f);
	
	NavMeshAgent navComponent;

	public Transform targetObject;

	public Vector2 Target
	{
		get{ return target; }set
		{
			target = value;
			
			StopCoroutine("Movement");
			StartCoroutine("Movement", target);
		}
	}
	

	// Use this for initialization
	void Start () {

		navComponent = this.transform.GetComponent <NavMeshAgent>();

		//TEST 
		//Target = testEndPoint;

	}

	private Vector2 target;
	private float zoomValue;
	
	void Update()
	{
		if (targetObject)
			navComponent.SetDestination (targetObject.position);

	}

	IEnumerator Movement (Vector2 target)
	{
		Vector3 endP = new Vector3(target.x, target.y, transform.position.z);


		while(Vector3.Distance(transform.position, endP) > 0.05f)
		{
			transform.position = Vector3.Lerp(transform.position, endP, smoothing * Time.deltaTime);
			
			yield return null;
		}

	}
}
