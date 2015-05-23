using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource ambiance;
	public AudioSource mainBG;
	public AudioSource[] tensionsLayers;


	void Start () {
	
		ambiance.Play ();
		mainBG.Play ();

	}
	

	void Update () {
	
	}
}
