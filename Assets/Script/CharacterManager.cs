using UnityEngine;
using System.Collections;
//using RAIN.Minds;
using RAIN.Core;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
		
	public GameObject boxiePrefab;
	public GameObject bossPrefab;

	public int nombreBoxies = 10;
	int nombreDecrasseurs;
	int nombreMarketeux;
	float production; //(production actuel)
	float productionParSec; //(somme de chaque vitesseDeTravail des employ�s)
	float objectifNiveau; //(production � atteindre pour atteindre le niveau suivant)

	public List<GameObject>  boxes;
  //  public GameObject[] workingHelp;

	List<GameObject> boxies;
	GameObject boss;

	public void Spawn()
	{
        GameObject spawn = GameObject.Find("spawnBoss");
		GameObject floor = GameObject.Find ("Office floor n0");			

		Box[] boxes = floor.GetComponentsInChildren<Box> ();
		
		boss =(GameObject)Instantiate (bossPrefab);
        boss.transform.localScale = boss.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;

        boss.transform.position = spawn.transform.position;
        boss.transform.Translate(0,boss.GetComponent<Collider>().bounds.extents.y,0);

		boxies = new List<GameObject>();

		// create Player
		for (int i = 0; i < nombreBoxies; i++) 
		{
			GameObject tempObject = (GameObject)Instantiate (boxiePrefab);

            tempObject.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

            tempObject.name = boxiePrefab.name + i;

            tempObject.GetComponent<Employe>().tMemory.SetItem("auTravail", true);

			foreach(Box box in boxes )
			{
				if (box.CompareTag("Box") && box.assigne == false)
				{
					tempObject.GetComponent<Employe>().floor = floor;
					tempObject.GetComponent<Employe>().setBox(box.gameObject);
                    //tempObject.GetComponent<Employe>().tMemory.SetItem("myTarget", box.gameObject);
                    //tempObject.GetComponent<Employe>().tMemory.SetItem("enDeplacement", true);

					//tempObject.GetComponent<Employe>().setTaget(box);
					box.assigne = true;
					break;
				}
			}
            tempObject.transform.localScale = tempObject.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;

			tempObject.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.position;
			tempObject.transform.Translate(Random.Range(0,40),tempObject.GetComponent<Collider>().bounds.extents.y,Random.Range(0,30));

            tempObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            tempObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

			boxies.Add(tempObject);
			}				
	}


    public void spawnOneBoxieInElevator()
    {
        GameObject spawn = GameObject.Find("spawnBoss");
        GameObject floor = GameObject.Find("Office floor n0");

        Box[] boxes = floor.GetComponentsInChildren<Box>();

        GameObject tempObject = (GameObject)Instantiate(boxiePrefab);

        tempObject.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

        tempObject.name = boxiePrefab.name + boxies.Count;

        tempObject.GetComponent<Employe>().tMemory.SetItem("auTravail", true);

        foreach (Box box in boxes)
        {
            if (box.CompareTag("Box") && box.assigne == false)
            {
                    tempObject.GetComponent<Employe>().floor = floor;
                    tempObject.GetComponent<Employe>().setBox(box.gameObject);
                    box.assigne = true;
                    break;
            }
        }
        tempObject.transform.localScale = tempObject.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;

        tempObject.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.position;
        tempObject.transform.Translate(40, tempObject.GetComponent<Collider>().bounds.extents.y, 0);

        tempObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        tempObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        boxies.Add(tempObject);
        }				
    
}
