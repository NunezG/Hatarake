using UnityEngine;
using System.Collections;
//using RAIN.Minds;
using RAIN.Core;

public class CharacterManager : MonoBehaviour {
		
	public GameObject boxiePrefab;
	public GameObject bossPrefab;

	public int nombreBoxies = 10;
	int nombreDecrasseurs;
	int nombreMarketeux;
	float production; //(production actuel)
	float productionParSec; //(somme de chaque vitesseDeTravail des employés)
	float objectifNiveau; //(production à atteindre pour atteindre le niveau suivant)

	public GameObject[] boxes;

    public GameObject[] workingHelp;

	GameObject[] boxies;
	GameObject boss;






	public void Spawn()
	{

        boxes = GameObject.FindGameObjectsWithTag("Box");
        workingHelp = GameObject.FindGameObjectsWithTag("WorkHelp");


        Repos[] rep = GameObject.FindObjectsOfType<Repos>();

        foreach (Repos repoo in rep)
        {
            print(repoo.transform.name);
            print(repoo.transform.position);

        }

		boss =(GameObject)Instantiate (bossPrefab);
        boss.transform.localScale = boss.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance().transform.localScale.x;

		boss.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance().transform.position;
		boss.transform.Translate(Random.Range(0,4),boss.GetComponent<Collider>().bounds.extents.y,Random.Range(0,3));


		boxies = new GameObject[nombreBoxies];

		// create Player
		for (int i = 0; i < nombreBoxies; i++) 
		{
			GameObject tempObject = (GameObject)Instantiate (boxiePrefab);

            tempObject.name = boxiePrefab.name + i;

			foreach(GameObject box in boxes )
			{
				if (box.GetComponent<Box>().occupe == false)
				{
					tempObject.GetComponent<Employe>().setBox(box);
                    tempObject.GetComponent<Employe>().tMemory.SetItem("workTarget", box.transform.position);
                    tempObject.GetComponent<Employe>().tMemory.SetItem("enDeplacement", true);

					//tempObject.GetComponent<Employe>().setTaget(box);
					box.GetComponent<Box>().occupe = true;
					break;
				}
			}
            tempObject.transform.localScale = tempObject.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance().transform.localScale.x;

			tempObject.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance().transform.position;
			tempObject.transform.Translate(Random.Range(0,40),tempObject.GetComponent<Collider>().bounds.extents.y,Random.Range(0,30));

            tempObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            tempObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

			boxies[i] = tempObject;

			//boxies [i].gameObject.GetComponentInChildren<AIRig>().AI.Motor.DefaultSpeed = 10;
		}				
	}	
}
