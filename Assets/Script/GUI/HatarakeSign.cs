using UnityEngine;
using System.Collections;

public class HatarakeSign : MonoBehaviour {


    public static Object prefab = Resources.Load("hatarake!");

    SpriteRenderer spriteRenderer;
    public float volume, alpha;

    public static HatarakeSign Create(float volume,Vector3 position)
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        HatarakeSign yourObject = newObject.GetComponent<HatarakeSign>();
        yourObject.alpha = 255;
        yourObject.volume = volume;
        yourObject.transform.position = position;
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
        if (alpha != 0){
            alpha = Mathf.Lerp(255f, 0f, (1 / volume) * Time.deltaTime*500);
            print("ALPHA : " + alpha);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }
        else{
            Destroy(this);
        }



	}
}
