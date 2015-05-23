using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource backgroundAmbiance;
	public AudioSource music;
	public float tensionLayersVolumes;


	void Start () {

		backgroundAmbiance = transform.FindChild ("ambianceSound").GetComponent<AudioSource> ();
		music = transform.FindChild ("mainBackground").GetComponent<AudioSource> ();

		backgroundAmbiance.Play ();
		music.Play ();
	}
	

	void Update () {
	
	}
}
