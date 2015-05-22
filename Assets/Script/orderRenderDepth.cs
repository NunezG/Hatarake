using UnityEngine;
using System.Collections;

public class orderRenderDepth : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
