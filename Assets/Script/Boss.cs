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

  public  float jaugeEngueulageMax = 15.0f; //se remplit quand on appuie sur le boss.

    public float vitesseJauge = 2.0f; //se remplit quand on appuie sur le boss.

	
	//float timer = 0;

	bool charge = false;
    Vector3 pos;

	Transform actionArea;
	private RAIN.Memory.BasicMemory tMemory;

	// Use this for initialization
	void Start () {
		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;

		actionArea = transform.GetChild (0);        
	
        //navComponent = this.transform.GetComponent <NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0))
		{
			//Vector3 pos;

            Collider[] colliders;

          if (!charge) 
            {

                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //pos.y = transform.position.y;
                pos.y = 2;
                //navComponent.SetDestination (pos);

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
        Sign.Create(pos,this.transform.position,SignType.Death);
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



