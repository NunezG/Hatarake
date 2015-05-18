using UnityEngine;
using System.Collections;

public class HatarakeSign : MonoBehaviour {


    public static Object prefab = Resources.Load("hatarake!");

    SpriteRenderer spriteRenderer;
    public float volume, alpha;

    public static HatarakeSign Create(float volume,Vector3 position)
    {
        Vector3 pos = new Vector3(position.x, position.y + 10, position.z);
        GameObject newObject = Instantiate(prefab) as GameObject;
        HatarakeSign yourObject = newObject.GetComponent<HatarakeSign>();
        yourObject.alpha = 255;
        yourObject.volume = volume;
        newObject.transform.position = pos;
        //do additional initialization steps here

        return yourObject;
    }

	// Use this for initialization
	void Start () {
        this.transform.localScale=this.transform.localScale* volume;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //spriteRenderer.material.SetColor()
	}
	
	// Update is called once per frame
	void Update () {
        if (alpha > 0){

            //alpha = Mathf.Lerp(255f, 0f, (1 / volume) * Time.deltaTime*400);
            alpha = alpha - ( 1/volume) * Time.deltaTime * 400;

            print("ALPHA : " + alpha);
            Color color = new Color(1, 1, 1, alpha);
            spriteRenderer.color =color;
            print(""+spriteRenderer.color.a);
        }
        else{
            Destroy(this);
        }



	}
}
