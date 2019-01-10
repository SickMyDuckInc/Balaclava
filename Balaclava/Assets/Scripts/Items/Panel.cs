using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool panelActive = false;


    public AudioClip standardAudio;
    public AudioClip goodAudio;
    public AudioClip badAudio;

    protected AudioSource audioS;

    public void EnablePanel()
    {
        panelActive = true;
    }

    public void DisablePanel()
    {
        panelActive = false;
    }

}
