using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDestroy : MonoBehaviour
{
    AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        audioS.Play();
    }

    private void OnDisable()
    {
        audioS.Play();
    }
}
