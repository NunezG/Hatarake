using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour {

	public GameObject WorkBoxTriggerPrefab;
	
	public GameObject ChillRoomsTriggerPrefab;

	public void addTrigger(int x,int z, int width, int height, Cell.CellType cellType)
	{
		GameObject newTrigger;

		switch(cellType)
		{
		case Cell.CellType.Coffeeroom :
			newTrigger = Instantiate(ChillRoomsTriggerPrefab) as GameObject;
		//newTrigger.transform.position
			break;
		case Cell.CellType.Bathroom :
			newTrigger = Instantiate(ChillRoomsTriggerPrefab) as GameObject;
			//newTrigger.transform.position
			break;
		case Cell.CellType.Bossroom :
			newTrigger = Instantiate(WorkBoxTriggerPrefab) as GameObject;
			break;
		default:
			newTrigger = null;
			break;
			
		}

		if (newTrigger != null)
		{
			newTrigger.transform.parent = transform;

			newTrigger.transform.localPosition = new Vector3 (x+(float)(width-1)/2.0f, 0f,z+(float)(height-1)/2.0f);

			newTrigger.GetComponent<Collider>().bounds.min.Set( x, 0, z);
			newTrigger.GetComponent<Collider>().bounds.max.Set(x+width, 0, z+height);

			newTrigger.transform.localScale = new Vector3 (width, 0f, height);
		}
	}	
}
