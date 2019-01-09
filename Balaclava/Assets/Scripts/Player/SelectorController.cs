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
    //action button
    public GameObject ActionButton;
    public GameObject HelpText;

    private GameObject selectedObject;


    private bool test = false;

    private void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if(camera == null)
            camera = GameObject.FindGameObjectWithTag("MainCamera");

        ActionButton.SetActive(false);
        HelpText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SelectableObject")
        {
            // Change the cube color to green.
            MeshRenderer mesh = other.gameObject.GetComponent<MeshRenderer>();
            oldMaterial = mesh.material;
            mesh.material = iluminated;
            selectable = true;
            selectedObject = other.gameObject;
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
            if (Input.GetMouseButton(0) && selectable)
            {

                //player.transform.position = new Vector3(5.76f, 0.56f, 3f);

                player.GetComponent<MovementController>().DisableMovement();
                player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
                camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
                selectedObject.GetComponentInChildren<Panel>().EnablePanel();
                camera.GetComponent<RotationController>().DisableRotation(selectedObject);
                //Debug.Log(player.transform.rotation.ToString());
            }
            if (test)
            {
                //Debug.Log(player.transform.rotation.ToString());
            }
        }        
    }

    //Only when isDevice 
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
        player.GetComponent<PlayerController>().EnableMovement();

        selected.GetComponentInChildren<Panel>().DisablePanel();

        if (SpawnerPlayer.ISDEVICE)
        {
            HelpText.SetActive(false);
        }
    }
}
