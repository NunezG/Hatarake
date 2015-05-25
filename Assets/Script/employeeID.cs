using UnityEngine;
using System.Collections;

public class employeeID : MonoBehaviour {

	private bool profileUpdated;
	private string pictureChild = "picture";
	private string[] pictureParts = {"figure", "hair", "eyes", "nose", "mouth", "top", "fond", "frame"};

	public GameObject employeePrefab;
	private EmployeeData employeeInfos;


	//Mise en commun de toutes les données, choix des vetements et de l'apparence
	void Update () {

		//Show prefab's profile
		if(gameObject.activeInHierarchy){

			//Update des infos si pas déjà fait
			if(!profileUpdated){

				try{
					//recupération des données
					employeeInfos = employeePrefab.GetComponent<Employe>().data;

					//Rendre visible
					gameObject.SetActive(true);

					//update ID name, first name and sex
					transform.FindChild("ID").FindChild("FamilyName_label").GetComponent<TextMesh>().text = employeeInfos.surname.ToUpper();
					transform.FindChild("ID").FindChild("Name_label").GetComponent<TextMesh>().text = employeeInfos.firstName.ToUpper();
					transform.FindChild("ID").FindChild("Sex_label").GetComponent<TextMesh>().text = (employeeInfos.isMale) ? "Homme" : "Femme";

					//Update hobbies
					for(int i = 0 ; i<5 ; i++){
						transform.FindChild("Hobbies").FindChild("hobby"+(i+1)).GetComponent<TextMesh>().text = employeeInfos.hobbies[i];
					}

					//update picture colors
					transform.FindChild(pictureChild).FindChild(pictureParts[0]).GetComponent<SpriteRenderer>().color = employeeInfos.skinColor;
					transform.FindChild(pictureChild).FindChild(pictureParts[1]).GetComponent<SpriteRenderer>().color = employeeInfos.hairColor;
					transform.FindChild(pictureChild).FindChild(pictureParts[3]).GetComponent<SpriteRenderer>().color = employeeInfos.skinColor;
					transform.FindChild(pictureChild).FindChild(pictureParts[5]).GetComponent<SpriteRenderer>().color = employeeInfos.topColor;

					//update images
					//transform.FindChild(pictureChild).FindChild(pictureParts[2]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;
					//transform.FindChild(pictureChild).FindChild(pictureParts[4]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;
					//transform.FindChild(pictureChild).FindChild(pictureParts[6]).GetComponent<SpriteRenderer>().color = employeePrefab.;
					//transform.FindChild(pictureChild).FindChild(pictureParts[7]).GetComponent<SpriteRenderer>().color = employeePrefab.skinColor;

					profileUpdated = true;
				}
				catch{
					profileUpdated = false;
					gameObject.SetActive(false);
					Debug.Log ("Erreur lors du chargement infos de l'employé");
				}
			}

			// Boucle d'affichage normale
			else{}
		}

		// Rendre invisible aux yeux de tous
		else{
			profileUpdated = false;
			gameObject.SetActive(false);
		}
		
	}
}
