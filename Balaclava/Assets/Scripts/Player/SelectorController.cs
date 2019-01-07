using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Material iluminated;
    Material oldMaterial;
    bool selectable = false;
    public GameObject player;
    public GameObject camera;

    private bool test = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SelectableObject")
        {
            // Change the cube color to green.
            oldMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
            other.gameObject.GetComponent<MeshRenderer>().material = iluminated;
            selectable = true;
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
    }

    private void Update()
    {
        //Click izquierdo
        if (Input.GetMouseButton(0) && selectable)
        {
            
            player.transform.position = new Vector3(5.76f, 0.56f, 3f);
            camera.GetComponent<RotationController>().DisableRotation();
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            camera.transform.rotation = Quaternion.Euler(34f,0f,0f);
            Debug.Log(player.transform.rotation.ToString());
        }
        if (test)
        {
            //Debug.Log(player.transform.rotation.ToString());
        }
    }
}
