﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float time = 3;

    public GameObject lightA;
    public GameObject lightB;

    public Material lightOn;
    public Material lightOff;

    private bool triggerA;
    private bool triggerB;
    private bool finish = false;

    private bool laserIsActive;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        laserIsActive = true;
        triggerA = false;
        triggerB = false;

        coroutine = WaitAndPrint(time);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            EnableLasers();
            triggerA = RandomBoolean();
            triggerB = RandomBoolean();
            if (triggerA)
            {
                lightA.GetComponent<Renderer>().material = lightOn;
            }
            else
            {
                lightA.GetComponent<Renderer>().material = lightOff;
            }
            if (triggerB)
            {
                lightB.GetComponent<Renderer>().material = lightOn;
            }
            else
            {
                lightB.GetComponent<Renderer>().material = lightOff;
            }
            if(triggerA && triggerB)
            {
                DisableLasers();
            }

        }
    }

    private bool RandomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }

    private void EnableLasers()
    {
        laserIsActive = true;
    }
    
    private void DisableLasers()
    {
        laserIsActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (laserIsActive)
        {
            if (other.gameObject.tag == "Player" && !finish)
            {
                GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().endGame();
                finish = true;
            }
        } 
    }

}
