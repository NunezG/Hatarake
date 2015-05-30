using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Navigation.NavMesh;

public static class NavMesh  {

	private static int _threadCount = 4;
    public static List<GameObject> navMeshes = new List<GameObject>();
   // GameObject navMeshPrefab;
    public static bool isNavMeshDone = false;
    static GameObject navmeshPrefab = (GameObject)Resources.Load("Navigation Mesh");
    

	// This will regenerate the navigation mesh when called
	public static IEnumerator GenerateNavmesh(Office office)
	{
        float size = office.size * office.transform.localScale.x;
        float officeMiddle = (size - office.transform.localScale.x) / 2.0f;

       // Debug.Log("pos " + );
       // GameObject go = (GameObject)Resources.Load("Navigation Mesh");

        GameObject go = GameObject.Instantiate(navmeshPrefab);
        go.name = "NavMesh of " + office.name;
        navMeshes.Add(go);
        go.transform.parent = office.transform;
        go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        go.transform.position = office.transform.position + new Vector3(officeMiddle, 0, officeMiddle);

        NavMeshRig tRig = go.GetComponent<RAIN.Navigation.NavMesh.NavMeshRig>();

		// Unregister any navigation mesh we may already have (probably none if you are using this)
		tRig.NavMesh.UnregisterNavigationGraph();
        tRig.NavMesh.Size = size;

       // print("NavecMesh Size : " + office.size);
		float startTime = Time.time;
		tRig.NavMesh.StartCreatingContours(tRig, _threadCount);
		while (tRig.NavMesh.Creating)
		{
			tRig.NavMesh.CreateContours();
			
			yield return new WaitForSeconds(1);
		}
		isNavMeshDone = true;
		float endTime = Time.time;
		Debug.Log("NavMesh generated in " + (endTime - startTime) + "s!!! That's frikkin' fast, man!!!");
		tRig.NavMesh.RegisterNavigationGraph();
		tRig.Awake();
	}
}