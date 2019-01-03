using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Material iluminated;
    Material oldMaterial;
    bool selectable = false;

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
}
