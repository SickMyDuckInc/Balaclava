﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Material iluminated;
    Material oldMaterial;
    bool selectable = false;
    bool key = false;
    bool door = false;
    bool keySelected = false;
    public GameObject player;
    public GameObject camera;
    //action button
    public GameObject ActionButton;
    public GameObject HelpText;

    private GameObject selectedObject;
    private GameObject doorObject;


    private bool test = false;

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (camera == null)
            camera = GameObject.FindGameObjectWithTag("MainCamera");

        ActionButton.SetActive(false);
        HelpText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SelectableObject" || other.gameObject.tag == "KeyObject")
        {
            if (other.gameObject.tag == "KeyObject")
            {
                keySelected = true;
            }
            else
            {
                selectable = true;
            }
            // Change the cube color to green.
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            oldMaterial = mesh.material;
            mesh.material = iluminated;
            selectedObject = other.gameObject;
        }
        else if (other.gameObject.tag == "DoorObject")
        {
            door = true;
            doorObject = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SelectableObject")
        {
            // Revert the cube color to white.
            other.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            selectable = false;
        }
        else if(other.gameObject.tag == "DoorObject")
        {
            door = false;
        }
        if (SpawnerPlayer.ISDEVICE)
        {
            ActionButton.SetActive(false);
            HelpText.SetActive(false);
        }
    }

    private void Update()
    {
        if (SpawnerPlayer.ISDEVICE)
        {
            if (selectable)
            {
                if (!ActionButton.activeInHierarchy)
                {
                    ActionButton.SetActive(true);
                }
            }
        }
        else
        {
            //Click izquierdo
            if (Input.GetMouseButton(0))
            {

                //player.transform.position = new Vector3(5.76f, 0.56f, 3f);
                if (selectable)
                {
                    player.GetComponent<MovementController>().DisableMovement();
                    player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
                    player.GetComponent<Rigidbody>().useGravity = false;
                    camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
                    selectedObject.GetComponentInChildren<Panel>().EnablePanel();
                    camera.GetComponent<RotationController>().DisableRotation(selectedObject);
                }
                if (door)
                {
                    doorObject.GetComponentInChildren<DoorController>().CheckDoor(key);
                }
                if (keySelected)
                {
                    key = true;
                    Destroy(selectedObject);
                    keySelected = false;
                }
                if (test)
                {
                    //Debug.Log(player.transform.rotation.ToString());
                }
            }
        }
    }

    public void ButtonPressed()
    {
        player.GetComponent<PlayerController>().DisableMovement();
        player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
        camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
        selectedObject.GetComponentInChildren<Panel>().EnablePanel();
        player.GetComponent<PlayerController>().DisableRotation(selectedObject);

        ActionButton.SetActive(false);
        HelpText.SetActive(true);
    } 

    public void ReturnControl(GameObject selected)
    {
        selectable = false;
        player.GetComponent<MovementController>().EnableMovement();

        selected.GetComponentInChildren<Panel>().DisablePanel();
        player.GetComponent<Rigidbody>().useGravity = true;

        if (SpawnerPlayer.ISDEVICE)
        {
            HelpText.SetActive(false);
        }
    }
}
