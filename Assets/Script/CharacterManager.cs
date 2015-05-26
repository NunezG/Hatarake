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
	float productionParSec; //(somme de chaque vitesseDeTravail des employés)
	float objectifNiveau; //(production à atteindre pour atteindre le niveau suivant)

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
        this.gameObject.GetComponent<GameManager>().SetBoss(boss);
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


    public void spawnOneBoxieInElevator(int floorNb)
    {
        GameObject floor = GameObject.Find("Office floor n" + floorNb);

        Box[] boxes = floor.GetComponentsInChildren<Box>();

        GameObject boxie = (GameObject)Instantiate(boxiePrefab);

        boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

        boxie.name = boxiePrefab.name + boxies.Count;

        boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);

        foreach (Box box in boxes)
        {
            if (box.CompareTag("Box") && box.assigne == false)
            {
                boxie.GetComponent<Employe>().floor = floor;
                boxie.GetComponent<Employe>().setBox(box.gameObject);
                box.assigne = true;
                break;
            }
        }
        boxie.transform.localScale = boxie.transform.localScale * this.gameObject.GetComponent<LevelManager>().getOfficeInstance()[floorNb].transform.localScale.x;

        boxie.transform.position = this.gameObject.GetComponent<LevelManager>().getOfficeInstance()[floorNb].transform.position;
        //boxie.transform.Translate(40, boxie.GetComponent<Collider>().bounds.extents.y, 0);
        boxie.transform.Translate(floor.transform.Find("Elevator Cell 4, 0").position.x, boxie.GetComponent<Collider>().bounds.extents.y, 0);
        boxie.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        boxies.Add(boxie);
    }				
    
}
