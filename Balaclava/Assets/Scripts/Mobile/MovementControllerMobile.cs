using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovementControllerMobile : MonoBehaviour {

    public float moveSpeed = 8f;
    [Header("Joystick elements")]
    public GameObject MovementJoystick;
    public Joystick joystick;
    public GameObject RotationJoystick;
    public Joystick rotJoystick;
    public Camera cam;

    private void Start()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            RotationJoystick.SetActive(false);
            MovementJoystick.SetActive(false);
            this.enabled = false;          
        }
        else
        {
            Debug.Log("Entro en Android, IphonePlayer");
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    void Update () 
	{
        Debug.DrawLine(this.transform.position, this.transform.forward * 10);
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        Vector3 rotateVector = (Vector3.up * rotJoystick.Horizontal + Vector3.forward * rotJoystick.Vertical);


        if (moveVector != Vector3.zero)
        {           
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
        if(rotateVector != Vector3.zero)
        {
            float Yaxis = rotateVector.z * moveSpeed/2;
            float Zaxis = rotateVector.y * moveSpeed/2;
            Vector3 rot = new Vector3(transform.rotation.x, transform.rotation.y * Yaxis, transform.rotation.z * Zaxis);

            cam.transform.Rotate(Vector3.up * Yaxis, Space.World);
            cam.transform.Rotate(Vector3.forward * Zaxis, Space.World);
        }
    }
}