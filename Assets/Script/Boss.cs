using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Minds;
using RAIN.Serialization;
using RAIN.Motion;

public class Boss : MonoBehaviour {

	//Vector2 position; //peut utiliser son transform
	float vitesseDep;

	//GameObject[] boxies;

	//float jaugeEngueulage; //se remplit quand on appuie sur le boss.

  public  float jaugeEngueulageMax = 15.0f;

    public float vitesseJauge = 2.0f;


	bool charge = false;
    Vector3 pos;

	Transform actionArea;
	private RAIN.Memory.BasicMemory tMemory;


	void Start () {
		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;

		actionArea = transform.FindChild("engueuladeJauge");

	}
	

	void Update () {

		if(Input.GetMouseButton(0))
		{


            Collider[] colliders;

          if (!charge) 
            {

                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.y = 2;

                colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

                if (pos != null && pos != transform.position && (colliders == null || (colliders != null && (colliders.Length == 0 || colliders[0].tag == "Nav"))))
                {
                    tMemory.SetItem("enDeplacement", true);
                    tMemory.SetItem("target", pos);
                }
            }
			

		}
        
        if (pos.x == transform.position.x && pos.z == transform.position.z)
           tMemory.SetItem("enDeplacement", false);

	}

    public IEnumerator Engueulade()
    {

        print("ENGUEULADE!!!!!!!!!!!!!!");

        actionArea.gameObject.SetActive(true);
        float pos=0;
        while (charge)
        {
            pos = Mathf.Lerp(actionArea.localScale.x, jaugeEngueulageMax, vitesseJauge * Time.deltaTime);
            actionArea.localScale = new Vector3(pos, actionArea.localScale.y, pos);

            yield return null;
        }
        print("HATARAKE!!!!!!!!!!!!!!!!! ");
        Sign.Create(pos,this.transform.position,SignType.Hatarake);
        if (pos > 10)
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude =  pos;
            GameObject audio = this.transform.Find("hatarake_strong").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (pos > 5)
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

        foreach (GameObject emp in actionArea.GetComponent<jaugeEngueulage>().getEmployesJauge())
        {
            emp.GetComponent<Employe>().Engueule();
        }
        actionArea.GetComponent<jaugeEngueulage>().clearEmployesJauge();

        //actionArea.GetComponent<jaugeEngueulage>().Engueule();
    }


	void OnMouseDown () 
	{
		print ("START CHARGE ");
		charge = true;
		tMemory.SetItem("charge",true);
        StartCoroutine(Engueulade());	

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



