using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;

public class MovementControllerMobile : MonoBehaviour {

    public static string DEFAULT_OPERATING_SYSTEM = "windows";

    public float moveSpeed = 8f;
    [Header("Joystick elements")]
    public GameObject MovementJoystick;
    public Joystick joystick;

    public TextMeshProUGUI debug;

    private void Start()
    {
        debug.text = "SystemInfo: " + SystemInfo.operatingSystem;
        string operatingSystem = SystemInfo.operatingSystem;

        if (operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM))
        {
            MovementJoystick.SetActive(false);
            this.enabled = false;
        }
        else{
            Debug.Log("Entro en Android, IphonePlayer");
        }

    }

    void Update () 
	{
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {           
            transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        }
    }
}