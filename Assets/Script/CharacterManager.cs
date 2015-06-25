using UnityEngine;
using System.Collections;
//using RAIN.Minds;
using RAIN.Core;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
		
	public GameObject boxiePrefab;
    public GameObject decrasseurPrefab;
	public GameObject bossPrefab;

	public int nombreBoxies = 10;
    public int nbDecrasseur;
	int nombreDecrasseurs;
	int nombreMarketeux;
	float production; //(production actuel)
	float productionParSec; //(somme de chaque vitesseDeTravail des employés)
	float objectifNiveau; //(production à atteindre pour atteindre le niveau suivant)

    public List<GameObject> boxies = new List<GameObject>();
    public List<GameObject> decrasseurs = new List<GameObject>();
    private GameObject boss;

    public bool freshMeatThreshold;

    public void Start()
    {
        boss = (GameObject)Instantiate(bossPrefab);
        GameManager.instance.SetBoss(boss);

    }

	public void Spawn()
	{
        GameObject spawn = GameObject.Find("spawnBoss");
		GameObject floor = GameObject.Find ("Office floor n0");

        boss.transform.localScale = boss.transform.localScale * floor.transform.localScale.x;
        boss.transform.position = floor.transform.position;
        boss.transform.Translate(spawn.transform.position.x, boss.GetComponent<Collider>().bounds.extents.y, spawn.transform.position.z);
        boss.transform.LookAt(GameObject.Find("targetToFace").transform.position);
        boss.GetComponent<Boss>().setTarget(spawn.transform.position);

        GameObject[] corridorsCell = GameObject.FindGameObjectsWithTag("Corridor");
        InteractWithEmployee[] targets = floor.GetComponentsInChildren<InteractWithEmployee>();

		// create Player
		for (int i = 0; i < nombreBoxies; i++) 
		{
			GameObject boxie = (GameObject)Instantiate (boxiePrefab);

            boxie.GetComponent<Employe>().floor = floor;
            boxie.GetComponent<Employe>().SetEmployeeLocations();
           // boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);
            boxie.name = boxiePrefab.name + i;

            foreach (InteractWithEmployee target in targets)
			{
                if (target.CompareTag("Box") && target.assigne == false)
				{
                    boxie.GetComponent<Employe>().setBox(target.gameObject);
                    boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);
                   // boxie.GetComponent<Employe>().tMemory.SetItem("myTarget", box.gameObject);
                    target.assigne = true;
					break;
				}
			}
            boxie.transform.localScale = boxie.transform.localScale * floor.transform.localScale.x;

            boxie.transform.position = floor.transform.position;
            int rdmIndex = Random.Range(0, corridorsCell.Length);
            boxie.transform.Translate(corridorsCell[rdmIndex].transform.position.x, boxie.GetComponent<Collider>().bounds.extents.y, corridorsCell[rdmIndex].transform.position.z);

            boxies.Add(boxie);
		}
        //SpawnDecrasseur();
	}

    public bool tutoDecrasseurLock = true;
    public void SpawnDecrasseur()
    {
        GameObject floor = GameObject.Find("Office floor n0");			
        GameObject[] corridorsCell = GameObject.FindGameObjectsWithTag("Corridor");
        GameObject decrasseur = (GameObject)Instantiate(decrasseurPrefab);
        decrasseur.GetComponent<Decrasseur>().floor = floor;
        decrasseur.GetComponent<Decrasseur>().SetEmployeeLocations();
        //decrasseur.GetComponent<Rigidbody>().mass = Random.Range(1, 100);
        decrasseur.transform.localScale = decrasseur.transform.localScale * floor.transform.localScale.x;
        decrasseur.transform.position = floor.transform.position;
        int rdmInd = Random.Range(0, corridorsCell.Length);
        decrasseur.transform.Translate(floor.transform.Find("Elevator Cell 4, 0").position.x, decrasseur.GetComponent<Collider>().bounds.extents.y, 0);
        decrasseurs.Add(decrasseur);
        
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
        InteractWithEmployee[] boxes = boxie.GetComponent<Employe>().floor.GetComponentsInChildren<InteractWithEmployee>();
        foreach (InteractWithEmployee box in boxes)
        {
            if (box.CompareTag("Box") && box.assigne == false)
            {
                boxie.GetComponent<Employe>().setBox(box.gameObject);
                boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);
                box.assigne = true;

                //boxie.GetComponent<Employe>().setTaget(box);
                break;
            }
        }

        boxie.transform.localScale = boxie.transform.localScale * gameObject.GetComponent<LevelManager>().getOfficeInstance()[0].transform.localScale.x;
        boxie.transform.position = new Vector3(boxie.GetComponent<Employe>().floor.transform.Find("Elevator Cell 4, 0").position.x, boxie.GetComponent<Collider>().bounds.extents.y, 0);
       
        boxie.name = boxiePrefab.name + (boxies.Count);

        boxies.Add(boxie);
    }

    public GameObject GenerateOneBoxieForHire()
    {
        GameObject boxie = (GameObject)Instantiate(boxiePrefab);

        int floorNb = 0;
        GameObject floor = GameObject.Find("Office floor n" + floorNb);
        boxie.GetComponent<Employe>().floor = floor;
        boxie.GetComponent<Employe>().SetEmployeeLocations();

        boxie.transform.Translate(-100, -100, -100);
        //boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);

        return boxie;
    }

    public void SpawnOneBoxieInElevator(int floorNb)
    {
        GameObject floor = GameObject.Find("Office floor n" + floorNb);
        InteractWithEmployee[] targets = floor.GetComponentsInChildren<InteractWithEmployee>();

        GameObject boxie = (GameObject)Instantiate(boxiePrefab);

        boxie.GetComponent<Employe>().floor = floor;
        boxie.GetComponent<Employe>().SetEmployeeLocations();
        // boxie.GetComponent<Rigidbody>().mass = Random.Range(1, 100);
        boxie.name = boxiePrefab.name + (boxies.Count);

        foreach (InteractWithEmployee target in targets)
        {
            if (target.CompareTag("Box") && target.assigne == false)
            {
                boxie.GetComponent<Employe>().setBox(target.gameObject);
                boxie.GetComponent<Employe>().tMemory.SetItem("auTravail", true);
                target.assigne = true;
                break;
            }
        }
        boxie.transform.localScale = boxie.transform.localScale * floor.transform.localScale.x;
        boxie.transform.position = new Vector3(boxie.GetComponent<Employe>().floor.transform.Find("Elevator Cell 4, 0").position.x, boxie.GetComponent<Collider>().bounds.extents.y, 0);

        boxies.Add(boxie);
    }

    public void sendBoxieToHell(GameObject boxie)
    {
        if (boxie.GetComponent<Employe>().boxDeTravail != null) boxie.GetComponent<Employe>().boxDeTravail.GetComponent<InteractWithEmployee>().assigne = false;
        boxies.Remove(boxie);
        Destroy(boxie);
    }

    void Update()
    {
        if (boxies.Count == 0 && freshMeatThreshold)
         {
              SpawnOneBoxieInElevator(0);
         }

        if (boxies.Count / 10 > nbDecrasseur)
        {
            nbDecrasseur++;
            SpawnDecrasseur();
        }

        if (nbDecrasseur==1 && !GameManager.instance.hiringTime && tutoDecrasseurLock)
        {
            tutoDecrasseurLock = false;
            GameManager.instance.decrasseurJustArrived = true;
            GameManager.instance.tutoIsOn = true;
        }
    }
}