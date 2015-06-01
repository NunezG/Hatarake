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

	//public Box[]  boxes;
  //  public GameObject[] workingHelp;

	List<GameObject> boxies=new List<GameObject>();
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

		// create Player
		for (int i = 0; i < nombreBoxies; i++) 
		{
			GameObject boxie = (GameObject)Instantiate (boxiePrefab);

            boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

            boxie.name = boxiePrefab.name + i;

            boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);

			foreach(Box box in boxes )
			{
				if (box.CompareTag("Box") && box.assigne == false)
				{
					boxie.GetComponent<Employe>().floor = floor;
					boxie.GetComponent<Employe>().setBox(box.gameObject);
                    boxie.GetComponent<Employe>().tMemory.SetItem("myTarget", box.gameObject);
                    boxie.GetComponent<Employe>().tMemory.SetItem("enDeplacement", true);

					//boxie.GetComponent<Employe>().setTaget(box);
					box.assigne = true;
					break;
				}
			}
            boxie.transform.localScale = boxie.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;

			boxie.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.position;
			boxie.transform.Translate(Random.Range(0,40),boxie.GetComponent<Collider>().bounds.extents.y,Random.Range(0,30));

            boxie.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

		}				
	}


    public int GetNumberOfWorkingBoxies()
    {
        int nbWorkingBoxies = 0;
        foreach (GameObject boxie in boxies)
        {
            if (boxie.GetComponent<Employe>().tMemory.GetItem<bool>("auTravail") && !boxie.GetComponent<Employe>().tMemory.GetItem<bool>("enDeplacement"))
                nbWorkingBoxies++;
        }
        return nbWorkingBoxies;
    }

    public int GetTotalNumberOfBoxies()
    {
        return boxies.Count;
    }

    public void AddBoxieFromeHire(GameObject boxie)
    {
        int floorNb = 0;
        GameObject floor = GameObject.Find("Office floor n" + floorNb);
        boxie.GetComponent<Employe>().boxDeTravail.GetComponent<Box>().assigne = true;


        boxie.transform.position = this.gameObject.GetComponent<LevelManager>().getOfficeInstance()[floorNb].transform.position;
        boxie.transform.position=new Vector3(floor.transform.Find("Elevator Cell 4, 0").position.x, boxie.GetComponent<Collider>().bounds.extents.y, 0);
        boxie.name = boxiePrefab.name + (boxies.Count);

        boxie.transform.GetChild(0).GetComponent<SpriteRenderer>().color = boxie.GetComponent<Employe>().data.hairColor;
        boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color= boxie.GetComponent<Employe>().data.topColor;
        boxies.Add(boxie);
    }

    public GameObject GenerateOneBoxieForHire()
    {
        GameObject boxie = (GameObject)Instantiate(boxiePrefab);

        boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

        boxie.name = boxiePrefab.name ;

        boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);

        GameObject floor = GameObject.Find("Office floor n0");	
        Box[] boxes = floor.GetComponentsInChildren<Box>();
        foreach (Box box in boxes)
        {
            if (box.CompareTag("Box") && box.assigne == false)
            {
                boxie.GetComponent<Employe>().floor = floor;
                boxie.GetComponent<Employe>().setBox(box.gameObject);
                boxie.GetComponent<Employe>().tMemory.SetItem("myTarget", box.gameObject);
                boxie.GetComponent<Employe>().tMemory.SetItem("enDeplacement", true);

                //boxie.GetComponent<Employe>().setTaget(box);
                box.assigne = true;
                break;
            }
        }
        boxie.transform.localScale = boxie.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;

        boxie.transform.position = gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.position;
        boxie.transform.Translate(-10*Random.Range(0, 40), boxie.GetComponent<Collider>().bounds.extents.y,-10* Random.Range(0, 30));

        /*
        boxie.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);*/
        
        //boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color = boxie.GetComponent<Employe>().data.topColor;

        //boxies.Add(boxie);
        return boxie;
    }


    public void SpawnOneBoxieInElevator(int floorNb)
    {
        GameObject floor = GameObject.Find("Office floor n" + floorNb);

        Box[] boxes = floor.GetComponentsInChildren<Box>();

        GameObject boxie = (GameObject)Instantiate(boxiePrefab);

        boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

        boxie.name = boxiePrefab.name + boxies.Count;

        boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);
        boxie.GetComponent<Employe>().floor = floor;

        foreach (Box box in boxes)
        {
            if (box.CompareTag("Box") && box.assigne == false)
            {
                boxie.GetComponent<Employe>().setBox(box.gameObject);
                box.assigne = true;
                break;
            }
        }
        boxie.transform.localScale = boxie.transform.localScale * this.gameObject.GetComponent<LevelManager>().getOfficeInstance()[floorNb].transform.localScale.x;

        boxie.transform.position = this.gameObject.GetComponent<LevelManager>().getOfficeInstance()[floorNb].transform.position;
        boxie.transform.Translate(floor.transform.Find("Elevator Cell 4, 0").position.x, boxie.GetComponent<Collider>().bounds.extents.y, 0);
        boxie.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        boxie.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        boxies.Add(boxie);
    }


    public void sendBoxieToHell(GameObject boxie)
    {
        boxie.GetComponent<Employe>().boxDeTravail.GetComponent<Box>().assigne = false;
        boxies.Remove(boxie);
        Destroy(boxie);
    }
}
