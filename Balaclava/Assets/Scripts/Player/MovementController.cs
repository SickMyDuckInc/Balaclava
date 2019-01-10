using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : PlayerController
{
    public float speed = 10.0f;
    
    float translation;
    float straffe;

    private Rigidbody rb;

    AudioSource audioS;

    private bool enableMovement = true;

    // Use this for initialization
    void Start () {
     
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.DrawLine(this.transform.position, this.transform.forward * 10);

        if (enableMovement)
        {
            translation = Input.GetAxis("Vertical") * speed;
            straffe = Input.GetAxis("Horizontal") * speed;

            if(translation != 0)
            {
                if (!audioS.isPlaying)
                {
                    audioS.Play();
                }
            }
            else
            {
                audioS.Pause();
            }

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;


            transform.Translate(straffe, 0, translation);
            //rb.MovePosition(rb.position + (translation * Vector3.forward));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public override void EnableMovement()
    {
        base.EnableMovement();
        enableMovement = true;
        rb.isKinematic = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void DisableMovement()
    {
        base.DisableMovement();
        enableMovement = false;
        rb.isKinematic = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void EnableRotation()
    {
        //throw new System.NotImplementedException();
    }

    public override void DisableRotation(GameObject handSelected)
    {
        //throw new System.NotImplementedException();
    }
}
