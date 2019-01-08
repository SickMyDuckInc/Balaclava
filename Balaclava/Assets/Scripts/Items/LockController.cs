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

    public int[] keyNumber;
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
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * speed);
            Debug.Log(Input.GetAxis("Mouse X"));
            select = false;
            if(- Input.GetAxis("Mouse X") > 0)
            {
                movement = 1;
            }
            else if(-Input.GetAxis("Mouse X") < 0)
            {
                movement = 2;
            }
        }
        else
        {
            select = true;

            if(movement == oldMovement)
            {
                index = 0;
            }
            float rotation = transform.rotation.eulerAngles.x;

            int number = (int)Mathf.Round((rotation) / (3.6f));

            if (number == 100)
            {
                number = 0;
            }

            if (index < keyNumber.Length && number == keyNumber[index])
            {
                Debug.Log("Número correcto");
                index++;
            }
            else
            {
                Debug.Log("Número incorrecto, comienza de nuevo");
                index = 0;
            }
            oldMovement = movement;

        }
        
        
    }
}
