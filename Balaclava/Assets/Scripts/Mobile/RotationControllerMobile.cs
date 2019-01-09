using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControllerMobile : PlayerController
{
    Vector2 JoystickLook;    

    [Header("Joystick elements")]
    public GameObject RotationJoystick;
    public Joystick rotJoystick;

    GameObject character;
    public GameObject Hands;
    private GameObject handSelected;

    private bool enableRotation = true;

    // Start is called before the first frame update
    void Start()
    {        
        character = this.transform.parent.gameObject;
    }

    private void FixedUpdate()
    {
        Vector2 md = new Vector2(rotJoystick.Horizontal * Time.deltaTime * sensitivity, rotJoystick.Vertical * Time.deltaTime * sensitivity);

        if (md != Vector2.zero && enableRotation)
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

    public override void EnableMovement()
    {
        //throw new System.NotImplementedException();
    }

    public override void DisableMovement()
    {
        //throw new System.NotImplementedException();
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
