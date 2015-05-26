using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class employeeID : MonoBehaviour {

	private bool profileUpdated;
	private string pictureChild = "picture";
	// 0:figure, 1:hair, 2:eyes, 3:nose, 4:mouth, 5:top, 6:background
	private string[] pictureParts = {"face", "hair", "eyes", "nose", "mouth", "top", "bg"}; 

	public GameObject currentEmployee;
	public GameObject previousEmployee;


	public Sprite[] bgs;
	public Sprite[] faces;
	public Sprite[] hairs;
	public Sprite[] eyes;
	public Sprite[] noses;
	public Sprite[] mouths;
	public Sprite[] tops;
	private Sprite[][] sprites;

	private EmployeeData employeeInfos;

	void Start(){

		sprites = new Sprite[][]{faces, hairs, eyes, noses, mouths, tops, bgs};

	}

	//Mise en commun de toutes les données, choix des vetements et de l'apparence
	void Update () {

		//Show prefab's profile
		if (currentEmployee != null) {

			//Check if target is active
			if(!currentEmployee.activeInHierarchy){
				currentEmployee = null;
			}

			//Récupération de la pancarte
			Transform profile = transform.FindChild ("profile");

			//Update des infos la première boucle
			if (!profileUpdated) {

				//Clone Prefab to compare later
				previousEmployee = currentEmployee;

				//get infos from employee
				employeeInfos = currentEmployee.GetComponent<Employe> ().data;

				//set the panel visible
				profile.gameObject.SetActive(true);

				//update ID name, first name and sex
				profile.FindChild ("familyName").GetComponent<Text> ().text = employeeInfos.surname.ToUpper ();
				profile.FindChild ("firstName").GetComponent<Text> ().text = employeeInfos.firstName.ToUpper ();

				//Update hobbies
				profile.FindChild ("Obi-Wan").GetComponent<Text> ().text = "- " + employeeInfos.hobbies [0].ToUpper ();
				for (int i = 1; i<5; i++) {
					profile.FindChild ("hobby" + (i + 1)).GetComponent<Text> ().text = "- " + employeeInfos.hobbies [i].ToUpper ();
				}

				//choosing images
				for(int i = 0 ; i < 7 ; i++){
					profile.FindChild (pictureParts [i]).GetComponent<Image> ().sprite = sprites[i][employeeInfos.physicalCaracteristics[i]];
				}

				//choosing colors
				profile.FindChild (pictureParts[0]).GetComponent<Image> ().color = employeeInfos.skinColor;
				profile.FindChild (pictureParts[1]).GetComponent<Image> ().color = employeeInfos.hairColor;
				profile.FindChild (pictureParts[5]).GetComponent<Image> ().color = employeeInfos.topColor;
				profile.FindChild (pictureParts[6]).GetComponent<Image> ().color = employeeInfos.backColor;

				//motivationMAX & fatigueMax
				profile.FindChild("motivation").GetComponent<Slider>().maxValue = currentEmployee.GetComponent<Employe>().data.motivationMax;
				profile.FindChild("fatigue").GetComponent<Slider>().maxValue = currentEmployee.GetComponent<Employe>().data.fatigueMAX;

				//Profile has been updated
				profileUpdated = true;
			}

			// Boucle d'affichage normale
			else {

				//update de la motivation et de la fatigue
				profile.FindChild("motivation").GetComponent<Slider>().value = currentEmployee.GetComponent<Employe>().data.motivation;
				profile.FindChild("fatigue").GetComponent<Slider>().value = currentEmployee.GetComponent<Employe>().data.fatigue;

				//if new focus
				if(previousEmployee != currentEmployee){

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
