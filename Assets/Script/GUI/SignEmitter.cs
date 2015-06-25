using UnityEngine;
using System.Collections;

public class SignEmitter : MonoBehaviour {


    public Material[] materials = new Material[10];
    public static Object SignGeneratorEmploye = Resources.Load("SignGenerator");
    public static Object SignGeneratorBoss = Resources.Load("HatarakeGenerator");

    ParticleSystemRenderer partiSysRender;
    ParticleSystem partiSys;
    public SignType type;
    SpriteRenderer spriteRenderer;
    public float  size;
    public GameObject boss;

    public static SignEmitter Create(Vector3 position, SignType type,float size)
    {
        Vector3 pos = new Vector3(position.x, position.y + 10, position.z);
        GameObject newObject;
        if (type != SignType.Hatarake)
        {
            newObject = Instantiate(SignGeneratorEmploye) as GameObject;

        }
        else
        {
            newObject = Instantiate(SignGeneratorBoss) as GameObject;


            //newObject.transform.localScale = newObject.transform.localScale * size * 50;
        }

        newObject.transform.position = pos;
        SignEmitter yourObject = newObject.GetComponent<SignEmitter>();
        yourObject.type = type;
        yourObject.size = size;
        yourObject.partiSysRender = newObject.GetComponent<ParticleSystemRenderer>();
        yourObject.partiSys = newObject.GetComponent<ParticleSystem>();
        if(type==SignType.Hatarake)
            yourObject.partiSys.startSize = yourObject.partiSys.startSize*size;
        //do additional initialization steps here

        return yourObject;
    }
	// Use this for initialization
	void Start () {

        boss = GameObject.FindGameObjectWithTag("Boss");
	    switch (type)
        {
            case SignType.Hatarake:
                //partiSysRender.material=material;
                partiSys.startSize = 50 * size;
                break;
            case SignType.Cellphone:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[0];
                break;
            case SignType.Coffee:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[1];
                break;
            case SignType.Death:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[2];
                break;
            case SignType.Drink:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[3];
                break;
            case SignType.Facebook:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[4];
                break;
            case SignType.GoingToGlande:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[5];
                break;
            case SignType.GoingToWork:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[6];
                break;
            case SignType.Photocopier:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[7];
                break;
            case SignType.Toilet:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[8];
                break;
            case SignType.Tv:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[9];
                break;
            case SignType.Work:
                boss.GetComponent<Boss>().addBubble();
                partiSysRender.material = materials[10];
                break;
            default:
                //partiSysRender.material = materials[10];
                break;
        }
	}

	void Update () {

        if (!partiSys.IsAlive() )
        {
            //if( type != SignType.Hatarake)
                //boss.GetComponent<Boss>().addBubble();

            Destroy(this.gameObject);
        }

		//if (partiSys.time > 0.6f && type != SignType.Hatarake) {
			//transform.position = Vector3.Lerp(transform.position, boss.transform.position, 0.2f);
		//}

	}
}
