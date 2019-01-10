using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : PlayerController
{
    public float speed = 10.0f;
    
    float translation;
    float straffe;

    private Rigidbody rb;

    private bool enableMovement = true;

    // Use this for initialization
    void Start () {
     
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.DrawLine(this.transform.position, this.transform.forward * 10);

        if (enableMovement)
        {
            translation = Input.GetAxis("Vertical") * speed;
            straffe = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);
            print("aaaa");
            //rb.MovePosition(rb.position + (translation * Vector3.forward));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public override void EnableMovement()
    {
        enableMovement = true;
        rb.isKinematic = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void DisableMovement()
    {
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
