using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Minds;
using RAIN.Serialization;
using RAIN.Motion;

public class Boss : MonoBehaviour {

   // public GameObject gameManager;
    public bool tutoLock = false;

	//Vector2 position; //peut utiliser son transform
	float vitesseDep;
    public bool moveLocked ;
    public bool hatarakeLocked ;
    public int yellingO_Meter = 0;

    public int maxYellingO_Meter = 50;
    public int gainByBubble = 2;
    public int maxLossByScream = 10;
	//GameObject[] boxies;

	//float jaugeEngueulage; //se remplit quand on appuie sur le boss.

    public  float jaugeEngueulageMax = 15.0f; //se remplit quand on appuie sur le boss.

    public float vitesseJauge = 2.0f; //se remplit quand on appuie sur le boss.

	//float timer = 0;

	bool charge = false;
    Vector3 pos;

	Transform actionArea;
	private RAIN.Memory.BasicMemory tMemory;
    private RAIN.Navigation.BasicNavigator tNav;

	// Use this for initialization
	void Start () {
       // gameManager = GameObject.Find("GameManager");
        if (GameManager.instance.tutoIsOn)
        {
            moveLocked = hatarakeLocked = true ;
        }
        else
        {
            moveLocked = hatarakeLocked = false;
        }

		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        /*
        moveLocked = true;
        hatarakeLocked = true;*/

        foreach (Transform go in transform)
        {
            if (go.name == "Cylinder") actionArea = go;

        }

        tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator;
       
        //navComponent = this.transform.GetComponent <NavMeshAgent>();

	}

    public void addBubble()
    {
        yellingO_Meter = yellingO_Meter + gainByBubble;
        if (yellingO_Meter > maxYellingO_Meter) yellingO_Meter = maxYellingO_Meter;
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && !charge && !moveLocked)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pos.y = transform.position.y;
            pos.y = 2;
            //navComponent.SetDestination (pos);
            //colliders = Physics.OverlapSphere(pos, 1f /* Radius */);
           
            Collider[] colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

            if (!(colliders.Length > 0 && colliders[0].tag == "Employe") && pos != null && pos != transform.position)
            {
                if (tNav.OnGraph(pos, 0))
                {
                    tMemory.SetItem("sabotage", false);
                    tMemory.SetItem("enDeplacement", true);
                    tMemory.SetItem("target", pos);
                }
                else if (colliders.Length > 0 && colliders[0].GetComponent<BreakableFurniture>() != null)
                    {
                        tMemory.SetItem("sabotage", true);
                        tMemory.SetItem("enDeplacement", true);
                        tMemory.SetItem<GameObject>("target", colliders[0].gameObject);
                    }
                
            }
        }
	}

    public IEnumerator Engueulade()
    {

        //print("ENGUEULADE!!!!!!!!!!!!!!");

        actionArea.gameObject.SetActive(true);
        float pos=0;
        while (charge)
        {
            pos = Mathf.Lerp(actionArea.localScale.x, jaugeEngueulageMax * (Mathf.Min(maxLossByScream, yellingO_Meter) / maxLossByScream), vitesseJauge * Time.deltaTime);
            actionArea.localScale = new Vector3(pos, actionArea.localScale.y, pos);

            yield return null;
        }

        yellingO_Meter = (int)(yellingO_Meter - (pos / jaugeEngueulageMax) * maxLossByScream);
        //print("HATARAKE!!!!!!!!!!!!!!!!! ");
        Sign.Create(pos,this.transform.position,SignType.Hatarake);
        if (pos > 7)
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude =  pos;
            GameObject audio =this.transform.Find("hatarake_strong").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (pos > 3)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude = pos;
            GameObject audio = this.transform.Find("hatarake_medium").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (pos > 0)
        {
            GameObject audio = this.transform.Find("hatarake_low").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        actionArea.gameObject.SetActive(false);
        actionArea.localScale = new Vector3(0.8f, actionArea.localScale.y, 0.8f);
        //ResetTimer
       // jaugeEngueulage = 0;
        List<GameObject> employesEngueulable = actionArea.GetComponent<jaugeEngueulage>().getEmployesJauge();
        foreach (GameObject emp in employesEngueulable)
        {
            emp.GetComponent<Employe>().Engueule();
        }
        if (!tutoLock && employesEngueulable.Count > 0)
        {
            tutoLock = true;
            GameManager.instance.nextTutoStep();
        }

        actionArea.GetComponent<jaugeEngueulage>().clearEmployesJauge();

        //actionArea.GetComponent<jaugeEngueulage>().Engueule();
    }


	void OnMouseDown () 
	{
        if (!hatarakeLocked)
        {
            //print("START CHARGE ");
            charge = true;
            tMemory.SetItem("charge", true);
            StartCoroutine(Engueulade());
        }

	}
    void OnPreviewMouseRightButtonDown( )
    {

    }
	void OnMouseUp ()
	{
		charge = false;
		tMemory.SetItem("charge",false);

	}
	
}



