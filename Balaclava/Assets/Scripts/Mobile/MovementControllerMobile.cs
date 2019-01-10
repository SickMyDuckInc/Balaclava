using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class MovementControllerMobile : PlayerController {

    //Movement Values
    [Header("Movement values")]
    public float moveSpeed = 8f;
    [Header("Joystick elements")]
    public Joystick joystick;

    private bool enableMovement = true;

    //Rotation values
    [Header("Rotation values")]
    Vector2 JoystickLook;

    [Header("Joystick elements")]
    public FixedTouchField rotPanel;

    public GameObject Hands;
    private GameObject handSelected;

    public GameObject camera;

    private AudioSource audioS;

    private bool enableRotation = true;

    //VARIABLES DE PRUEBA
    private float pitch;
    private float yaw;

    private void Start()
    {
        JoystickLook = new Vector2(transform.localRotation.y,camera.transform.localRotation.x);
        audioS = GetComponent<AudioSource>();

        pitch = camera.transform.eulerAngles.x;
        yaw = this.transform.eulerAngles.y;
    }

    void LateUpdate () 
	{
        if (enableMovement)
        {
            Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

            if (moveVector != Vector3.zero)
            {
                transform.Translate(moveVector * moveSpeed * Time.deltaTime);
                if (!audioS.isPlaying)
                {
                    audioS.Play();
                }
            }
            else
            {
                audioS.Pause();
            }
        }

        Vector2 md = new Vector2(rotPanel.TouchDist.x * Time.deltaTime * sensitivity, rotPanel.TouchDist.y * Time.deltaTime * sensitivity);

        if (md != Vector2.zero && enableRotation)
        {
            pitch -= md.y;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            yaw += md.x;

            //Debug.Log("Pitch: " + pitch);
            //Debug.Log("Yaw: " + yaw);

            camera.transform.localRotation = Quaternion.AngleAxis(pitch, Vector3.right);
            this.transform.localRotation = Quaternion.AngleAxis(yaw, Vector3.up);            
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