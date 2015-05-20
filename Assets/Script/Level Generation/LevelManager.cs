using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Office officePrefab;
	
	private List<Office> officeFloors = new List<Office>();

    public int nbCoffeeRooms, nbBathRooms, nbBoxes;
	public void BeginGame () {
        int nbFloors = 3;
        for (int i = 0; i < nbFloors; i++) {
            Office officeInstance = Instantiate(officePrefab) as Office;
            officeInstance.name = "Office floor n" + i;
            if(i==0)
                officeInstance.init(i, 1, nbCoffeeRooms, nbBathRooms, nbBoxes);
            else
                officeInstance.init(i, 0, nbCoffeeRooms, nbBathRooms, nbBoxes+6);
            officeFloors.Add(officeInstance);
        }

	}
	
	private void RestartGame () {
        for (int i = 0; i < officeFloors.Count;i++ )
            Destroy(officeFloors[i].gameObject);
		BeginGame();
	}
	private void Start () {
		//BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			//RestartGame();
		}
	}

    public List<Office> getOfficeInstance()
    {
        return officeFloors;
	}

}
