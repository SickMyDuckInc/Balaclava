using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool openDoor = false;
    float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
