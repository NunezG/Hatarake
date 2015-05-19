using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour
{
    public static Object hatarake = Resources.Load("hatarake!");
    public static Object coffee = Resources.Load("coffee");
    public static Object death = Resources.Load("death");
    public static Object facebook = Resources.Load("facebook");
    public static Object glande = Resources.Load("goingToGlande");
    public static Object work = Resources.Load("work");

    SpriteRenderer spriteRenderer;
    public float volume, alpha;

    public static Sign Create(float volume, Vector3 position,SignType type)
    {
        Vector3 pos = new Vector3(position.x, position.y + 10, position.z);
        GameObject newObject ;

        switch (type)
        {
            case SignType.Hatarake:
                newObject = Instantiate(hatarake) as GameObject;
                break;
            case SignType.Coffee:
                newObject = Instantiate(coffee) as GameObject;
                break;
            case SignType.Death:
                newObject = Instantiate(death) as GameObject;
                break;
            case SignType.Facebook:
                newObject = Instantiate(facebook) as GameObject;
                break;
            case SignType.Glande:
                newObject = Instantiate(glande) as GameObject;
                break;
            case SignType.Work:
                newObject = Instantiate(work) as GameObject;
                break;
            default:
                newObject = Instantiate(hatarake) as GameObject;
                break;
        }
        Sign yourObject = newObject.GetComponent<Sign>();
        yourObject.alpha = 255;
        yourObject.volume = volume;
        newObject.transform.position = pos;
        //do additional initialization steps here

        return yourObject;
    }

    // Use this for initialization
    void Start()
    {
        this.transform.localScale = this.transform.localScale * volume * 10;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //spriteRenderer.material.SetColor()
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha > 0)
        {

            //alpha = Mathf.Lerp(255f, 0f, (1 / volume) * Time.deltaTime*400);
            alpha = alpha -  Time.deltaTime * 400;

            print("ALPHA : " + alpha);
            Color color = new Color(1, 1, 1, alpha);
            spriteRenderer.color = color;
            print("" + spriteRenderer.color.a);
        }
        else
        {
            Destroy(this.gameObject);
        }



    }
}
