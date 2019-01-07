using System.Collections;
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
    private Material materialA;
    private Material materialB;
    private bool laserIsActive;

    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        laserIsActive = true;
        triggerA = false;
        triggerB = false;

        materialA =  lightA.GetComponent<Renderer>().material;
        materialB = lightB.GetComponent<Renderer>().material;

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
                materialA = lightOn;
            }
            else
            {
                materialA = lightOff;
            }
            if (triggerB)
            {
                materialB = lightOn;
            }
            else
            {
                materialB = lightOff;
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

    private void OnTriggerEnter(Collider other)
    {
        if (laserIsActive)
        {
            if (other.gameObject.tag == "Player")
            {

            }
        } 
    }

}
