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

	//Vector2 position; //peut utiliser son transform
	float vitesseDep;
    public bool moveLocked ;
    public bool hatarakeLocked ;
    public int yellingO_Meter = 0;

    public int maxYellingO_Meter = 8;
    public int gainByBubble = 1;
    public int maxLossByScream = 8;


    public float jaugeEngueulageMin = 4.0f; //se remplit quand on appuie sur le boss.
    public float jaugeEngueulageMax = 15.0f; //se remplit quand on appuie sur le boss.
    public float tempsRemplissageJauge = 1.0f; 

	//GameObject[] boxies;

    public AudioSource[] bossMovingSounds;
    public AudioSource[] voicelessBossSounds;
    public AudioSource[] bubbleSound;


	//float timer = 0;

	public bool charge = false;
    Vector3 pos;

	Transform actionArea;
	public RAIN.Memory.BasicMemory tMemory;
    private RAIN.Navigation.BasicNavigator tNav;

	// Use this for initialization
	void Start () {
        if (GameManager.instance.tutoIsOn) { 
            moveLocked = hatarakeLocked = true;
        }
        else { }

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

    public void playRandomBossSound(){
        bool alreadyPlaying=false;
        for (int i = 0; i < bossMovingSounds.Length; i++)
            if (bossMovingSounds[i].isPlaying) {
                alreadyPlaying = true; 
            };

        if (!alreadyPlaying)
        {
            int rdmIndex = Random.Range(0, bossMovingSounds.Length);
            bossMovingSounds[rdmIndex].Play();
        }
    }

    public void playRandomVoicelessBossSound()
    {
        bool alreadyPlaying = false;
        for (int i = 0; i < voicelessBossSounds.Length; i++)
            if (voicelessBossSounds[i].isPlaying) {
                alreadyPlaying = true; 
            };

        if (!alreadyPlaying)
        {
            int rdmIndex = Random.Range(0, voicelessBossSounds.Length);
            voicelessBossSounds[rdmIndex].Play();
        }
    }

    public void addBubble()
    {
        for (int i = 0; i < bubbleSound.Length;i++ )
            if (!bubbleSound[i].isPlaying)
            {
                bubbleSound[i].Play();
                break;
            }
        yellingO_Meter = yellingO_Meter + gainByBubble;
        if (yellingO_Meter > maxYellingO_Meter) yellingO_Meter = maxYellingO_Meter;
    }
    
    public bool moveSoundLock=false;

	// Update is called once per frame
	void Update () {
       
        if (Input.GetMouseButtonUp(0) && !charge && !moveLocked)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pos.y = transform.position.y;
            pos.y = 2;
            //navComponent.SetDestination (pos);
            //colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

            //print("un deux un deux");

            Collider[] colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

            if (!(colliders.Length > 0 && colliders[0].tag == "Employe") && pos != null && pos != transform.position)
            {
                if (tNav.OnGraph(pos, 0))
                {
                    tMemory.SetItem("sabotage", false);
                    tMemory.SetItem("enDeplacement", true);
                    tMemory.SetItem("target", pos);                    
                }
               
            }
        }

        if (tMemory.GetItem<bool>("enDeplacement") && !moveSoundLock)
        {
            playRandomBossSound();
        }
        else if (!tMemory.GetItem<bool>("enDeplacement") && moveSoundLock)
        {
            moveSoundLock = false;
        }

	}

    public IEnumerator Engueulade()
    {
        float currentYellingOMeter = yellingO_Meter;
        float top = Mathf.Min(maxLossByScream, yellingO_Meter);

        actionArea.gameObject.SetActive(true);
        float pos=0;
        float time = 0;
        while (charge)
        {
            time = time + Time.deltaTime;

            pos = Mathf.Lerp(jaugeEngueulageMin, jaugeEngueulageMin + jaugeEngueulageMax * (top / maxLossByScream), time  / (tempsRemplissageJauge* (top / maxLossByScream))    );

            actionArea.localScale = new Vector3(pos, actionArea.localScale.y, pos);

            yield return null;
        }
        int yellingO_OnEight = (int)(((pos - jaugeEngueulageMin) / jaugeEngueulageMax) * 8);
        yellingO_Meter = yellingO_Meter - yellingO_OnEight*maxYellingO_Meter/8;

        Sign.Create(pos, this.transform.position, SignType.Hatarake);
        print("pos : "+pos+" ,yellingO_OnEight : " + yellingO_OnEight);
        /*
		if(yellingO_Meter == 8){
        	yellingO_Meter = 0;
        	Sign.Create(pos,this.transform.position,SignType.Hatarake);
		}*/

        if (yellingO_OnEight > 6)
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude =  pos;
            GameObject audio =this.transform.Find("hatarake_strong").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (yellingO_OnEight > 1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude = pos;
            GameObject audio = this.transform.Find("hatarake_medium").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (yellingO_OnEight > 0)
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
            if (emp.GetComponent<Decrasseur>() != null)
                emp.GetComponent<Decrasseur>().Engueule();
            else
            emp.GetComponent<Employe>().Engueule();
        }
        if (GameManager.instance.tutoIsOn && GameManager.instance.goingToHatarakeSlacker)
        {
            if (employesEngueulable.Count >= 1)
            {
                GameManager.instance.TutoEmployeeHataraked();
            }
        }

        actionArea.GetComponent<jaugeEngueulage>().clearEmployesJauge();

        //actionArea.GetComponent<jaugeEngueulage>().Engueule();
    }


	void OnMouseDown () 
	{
        if (!hatarakeLocked)
        {
            if (yellingO_Meter == 0)
            {
                playRandomVoicelessBossSound();
            }
            else
            {
                //print("START CHARGE ");
                charge = true;
                tMemory.SetItem("charge", true);
                StartCoroutine(Engueulade());

            }
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



