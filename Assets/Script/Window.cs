﻿using UnityEngine;
using System.Collections;
//using RAIN.Core;



public class Window : MonoBehaviour
{
    public GameObject bloodStain;
    public AudioSource suicideMale, suicideFemale;

    public ParticleSystem brokenGlass;

    public Animator suicideHeadAnimator,suicideTopAnimator,suicideFeetAnimator;
    public SpriteRenderer suicideHeadSpriteRenderer, suicideTopSpriteRenderer, suicideFeetSpriteRenderer;
    public BreakableFurnitureee breakableFurniture;
    //public bool occupe = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playSuicide(bool isMale,Color hairColor,Color topColor)
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
        if (!suicideHeadAnimator.gameObject.activeInHierarchy)
        {
            suicideHeadAnimator.gameObject.SetActive(true);
            suicideTopAnimator.gameObject.SetActive(true);
            suicideFeetAnimator.gameObject.SetActive(true);
        }

        suicideHeadSpriteRenderer.color = hairColor;
        suicideTopSpriteRenderer.color = topColor;
        //suicideAnimator.Play("idle");
        suicideHeadAnimator.Play("suicide_head_anim", -1, 0f);
        suicideHeadAnimator.CrossFade("suicide_head_anim", 0);
        suicideTopAnimator.Play("suicide_top_anim", -1, 0f);
        suicideTopAnimator.CrossFade("suicide_top_anim", 0);
        suicideFeetAnimator.Play("suicide_feet_anim", -1, 0f);
        suicideFeetAnimator.CrossFade("suicide_feet_anim", 0);
        brokenGlass.Play();
        breakableFurniture.Break();
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

