﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation;

public class Decrasseur : MonoBehaviour {
    public static List<GameObject> emptyRepair;
    public GameObject floor;

    public RAIN.Memory.BasicMemory tMemory;
    private RAIN.Navigation.BasicNavigator tNav;
    public EmployeeData data;
    GameObject employeProfile;

    void Awake()
    {
        AIRig aiRig = GetComponentInChildren<AIRig>();
        tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}


    // mets les repères pour l'employé
    public void SetEmployeeLocations()
    {
        //if (emptyRepair == null)
       // {
            emptyRepair = new List<GameObject>();
            InteractWithEmployee[] chills = floor.GetComponentsInChildren<InteractWithEmployee>();

         bool alreadyInList = false;
            foreach (InteractWithEmployee chi in chills)
            {
               alreadyInList = false;
                foreach (GameObject available in emptyRepair)
                {
                    if (available.transform.parent == chi.transform.parent)
                        alreadyInList = true;
                }

                if (!alreadyInList)
                {
                    if (chi.transform.parent.Find("breakPos") != null)
                        emptyRepair.Add(chi.transform.parent.Find("breakPos").gameObject);
                    else
                        if (chi.transform.parent.GetComponentInChildren<BreakableFurniture>() != null)
                            emptyRepair.Add(chi.gameObject);
                }
            }
       // }
    }


    public void Engueule()
    {
        //Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.

        //if (data.fatigue < data.fatigueMAX) {
        //suicidaire = true;	
        tMemory.SetItem<bool>("hatarake", true);
        //tMemory.SetItem<bool>("auTravail", true);
        //   tMemory.SetItem<bool>("wander", false);
       // this.setActiveSound(true, false, false, false, false, false, false, false, false);

        //}	
    }



}
