using UnityEngine;
using System.Collections;
//using RAIN.Core;



public class SuicideWindow : MonoBehaviour
{


    public bool occupe = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

