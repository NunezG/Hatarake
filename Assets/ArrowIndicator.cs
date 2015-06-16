using UnityEngine;
using System.Collections;

public class ArrowIndicator : MonoBehaviour {

    Quaternion rotationInitial;

	// Use this for initialization
	void Start () {
        rotationInitial = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.coffeeTable != null)
        {
            transform.rotation = rotationInitial;
            Vector3 z=new Vector3(0,0,1);
            //Vector2 direction = new Vector2(GameManager.instance.coffeeTable.transform.position.x - transform.position.x, GameManager.instance.coffeeTable.transform.position.z - transform.position.z).normalized;
            Vector3 direction = GameManager.instance.coffeeTable.transform.position - transform.position;
            
            print("Angle entre z et direction : "+Vector2.Angle(z, direction));
            transform.rotation = Quaternion.FromToRotation(z, direction);
            //transform.Rotate(0, 0, Vector2.Angle(z, direction));
        }
	
	}
}
