using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MovementControllerMobile : PlayerController {

    public static string DEFAULT_OPERATING_SYSTEM = "windows";

    public float moveSpeed = 8f;
    [Header("Joystick elements")]
    public GameObject MovementJoystick;
    public Joystick joystick;

    private bool enableMovement = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update () 
	{
        if (enableMovement)
        {
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                transform.Translate(moveVector * moveSpeed * Time.deltaTime);
            }
        }
        
    }

    public override void DisableMovement()
    {
        enableMovement = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void DisableRotation(GameObject handSelected)
    {
        //throw new System.NotImplementedException();
    }

    public override void EnableMovement()
    {
        enableMovement = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void EnableRotation()
    {
        //throw new System.NotImplementedException();
    }
}