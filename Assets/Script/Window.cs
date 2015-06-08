using UnityEngine;
using System.Collections;
//using RAIN.Core;



public class Window : MonoBehaviour
{
    public GameObject bloodStain;
    public AudioSource suicideMale, suicideFemale;

    public ParticleSystem brokenGlass;

    public Animator suicideAnimator;

    //public bool occupe = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playSuicide(bool isMale)
    {
        if (isMale)
        {
            if (!suicideMale.gameObject.activeInHierarchy)
            {
                suicideMale.gameObject.SetActive(true);
            }
            suicideMale.Play();
        }
        else
        {

            if (!suicideFemale.gameObject.activeInHierarchy)
            {
                suicideFemale.gameObject.SetActive(true);
            }
            suicideFemale.Play();
        }
        bloodStain.SetActive(true);
        if (!suicideAnimator.gameObject.activeInHierarchy)
        {
            suicideAnimator.gameObject.SetActive(true);
        }
        //suicideAnimator.Play("idle");
        suicideAnimator.Play("Suicide", -1, 0f);
        suicideAnimator.CrossFade("Suicide", 0);
        brokenGlass.Play();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Employe")
        {

            //	StartCoroutine(other.GetComponentInChildren<Employe>().Repos());	
        }
    }
    void OnTriggerExit(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {



        //AIRig rig = other.GetComponentInChildren<AIRig>();

        //	RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory;//AIRig rig = other.GetComponentInChildren<AIRig>();

        //	RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory;



    }




}

