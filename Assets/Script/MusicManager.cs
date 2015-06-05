using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource mainBG;
	public AudioSource tensionLayer;
	public float[] volumes;


	void Start () {
	
		// getting musics
		mainBG = transform.FindChild ("mainBackground").GetComponent<AudioSource> ();
		tensionLayer = transform.FindChild ("tensionLayer").GetComponent<AudioSource> ();

		// triggering musics
		mainBG.Play ();
		tensionLayer.Play();

	}

	void Update () {
		mainBG.volume = volumes [0];
		tensionLayer.volume = volumes [1];
	}
}
