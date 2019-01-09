using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static string DEFAULT_OPERATING_SYSTEM = "windows";

    public float speed = 10.0f;
    
    float translation;
    float straffe;

    private bool enableMovement = true;

    // Use this for initialization
    void Start () {

        string operatingSystem = SystemInfo.operatingSystem;

        //Prueba para canvas en moviles
        if (operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM))
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
        Debug.DrawLine(this.transform.position, this.transform.forward * 10);

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
