using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    private bool enableRotation = true;

    GameObject character;
    public GameObject Hands;

	// Use this for initialization
	void Start () {
        //Prueba para canvas en moviles
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            Debug.Log("Entro en windowsPlayer");
        }
        else 
        {
            this.enabled = false;
            Debug.Log("Entro en Android, IphonePlayer");
        }

        character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        if (enableRotation)
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
