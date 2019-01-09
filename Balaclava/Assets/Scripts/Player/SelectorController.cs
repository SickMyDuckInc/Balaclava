﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Material iluminated;
    Material oldMaterial;
    bool selectable = false;
    bool key = true;
    bool door = false;
    bool keySelected = false;
    public GameObject player;
    public GameObject camera;
    private GameObject selectedObject;
    private GameObject doorObject;

    private bool test = false;

    private void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(camera == null)
            camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SelectableObject" || other.gameObject.tag == "KeyObject")
        {
            if(other.gameObject.tag == "KeyObject")
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
        else if(other.gameObject.tag == "DoorObject")
        {
            //MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            //oldMaterial = mesh.material;
            //mesh.material = iluminated;
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
        else if (other.gameObject.tag == "DoorObject")
        {
            //other.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            door = false;
        }
    }

    private void Update()
    {
        //Click izquierdo
        if (Input.GetMouseButton(0) )
        {
            if (selectable)
            {
                //player.transform.position = new Vector3(5.76f, 0.56f, 3f);

                player.GetComponent<MovementController>().DisableMovement();
                player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
                player.GetComponent<Rigidbody>().useGravity = false;
                camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
                selectedObject.GetComponentInChildren<Panel>().EnablePanel();
                camera.GetComponent<RotationController>().DisableRotation(selectedObject);
                //Debug.Log(player.transform.rotation.ToString());
            }
            if (door)
            {
                doorObject.GetComponentInChildren<DoorController>().CheckDoor(key);
            }
            if (keySelected)
            {
                key = true;
                Destroy(selectedObject);
                key = false;
            }
        }
        if (test)
        {
            //Debug.Log(player.transform.rotation.ToString());
        }
    }

    public void ReturnControl(GameObject selected)
    {
        selectable = false;
        player.GetComponent<MovementController>().EnableMovement();

        selected.GetComponentInChildren<Panel>().DisablePanel();
        player.GetComponent<Rigidbody>().useGravity = true;

    }
}
