﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation;

public class Decrasseur : MonoBehaviour {
    public List<GameObject> emptyChill;
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
        employeProfile = GameObject.Find("EmployeeProfile");

        data.InitializeEmployee();

        this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = data.hairColor;
        this.transform.GetChild(1).GetComponent<SpriteRenderer>().color = data.topColor;

	}
	
	// Update is called once per frame
	void Update () {



	}


    // mets les repères pour l'employé
    public void SetEmployeeLocations()
    {
       
            emptyChill = new List<GameObject>();
            Repos[] chills = floor.GetComponentsInChildren<Repos>();
            foreach (Repos chi in chills)
            {
                if (chi.transform.parent.GetComponentInChildren<BreakableFurniture>() != null)
                emptyChill.Add(chi.gameObject);

            }

            
        
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