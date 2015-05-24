using UnityEngine;
using System.Collections;

public class employeeID : MonoBehaviour {

	EmployeeData data;

	public string firstName;
	public string surname;
	public bool isMale;

	public int[] physicalCaracteristics; // 0:figure, 1:hair, 2:eyes, 3:nose, 4:mouth, 5:top, 6:background

	public Color[] skinColors;
	public Color[] hairColors;
	public Color[] topColors;
	public Color[] bottomColors;

	public Color skinColor;
	public Color hairColor;
	public Color topColor;
	public Color bottomColor;

	public string[] hobbies;


	//Mise en commun de toutes les données, choix des vetements et de l'apparence
	void Start () {

		skinColors = new Color[]{new Color(255, 0, 0)};
		hairColors = new Color[]{new Color(0, 255, 0)};
		topColors = new Color[]{new Color(0, 0, 255)};
		bottomColors = new Color[]{new Color(255, 255, 0)};

		data = GetComponentInParent<Employe> ().data;


		//Randomizing caracteristics
		physicalCaracteristics = new int[7];
		for (int i = 0; i < physicalCaracteristics.Length; i++) {

			physicalCaracteristics[i] = (int) Random.Range(0, 9);

		}

		// If the employee is a woman, shift right index by 8
		if(!data.isMale) {

			for (int i = 0; i < physicalCaracteristics.Length; i++) {
				
				physicalCaracteristics[i] += 8;
				
			}

		}

		//Assigning colors
		int cho = (int)Random.Range(0, skinColors.Length);
		Debug.Log (cho);
		skinColor = skinColors [cho];


		cho = (int)Random.Range(0, hairColors.Length);
		hairColor = hairColors [cho];

		cho = (int)Random.Range(0, topColors.Length);
		topColor = topColors [cho];

		cho = (int)Random.Range(0, bottomColors.Length);
		bottomColor = bottomColors [cho];

		//Freezing correct frame for caracteristics


		//Assigning hobbies
		for (int i = 0; i<5; i++) {

			transform.FindChild ("Hobbies").FindChild ("hobby"+(i+1)).GetComponent<TextMesh>().text = data.hobbies [i];

		}


		
	}
}
