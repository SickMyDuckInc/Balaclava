﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : Panel
{
    //detectamos el joystick
    private FixedJoystick movementJoystick;
    private bool joystickPressedOnce;
    private bool joystickReleased;
    private float joystickSpeed = 60f;

    private bool resolved;

    private float speed = 200.0f;
    private int oldMovement = 0;
    private int movement = 0;
    public float divider;
    int actualNumber = 0;

    public List<AudioClip> badAudios;
    public GameObject door;

    public int[] keyNumber = new int[4];
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        
        index = 0;
        audioS = GetComponent<AudioSource>();

        movementJoystick = GameObject.FindGameObjectWithTag("MovementJoystick").GetComponent<FixedJoystick>();
        joystickPressedOnce = false;
        joystickReleased = true;
        resolved = false;
        this.GetComponent<KeyGenerator>().GenerateKey();
    }

    // Update is called once per frame
    void Update()
    {
        if (resolved)
        {
            return;
        }

        if (panelActive && !SpawnerPlayer.ISDEVICE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                actualNumber = (int)Mathf.Round((transform.localRotation.eulerAngles.z) / (divider));
                if (actualNumber == (int)Mathf.Round(360 / divider))
                {
                    actualNumber = 0;
                }
            }
            if (Input.GetMouseButton(0))
            {
                //transform.eulerAngles = transform.eulerAngles + new Vector3(-Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * speed;
                transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Mouse X")) * Time.deltaTime * speed, Space.Self);
                int actualAux = (int)Mathf.Round((transform.localRotation.eulerAngles.z) / (divider));
                if (actualAux == (int)Mathf.Round(360 / divider))
                {
                    actualAux = 0;
                }
                if (actualNumber != actualAux)
                {
                    if (actualAux != keyNumber[index]) {
                        int index = Random.Range(0, badAudios.Count);
                        audioS.clip = badAudios[index];
                    }
                    else
                    {
                        audioS.clip = goodAudio;
                    }
                    audioS.Play();
                    
                    //print(actualAux);
                    actualNumber = actualAux;
                }
                //Debug.Log("old: " +oldMovement);

                if (-Input.GetAxis("Mouse X") > 0.1)
                {
                    movement = 1;
                    if (oldMovement == 1)
                    {
                        index = 0;
                        oldMovement = 0;
                        Debug.Log("Se resetea");
                    }
                }
                else if (-Input.GetAxis("Mouse X") < -0.1)
                {
                    movement = 2;
                    if (oldMovement == 2)
                    {
                        index = 0;
                        oldMovement = 0;
                        Debug.Log("Se resetea");
                    }
                }
                //Debug.Log("new; " + movement);
            }
            else if (Input.GetMouseButtonUp(0))
            {

                if (movement == oldMovement)
                {
                    Debug.Log("Vas en el mismo sentido, se resetea la cuenta");
                    //index = 0;
                }
                float rotation = transform.localRotation.eulerAngles.z;

                int number = (int)Mathf.Round((rotation) / (divider));

                if (number == (int)Mathf.Round(360 / divider))
                {
                    number = 0;
                }

                if (index < keyNumber.Length)
                {
                    print("number = " + number);
                    if (number == keyNumber[index])
                    {
                        Debug.Log("Número correcto");
                        oldMovement = movement;
                        index++;
                    }
                    else
                    {
                        Debug.Log("Número incorrecto, comienza de nuevo");
                        //index = 0;
                    }
                }

                if (index >= keyNumber.Length)
                {
                    Debug.Log("Has ganado");
                    door.SetActive(false);
                    GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().rewardPlayer();
                    transform.parent.gameObject.tag = "Untagged";
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RotationController>().EnableRotation();
                    resolved = true;
                    GetComponent<MeshRenderer>().enabled = false;
                }
                
            }
        }
        else if(panelActive && SpawnerPlayer.ISDEVICE){
            DeviceLockController();
        }
    }

    private void DeviceLockController()
    {
        if (movementJoystick.pressedJoystick && !joystickPressedOnce)
        {
            Debug.Log("Joystick Pressed Once");
            //joysticks inputs
            joystickPressedOnce = true;
            joystickReleased = false;

            actualNumber = (int)Mathf.Round((transform.localRotation.eulerAngles.z) / (divider));
            if (actualNumber == (int)Mathf.Round(360 / divider))
            {
                actualNumber = 0;
            }
        }
        //if drag the joystick
        if (movementJoystick.pressedJoystick)
        {
            //transform.eulerAngles = transform.eulerAngles + new Vector3(-Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * speed;
            transform.Rotate(new Vector3(0, 0, -movementJoystick.Horizontal) * Time.deltaTime * joystickSpeed, Space.Self);
            int actualAux = (int)Mathf.Round((transform.localRotation.eulerAngles.z) / (divider));
            if (actualAux == (int)Mathf.Round(360 / divider))
            {
                actualAux = 0;
            }
            if (actualNumber != actualAux)
            {
                if (actualAux != keyNumber[index])
                {
                    int index = Random.Range(0, badAudios.Count);
                    audioS.clip = badAudios[index];
                }
                else
                {
                    audioS.clip = goodAudio;
                }
                audioS.Play();

                //print(actualAux);
                actualNumber = actualAux;
            }
            //Debug.Log("old: " +oldMovement);

            if (-movementJoystick.Horizontal > 0.6)
            {
                movement = 1;
                if (oldMovement == 1)
                {
                    index = 0;
                    oldMovement = 0;
                    Debug.Log("Se resetea");
                }
            }
            else if (-movementJoystick.Horizontal < -0.6)
            {
                movement = 2;
                if (oldMovement == 2)
                {
                    index = 0;
                    oldMovement = 0;
                    Debug.Log("Se resetea");
                }
            }
            //Debug.Log("new; " + movement);
        }
        //if release the joystick
        else if (!movementJoystick.pressedJoystick && !joystickReleased)
        {
            Debug.Log("Joystick Released Once");

            //joysticks inputs
            joystickPressedOnce = false;
            joystickReleased = true;

            if (movement == oldMovement)
            {
                Debug.Log("Vas en el mismo sentido, se resetea la cuenta");
                //index = 0;
            }
            float rotation = transform.localRotation.eulerAngles.z;

            int number = (int)Mathf.Round((rotation) / (divider));

            if (number == (int)Mathf.Round(360 / divider))
            {
                number = 0;
            }

            if (index < keyNumber.Length)
            {
                print("number = " + number);
                if (number == keyNumber[index])
                {
                    Debug.Log("Número correcto");
                    oldMovement = movement;
                    index++;
                }
                else
                {
                    Debug.Log("Número incorrecto, comienza de nuevo");
                    //index = 0;
                }
            }

            if (index >= keyNumber.Length)
            {
                Debug.Log("Has ganado");
                door.SetActive(false);
                GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().rewardPlayer();
                transform.parent.gameObject.tag = "Untagged";
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableRotation();
                movementJoystick = null;
                resolved = true;
                GetComponent<MeshRenderer>().enabled = false;
            }

        }
    }
}
