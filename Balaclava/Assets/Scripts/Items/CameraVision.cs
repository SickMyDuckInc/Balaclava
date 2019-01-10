using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    Vector3 playerDirection;

    private bool isPlayerVisible;

    RaycastHit hit;
    //layer mask
    int layerMask;
    //max distance of RayCast
    float maxDistance;

    public GameObject camera;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Water");
        layerMask = ~layerMask;
        maxDistance = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerDirection = other.gameObject.transform.position - camera.transform.position;
            if (Physics.Raycast(camera.transform.position, playerDirection, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    CountDown.cd.ActivatePoliceTime();
                    isPlayerVisible = true;
                    Debug.Log("Enter Camera vision te veo Player");
                }
            }
            else
            {
                isPlayerVisible = false;
                Debug.Log("Enter Camera vision te dejo de ver Player");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !isPlayerVisible)
        {
            playerDirection = other.gameObject.transform.position - camera.transform.position;
            if (Physics.Raycast(camera.transform.position, playerDirection, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    CountDown.cd.ActivatePoliceTime();
                    isPlayerVisible = true;
                    Debug.Log("Camera vision te veo Player");
                }
            }
            else
            {
                isPlayerVisible = false;
                Debug.Log("Camera vision te dejo de ver Player");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("exit player");
            other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            isPlayerVisible = false;
        }
    }
}
