using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource mainBG;
	public AudioSource tensionLayer;
	public float[] volumes;
	public GameManager GM;


	void Start () {
	
		// getting musics
		mainBG = transform.FindChild ("mainBackground").GetComponent<AudioSource> ();
		tensionLayer = transform.FindChild ("tensionLayer").GetComponent<AudioSource> ();
		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		// triggering musics
		mainBG.Play ();
		tensionLayer.Play();

	}

	void Update () {
		mainBG.volume = volumes [0];
		if(GM.workingIsActuallyUsefull)
			tensionLayer.volume = Mathf.Lerp(tensionLayer.volume, volumes [1], 0.05f);
		else
			tensionLayer.volume = Mathf.Lerp(tensionLayer.volume, 0.0f, 0.05f);
	}
}
