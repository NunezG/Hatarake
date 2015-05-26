using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class employeeID : MonoBehaviour {

	private bool profileUpdated;
	private string pictureChild = "picture";
	private string[] pictureParts = {"face", "hair", "eyes", "nose", "mouth", "top", "bg", "frame"};

	public GameObject employeePrefab;
	public GameObject employeeClone;
	private EmployeeData employeeInfos;


	//Mise en commun de toutes les données, choix des vetements et de l'apparence
	void Update () {

		//Show prefab's profile
		if (employeePrefab != null) {

			//Check if not dead
			if(!employeePrefab.activeInHierarchy){
				employeePrefab = null;
			}

			//Récupération de la pancarte
			Transform profile = transform.FindChild ("profile");

			//Update des infos la première boucle
			if (!profileUpdated) {

				//Clone Prefab to compare later
				employeeClone = employeePrefab;

				//get infos from employee
				employeeInfos = employeePrefab.GetComponent<Employe> ().data;

				//set the panel visible
				profile.gameObject.SetActive(true);

				//update ID name, first name and sex
				profile.FindChild ("familyName").GetComponent<Text> ().text = employeeInfos.surname.ToUpper ();
				profile.FindChild ("firstName").GetComponent<Text> ().text = employeeInfos.firstName.ToUpper ();
				//profile.FindChild("Sex_label").GetComponent<TextMesh>().text = (employeeInfos.isMale) ? "Homme" : "Femme";

				//Update hobbies
				profile.FindChild ("Obi-Wan").GetComponent<Text> ().text = "- " + employeeInfos.hobbies [0].ToUpper ();
				for (int i = 1; i<5; i++) {
					profile.FindChild ("hobby" + (i + 1)).GetComponent<Text> ().text = "- " + employeeInfos.hobbies [i].ToUpper ();
				}

				//update picture colors
				profile.FindChild (pictureParts [0]).GetComponent<Image> ().color = employeeInfos.skinColor;
				profile.FindChild (pictureParts [1]).GetComponent<Image> ().color = employeeInfos.hairColor;
				profile.FindChild (pictureParts [3]).GetComponent<Image> ().color = employeeInfos.skinColor;
				profile.FindChild (pictureParts [5]).GetComponent<Image> ().color = employeeInfos.topColor;

				//update images

				//gameObject.GetComponent<Animator> ().Play("choice", -1, (int)Random.Range (0,18)/18.0f);
//					transform.FindChild(pictureChild).FindChild(pictureParts[2]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;
//					transform.FindChild(pictureChild).FindChild(pictureParts[4]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;
//					transform.FindChild(pictureChild).FindChild(pictureParts[6]).GetComponent<SpriteRenderer>().color = employeePrefab.;
//					transform.FindChild(pictureChild).FindChild(pictureParts[7]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;

				//motivationMAX & fatigueMax
				profile.FindChild("motivation").GetComponent<Slider>().maxValue = employeePrefab.GetComponent<Employe>().data.motivationMax;
				profile.FindChild("fatigue").GetComponent<Slider>().maxValue = employeePrefab.GetComponent<Employe>().data.fatigueMAX;

				//Profile has been updated
				profileUpdated = true;
			}

			// Boucle d'affichage normale
			else {

				//update de la motivation et de la fatigue
				profile.FindChild("motivation").GetComponent<Slider>().value = employeePrefab.GetComponent<Employe>().data.motivation;
				profile.FindChild("fatigue").GetComponent<Slider>().value = employeePrefab.GetComponent<Employe>().data.fatigue;

				//if new focus
				if(employeeClone != employeePrefab){

					profileUpdated = false;

				}

			}
		}

		// La pancarte n'est pas affichée
		else {
			transform.FindChild ("profile").gameObject.SetActive(false);
			profileUpdated = false;
		}
		
	}
}
