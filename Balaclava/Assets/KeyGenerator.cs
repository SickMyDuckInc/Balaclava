using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateKey()
    {
        if (this.name == "SafeLockerMove56")
        {
            GenerateKey(50);
        }
        else if(this.name == "SafeLockerMove01")
        {
            GenerateKey(10);
        }
        else if(this.name == "panel56")
        {
            GeneratekeyLights();
        }

    }

    private void GenerateKey(int number)
    {
        int[] totalKey = new int[4];
        int count = 0;
        while (count < 4)
        {
            int key = Random.Range(0, number);
            if(count == 0)
            {
                totalKey[count] = key;
                count++;
            }
            else
            {
                if(totalKey[count-1] != key)
                {
                    totalKey[count] = key;
                    count++;
                }
            }
        }

        GetComponent<LockController>().keyNumber = totalKey;
    }

    private void GeneratekeyLights()
    {
        int key = Random.Range(0, 10);
        LightController light = GetComponent<LightController>();
        light.key[0] = key;
        for(int i = 1; i<4; i++)
        {
            int rand = Random.Range(0, 10);
            light.key[i] = rand;
            key = key * 10 + rand; 
        }
        GetComponent<PanelController>().key = key;
        //GetComponent<LightController>().key[0] 
    }
}
