using UnityEngine;
using System.Collections;

public class ArrowIndicator : MonoBehaviour {

    public Quaternion rotationInitial;
    public Vector3 initalScale;
    public GameObject boss;
    public GameObject target;
    public GameObject sprite;
    public float minScale = 1.6f;
    public int spriteActiveThreshold = 15,logFactor =2;
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
            if (direction.magnitude < spriteActiveThreshold && sprite.activeInHierarchy)
            {
                sprite.SetActive(false);
            }
            else if (direction.magnitude > spriteActiveThreshold && !sprite.activeInHierarchy)
            {
                sprite.SetActive(true);
            }
            float scaleDistance = direction.magnitude / 100;
            //print("distance :" + scaleDistance);
            transform.localScale = initalScale * (Mathf.Log10(scaleDistance + 1)*logFactor);
            if (transform.localScale.x < minScale)
            {
                transform.localScale = new Vector3(minScale, minScale, minScale);
            }
            transform.LookAt(target.transform.position);
        }
	
	}
}
