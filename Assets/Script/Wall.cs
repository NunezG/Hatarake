﻿using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {


		if (Time.frameCount % 200 == 0) {

			if (Random.value >= 0.5)
				
				
			{
				
				
				gameObject.SetActive (false);
				
			}else gameObject.SetActive (true);


		}

	
	}
}
