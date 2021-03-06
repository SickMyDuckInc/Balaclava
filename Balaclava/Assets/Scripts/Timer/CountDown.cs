﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    //Singleton
    public static CountDown cd = null;

    public TextMeshProUGUI timerText;
    [Header("Time in minutes")]
    public float gameTimer = 10f;

    public Canvas canvas;
    [HideInInspector]
    public bool callPolice;
    private bool gameOver;

    AudioSource audioS;

    string t = "Timer: ";
    string s = ":";

    // Start is called before the first frame update
    void Start()
    {
        cd = this;
        audioS = GetComponent<AudioSource>();

        gameTimer *= 60;
        callPolice = false;
        gameOver = false;
        StartCoroutine(RefreshTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            gameTimer -= Time.deltaTime;
        }
        else
        {
            GameOver();
        }
    }

    private IEnumerator RefreshTimer()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1f);
            //update minutes
            int minutes = (int)(gameTimer / 60) % 60;
            string min;
            if(minutes < 10 && minutes >= 0)
            {
                min = "0" + minutes;
            }
            else
            {
                min = ""+minutes;
            }
            //update seconds
            int seconds = (int)(gameTimer % 60);
            string sec;
            if (seconds < 10 && seconds >= 0)
            {
                sec = "0" + seconds;
            }
            else
            {
                sec = "" + seconds;
            }

            if (minutes <= 0 && seconds < 0)
            {
                Debug.Log("TIME FINISHED");
                //string format          
                string timerString = string.Format("{0}{1}{2}{3}", t, 00, s, 00);
                gameOver = true;
            }
            else
            {
                //string format          
                string timerString = string.Format("{0}{1}{2}{3}", t, min, s, sec);
                //write text
                timerText.text = timerString;
                //Debug.Log("Time : " + minutes + ": " + seconds);
            }
   
        }       
    }

    /*private void LateUpdate()
    {
        canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
    }*/

    public void ActivatePoliceTime()
    {
        if (!callPolice)
        {
            int minutes = (int)(gameTimer / 60) % 60;
            int seconds = (int)(gameTimer % 60);
            Debug.Log("Minutes and seconds = " + minutes + " , " + seconds);
            if (minutes * 60 + seconds > 45f)
            {
                gameTimer = 45f;
            }
            Debug.Log("Llamamos a la policia");
            audioS.Play();
            callPolice = true;
        }        
    }

    private void GameOver()
    {
        //Debug.Log("JUEGO TERMINADO");
        GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().endGame();
    }
}
