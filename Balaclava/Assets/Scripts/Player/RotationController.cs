using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 JoystickLook;

    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    private bool enableRotation = true;
    private bool isDevice;

    GameObject character;
    public GameObject Hands;

    [Header("Joystick elements")]
    public GameObject RotationJoystick;
    public FixedJoystick rotJoystick;

    // Use this for initialization
    void Start () {
        //Prueba para canvas en moviles
        string operatingSystem = SystemInfo.operatingSystem;

        if (operatingSystem.ToLower().Contains(MovementController.DEFAULT_OPERATING_SYSTEM))
        {
            Debug.Log("Entro en windowsPlayer");
            isDevice = false;
            RotationJoystick.SetActive(false);
            rotJoystick.enabled = false;
        }
        else 
        {
            isDevice = true;
            this.enabled = false;
            Debug.Log("Entro en Android, IphonePlayer");
        }

        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if (enableRotation && !isDevice)
        {
            PCrotation();
        }
        else if (enableRotation && isDevice)
        {
            DeviceRotation();
        }
    }

    private void PCrotation()
    {
        //not isDevice variables
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }

    private void DeviceRotation()
    {
        Vector2 md = new Vector2(rotJoystick.Horizontal * Time.deltaTime * sensitivity, rotJoystick.Vertical * Time.deltaTime * sensitivity);

        if (md != Vector2.zero)
        {
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            JoystickLook += smoothV;
            JoystickLook.y = Mathf.Clamp(JoystickLook.y, -90f, 90f);

            character.transform.localRotation = Quaternion.AngleAxis(JoystickLook.x, Vector3.up);
            transform.localRotation = Quaternion.AngleAxis(-JoystickLook.y, Vector3.right);
        }
    }

    public void DisableRotation()
    {
        enableRotation = false;
        Hands.SetActive(false);
    }

    public void EnableRotation()
    {
        enableRotation = true;
        Hands.SetActive(true);
    }
}
