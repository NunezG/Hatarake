using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioSource mainBG;
	public float[] volumes;
	public GameManager GM;


	void Start () {
	
		// getting musics
		mainBG = transform.FindChild ("mainBackground").GetComponent<AudioSource> ();
		GM = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		mainBG.volume = 0.0f;

		// triggering musics
		mainBG.Play ();

	}

	void Update () {
		mainBG.volume = Mathf.Lerp(mainBG.volume, volumes [0], 0.05f);
	}
}