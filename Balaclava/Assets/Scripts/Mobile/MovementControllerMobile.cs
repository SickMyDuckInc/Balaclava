using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MovementControllerMobile : PlayerController {

    //Movement Values
    [Header("Movement values")]
    public float moveSpeed = 8f;
    [Header("Joystick elements")]
    public GameObject MovementJoystick;
    public Joystick joystick;

    private bool enableMovement = true;

    //Rotation values
    [Header("Rotation values")]
    Vector2 JoystickLook;

    [Header("Joystick elements")]
    public GameObject RotationPanel;
    public FixedTouchField rotPanel;

    public GameObject Hands;
    private GameObject handSelected;

    public GameObject camera;

    private bool enableRotation = true;

    private void Start()
    {
        JoystickLook = new Vector2(camera.transform.eulerAngles.z, camera.transform.eulerAngles.y);
    }

    void FixedUpdate () 
	{
        if (enableMovement)
        {
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                transform.Translate(moveVector * moveSpeed * Time.deltaTime);
            }
        }

        Vector2 md = new Vector2(rotPanel.TouchDist.x * Time.deltaTime * sensitivity, rotPanel.TouchDist.y * Time.deltaTime * sensitivity);

        if (md != Vector2.zero && enableRotation)
        {
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            JoystickLook += smoothV;
            JoystickLook.y = Mathf.Clamp(JoystickLook.y, -90f, 90f);

            this.transform.localRotation = Quaternion.AngleAxis(JoystickLook.x, Vector3.up);
            camera.transform.localRotation = Quaternion.AngleAxis(-JoystickLook.y, Vector3.right);
        }
    }

    public override void DisableMovement()
    {
        base.DisableMovement();
        enableMovement = false;
    }

    public override void EnableMovement()
    {
        base.EnableMovement();
        enableMovement = true;
    }

    public override void EnableRotation()
    {
        enableRotation = true;
        Hands.SetActive(true);
        Hands.GetComponent<SelectorController>().ReturnControl(handSelected);
    }

    public override void DisableRotation(GameObject handSelected)
    {
        enableRotation = false;
        Hands.SetActive(false);
        this.handSelected = handSelected;
    }
}