using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControllerMobile : MonoBehaviour
{
    public static string DEFAULT_OPERATING_SYSTEM = "windows";

    Vector2 JoystickLook;    

    [Header("Joystick elements")]
    public GameObject RotationJoystick;
    public Joystick rotJoystick;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        string operatingSystem = SystemInfo.operatingSystem;

        if (operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM))
        {
            RotationJoystick.SetActive(false);
            this.enabled = false;
        }
        else
        {
            Debug.Log("Entro en Android, IphonePlayer");
            character = this.transform.parent.gameObject;
        }

        character = this.transform.parent.gameObject;
    }

    private void FixedUpdate()
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
}
