using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlayer : MonoBehaviour
{
    protected string DEFAULT_OPERATING_SYSTEM_ANDROID = "android";
    protected string DEFAULT_OPERATING_SYSTEM_APPLE = "ios";

    public GameObject WindowsPlayerPrefab;
    public GameObject MobilePlayerPrefab;

    public GameObject MovementJoystick;
    public GameObject RotationJoystick;

    // Start is called before the first frame update
    void Start()
    {
        string operatingSystem = SystemInfo.operatingSystem;

        if (operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM_ANDROID) || operatingSystem.ToLower().Contains(DEFAULT_OPERATING_SYSTEM_APPLE))
        {
            Debug.Log("SpawnPlayer movil en móvil");
            MobilePlayerPrefab.SetActive(true);
            Destroy(WindowsPlayerPrefab);
        }
        else
        {
            Debug.Log("SpawnPlayer Windows");
            MovementJoystick.SetActive(false);
            RotationJoystick.SetActive(false);
            WindowsPlayerPrefab.SetActive(true);
            Destroy(MobilePlayerPrefab);
        }
    }

}
