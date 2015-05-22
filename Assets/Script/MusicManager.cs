using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource ambiance;
	public AudioSource mainBG;
	public AudioSource[] tensionsLayers;
	public float[] tensionsLayersVolumes;


	void Start () {
	

		// triggering ambiance sound
		ambiance.gameObject.SetActive (true);
		ambiance.Play ();

		// triggering BG music
		mainBG.Play ();

		// triggering tension layers so they are in synch
		for (int i = 0 ; i < tensionsLayers.Length ; i++)
		{
			tensionsLayers[i].Play();
			tensionsLayers[i].volume = 0.0f;
		}

	}

	void Update () {
	
	}
}
