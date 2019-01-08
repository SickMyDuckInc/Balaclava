using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    private float speed = 100.0f;
    private bool select = false;
    private int oldMovement = 0;
    private int movement = 0;
    private int numberSelected;
    public float divider;

    public int[] keyNumber = new int[4];
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        numberSelected = 0;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //transform.eulerAngles = transform.eulerAngles + new Vector3(-Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Mouse X")) * Time.deltaTime * speed,Space.Self);
            //Debug.Log(transform.eulerAngles);
            select = false;
            if(- Input.GetAxis("Mouse X") > 0.1)
            {
                movement = 1;
                if(oldMovement == 1)
                {
                    index = 0;
                    oldMovement = 0;
                    Debug.Log("Se resetea");
                }
            }
            else if(-Input.GetAxis("Mouse X") < -0.1)
            {
                movement = 2;
                if(oldMovement == 2)
                {
                    index = 0;
                    oldMovement = 0;
                    Debug.Log("Se resetea");
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            select = true;

            if(movement == oldMovement)
            {
                Debug.Log("Vas en el mismo sentido, se resetea la cuenta");
                index = 0;
            }
            float rotation = transform.rotation.eulerAngles.z;
            
            int number = (int)Mathf.Round((rotation) / (divider));

            if (number == (int)Mathf.Round(360/ divider))
            {
                number = 0;
            }

            if (index < keyNumber.Length)
            {
                if(number == keyNumber[index])
                {
                    Debug.Log("Número correcto");
                    index++;
                }
                else
                {
                    Debug.Log("Número incorrecto, comienza de nuevo");
                    index = 0;
                }                
            }
            
            if(index > keyNumber.Length)
            {
                Debug.Log("Has ganado");
            }
            oldMovement = movement;

        }
        
        
    }
}
