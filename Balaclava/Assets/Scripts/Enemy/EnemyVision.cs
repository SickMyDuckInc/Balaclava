using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    //my position
    public Transform securityGuardPosition;

    public EnemyController em;

    Vector3 playerDirection;
    RaycastHit hit;

    private bool isPlayerVisible;

    //max distance of RayCast
    float maxDistance;
    //layer mask
    int layerMask;

    private void Start()
    {
        isPlayerVisible = false;
        maxDistance = GetComponent<BoxCollider>().size.z;
        layerMask = LayerMask.GetMask("Water");
        layerMask = ~layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!em.playerEnabledToSearch)
        {
            Debug.Log("Enter PlayerEnabledToSearch = false");
            return;
        }

        if (other.gameObject.tag.Equals("Player") && em.playerEnabledToSearch)
        {
            Debug.Log("Player detected");
            playerDirection = other.gameObject.transform.position - securityGuardPosition.position;
            Physics.Raycast(securityGuardPosition.position, playerDirection, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore);
            if (Physics.Raycast(securityGuardPosition.position, playerDirection, out hit))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    Debug.Log("VEO AL JUGADOR");
                    isPlayerVisible = true;
                }
            }
            else
            {
                if(!isPlayerVisible)
                    isPlayerVisible = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!em.playerEnabledToSearch)
        {
            Debug.Log("PlayerEnabledToSearch = false");
            return;
        }

        if (other.gameObject.tag.Equals("Player") && em.playerEnabledToSearch)
        {
            //Debug.Log("Player detected");
            playerDirection = other.gameObject.transform.position - securityGuardPosition.position;
            if (Physics.Raycast(securityGuardPosition.position, playerDirection, out hit))
            {
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    Debug.Log("VEO AL JUGADOR");
                    other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    isPlayerVisible = true;
                }
            }
            else
            {
                if (!isPlayerVisible)
                    isPlayerVisible = false;
            }
        }
    } 
}
