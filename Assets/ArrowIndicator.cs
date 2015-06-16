using UnityEngine;
using System.Collections;

public class ArrowIndicator : MonoBehaviour {

    public Quaternion rotationInitial;
    public Vector3 initalScale;
    public GameObject boss;
    public GameObject target;

	// Use this for initialization
	void Start () {
        rotationInitial = transform.rotation;
        initalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.boss != null && this.boss == null)
        {
            this.boss = GameManager.instance.boss;
        }

        if (this.boss != null)
        {
            this.transform.position = boss.transform.position;
        }
        if (target != null)
        {
            Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z);
            float scaleDistance = direction.magnitude / 100;
            //print("distance :" + scaleDistance);
            transform.localScale = initalScale * scaleDistance;
            //transform.rotation = rotationInitial;
            //Vector2 z=new Vector2(0,1);
            //Vector3 z=new Vector3(0,0,1);
            //Vector2 direction = new Vector2(GameManager.instance.coffeeTable.transform.position.x - transform.position.x, GameManager.instance.coffeeTable.transform.position.z - transform.position.z).normalized;
            //Vector3 direction = GameManager.instance.coffeeTable.transform.position - transform.position;
            
            //print("Angle entre z et direction : "+Vector2.Angle(z, direction));
            //transform.rotation = Quaternion.FromToRotation(z, direction);
            //transform.rotation = Quaternion.Euler(0,-Vector2.Angle(z, direction),0);
            transform.LookAt(target.transform.position);
            //transform.rotation = new Quaternion(0,1,0,Mathf.Cos(Vector2.Angle(z, direction)/2));
            //transform.Rotate(0, 0, Vector2.Angle(z, direction));
        }
	
	}
}
