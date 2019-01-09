using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : PlayerController {
    Vector2 mouseLook;

    private bool enableRotation = true;

    GameObject character;
    public GameObject Hands;
    private GameObject handSelected;

    // Use this for initialization
    void Start () {
       
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

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y + transform.localRotation.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
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
