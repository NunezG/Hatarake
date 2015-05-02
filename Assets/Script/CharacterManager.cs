using UnityEngine;
using System.Collections;
//using RAIN.Minds;
using RAIN.Core;

public class CharacterManager : MonoBehaviour {
		
	public GameObject boxiePrefab;
	public int nombreBoxies = 10;
	int nombreDecrasseurs;
	int nombreMarketeux;
	float production; //(production actuel)
	float productionParSec; //(somme de chaque vitesseDeTravail des employ�s)
	float objectifNiveau; //(production � atteindre pour atteindre le niveau suivant)

	public GameObject[] boxes;

	public void Spawn()
	{
		GameObject[] boxies = new GameObject[nombreBoxies];

		// create Player
		for (int i = 0; i < nombreBoxies; i++) 
		{
			GameObject tempObject = (GameObject)Instantiate (boxiePrefab);

			tempObject.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance().transform.position;
			tempObject.transform.Translate(Random.Range(0,4),tempObject.GetComponent<Collider>().bounds.extents.y,Random.Range(0,3));

			boxes = GameObject.FindGameObjectsWithTag("Box"); 

			foreach(GameObject box in boxes )
			{
				if (box.GetComponent<Box>().occupe == false)
				{
					tempObject.GetComponent<Employe>().setBox(box);
					tempObject.GetComponent<Employe>().setTaget(box);
					box.GetComponent<Box>().occupe = true;
					break;
				}
			}

			boxies[i] = tempObject;

			//boxies [i].gameObject.GetComponentInChildren<AIRig>().AI.Motor.DefaultSpeed = 10;
		}				
	}	
}
