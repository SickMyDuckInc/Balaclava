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
}
