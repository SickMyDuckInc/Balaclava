using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float speed = 10.0f;
    public GameObject MovementJoystick;
    
    float translation;
    float straffe;


    // Use this for initialization
    void Start () {
        //Prueba para canvas en moviles
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MovementJoystick.SetActive(false);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            MovementJoystick.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        translation = Input.GetAxis("Vertical") * speed;
        straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}
}
