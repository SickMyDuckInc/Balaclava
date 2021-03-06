﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : Panel
{
    public int key;
    private int numberIntroduce;
    private int count;
    private int tries;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<KeyGenerator>().GenerateKey();
        count = 0;
        numberIntroduce = 0;
        tries = 0;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(int button)
    {
        if (panelActive)
        {
            count++;
            numberIntroduce *= 10;
            numberIntroduce += button;
            audioS.clip = standardAudio;
            audioS.Play();

            if (count == 4)
            {
                if (numberIntroduce == key)
                {
                    audioS.clip = goodAudio;
                    audioS.Play();
                    Debug.Log("Exito");
                    door.SetActive(false);
                    foreach (Renderer r in GetComponentsInChildren<Renderer>())
                        r.enabled = false;

                    GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().rewardPlayer();

                    //Abrir puerta o lo que sea
                    transform.parent.gameObject.tag = "Untagged";
                    
                    //Si no es un móvil
                    if (!SpawnerPlayer.ISDEVICE)
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerController>().EnableRotation();
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableRotation();
                    }
                }
                else
                {
                    audioS.clip = badAudio;
                    audioS.Play();
                    tries++;
                    if (tries == 3)
                    {
                        Debug.Log("has perdido");
                        GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().endGame();
                    }
                    else
                    {
                        count = 0;
                        numberIntroduce = 0;
                        Debug.Log("Error, intentalo de nuevo");
                    }
                }
            }
        }
        
    }
}
