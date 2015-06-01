using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextScene : MonoBehaviour {

	public string m_scene;
	public Image enjmin;

	void Start(){

	}

	void Update () {

		if (Time.time > 5) {
			Application.LoadLevel (m_scene);
		}

		else if (Time.time > 2.5f) {
			float alpha = enjmin.color.a - 1.0f;
			enjmin.color = new Color(enjmin.color.r,  enjmin.color.g, enjmin.color.b, alpha);

		}

		else if (Time.time < 2.5f) {
			float alpha = enjmin.color.a + 1.0f;
			enjmin.color = new Color(enjmin.color.r,  enjmin.color.g, enjmin.color.b, alpha);
			
		}
	}
}