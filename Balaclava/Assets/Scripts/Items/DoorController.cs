using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool openDoor = false;
    float counter = 0;
    AudioSource audioS;
    public AudioClip goodClip;
    public AudioClip badClip;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(openDoor && counter < 90)
        {
            transform.Rotate(Vector3.up * -Time.deltaTime*50);
            counter += Time.deltaTime*50;
        }
        
    }

    public void CheckDoor(bool key)
    {
        if (key)
        {
            openDoor = true;
            if (!audioS.isPlaying)
            {
                audioS.volume = 1;
                audioS.clip = goodClip;
                audioS.Play();
            }
        }
        else
        {
            if (!audioS.isPlaying)
            {
                audioS.volume = 0.4f;
                audioS.clip = badClip;
                audioS.Play();
            }
        }
    }
}
