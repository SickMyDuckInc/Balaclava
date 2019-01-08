using UnityEngine;

public class Player3DExample : MonoBehaviour {

    public float moveSpeed = 8f;
    public Joystick joystick;
    public Joystick joystick2;

	void Update () 
	{
        Debug.DrawLine(this.transform.position, this.transform.forward * 10);
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);
        Vector3 rotateVector = (Vector3.up * joystick2.Horizontal + Vector3.forward * joystick2.Vertical);
        //Debug.Log("MoveVector = " + moveVector);
        //Debug.Log("RotateVector = " + rotateVector);



        if (moveVector != Vector3.zero)
        {           
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
        if(rotateVector != Vector3.zero)
        {
            float Yaxis = rotateVector.z * moveSpeed/2;
            float Zaxis = rotateVector.y * moveSpeed/2;
            Vector3 rot = new Vector3(transform.rotation.x, transform.rotation.y * Yaxis, transform.rotation.z * Zaxis);

            Debug.Log("Antiguo transform.rotation = " + transform.rotation.eulerAngles);
            Debug.Log("Nuevo transform.rotation = " + transform.rotation.eulerAngles);
            transform.Rotate(Vector3.up * Yaxis, Space.World);
            transform.Rotate(Vector3.forward * Zaxis, Space.World);

            //transform.rotation = Quaternion.LookRotation(rot);
        }
    }
}