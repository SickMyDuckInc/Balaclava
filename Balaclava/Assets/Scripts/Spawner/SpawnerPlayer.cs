using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{
    public static bool ISDEVICE;

    protected string DEFAULT_OPERATING_SYSTEM_ANDROID = "android";
    protected string DEFAULT_OPERATING_SYSTEM_APPLE = "ios";

    public GameObject WindowsPlayerPrefab;
    public GameObject MobilePlayerPrefab;

    public GameObject MovementJoystick;
    public GameObject RotationJoystick;

    public GameObject ActionButton;
    public GameObject HelpText;

    // Start is called before the first frame update
    void Start()
    {
        string operatingSystem = SystemInfo.operatingSystem;

        if (operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM_ANDROID) || operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM_APPLE))
        {
            Debug.Log("SpawnPlayer en móvil");
            MobilePlayerPrefab.SetActive(true);
            Destroy(WindowsPlayerPrefab);
            ISDEVICE = true;
        }
        else
        {
            Debug.Log("SpawnPlayer Windows");
            MovementJoystick.SetActive(false);
            RotationJoystick.SetActive(false);
            WindowsPlayerPrefab.SetActive(true);
            Destroy(MobilePlayerPrefab);
            ISDEVICE = false;
        }

        //Hide always at start
        ActionButton.SetActive(false);
        HelpText.SetActive(false);

        /*MobilePlayerPrefab.SetActive(true);
        Destroy(WindowsPlayerPrefab);
        ISDEVICE = true;*/
    }

}
