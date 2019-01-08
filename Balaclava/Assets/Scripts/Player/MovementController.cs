using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float speed = 10.0f;
    
    float translation;
    float straffe;

    private bool enableMovement = true;

    // Use this for initialization
    void Start () {

        //Prueba para canvas en moviles
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            Debug.Log("Entro en windowsPlayer");
        }
        else
        {
            Debug.Log("Entro en Android, IphonePlayer");
            this.enabled = false;
        }
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {      
        if (enableMovement)
        {
            translation = Input.GetAxis("Vertical") * speed;
            straffe = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);
           

            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void EnableMovement()
    {
        enableMovement = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableMovement()
    {
        enableMovement = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
