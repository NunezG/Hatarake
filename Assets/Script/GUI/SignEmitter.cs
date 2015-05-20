using UnityEngine;
using System.Collections;

public class SignEmitter : MonoBehaviour {


    public Material[] materials = new Material[6];
    public static Object SignGenerator=Resources.Load("SignGenerator");

    ParticleSystemRenderer partiSysRender;
    ParticleSystem partiSys;
    public SignType type;
    SpriteRenderer spriteRenderer;
    public float volume, alpha;

    public static SignEmitter Create(float volume, Vector3 position, SignType type)
    {
        Vector3 pos = new Vector3(position.x, position.y + 10, position.z);
        GameObject newObject = Instantiate(SignGenerator) as GameObject;
        newObject.transform.position = pos;
        SignEmitter yourObject = newObject.GetComponent<SignEmitter>();
        yourObject.type = type;
        yourObject.partiSysRender = newObject.GetComponent<ParticleSystemRenderer>();
        yourObject.partiSys = newObject.GetComponent<ParticleSystem>();
        //do additional initialization steps here

        return yourObject;
    }
	// Use this for initialization
	void Start () {
        

	    switch (type)
        {
            case SignType.Hatarake:
                //partiSysRender.material=material;
                break;
            case SignType.Coffee:
                partiSysRender.material = materials[0];
                break;
            case SignType.Death:
                partiSysRender.material = materials[1];
                break;
            case SignType.Facebook:
                partiSysRender.material = materials[2];
                break;
            case SignType.Glande:
                partiSysRender.material = materials[3];
                break;
            case SignType.Work:
                partiSysRender.material = materials[4];
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!partiSys.IsAlive())
            Destroy(this.gameObject);
	}
}
