using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectorController : MonoBehaviour
{
    public Material iluminated;
    public Material redOne;
    public Material keyMat;
    Material oldMaterial;
    bool selectable = false;
    bool key = false;
    bool door = false;
    bool drawer = false;
    bool keySelected = false;
    bool drawerOpen = false;
    public GameObject player;
    public GameObject camera;
    //action button
    public GameObject ActionButton;
    public GameObject HelpText;

    private GameObject selectedObject;
    private GameObject doorObject;

    public Animator drawerAnim;

    //for device helpText
    private const string CHECK_KEY = "Comprueba si hay una llave en el cajón";
    private const string TAKED_KEY = "Llave cogida";
    private const string INTRODUCE_KEY = "Introduce el codigo para desbloquear";

    private bool test = false;

    private void Start()
    {
        Application.ExternalCall("OnAppReady()");
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (camera == null)
            camera = GameObject.FindGameObjectWithTag("MainCamera");

        drawerAnim = GameObject.FindGameObjectWithTag("FullDesk").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SelectableObject" || (other.gameObject.tag == "KeyObject" && drawerOpen) || (other.gameObject.tag == "DrawerObject" && !drawerOpen))
        {
            if (other.gameObject.tag == "KeyObject" && drawerOpen)
            {
                keySelected = true;
            }
            else if(other.gameObject.tag == "DrawerObject" && !drawerOpen)
            {
                drawer = true;
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
            
            oldMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
            if (key)
            {
                other.gameObject.GetComponent<MeshRenderer>().material = iluminated;
            }
            else
            {
                other.gameObject.GetComponent<MeshRenderer>().material = redOne;
            }
            
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
            other.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            door = false;
        }
        else if(other.gameObject.tag == "DrawerObject" && !drawerOpen)
        {
            other.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            drawer = false;
        }
        else if(other.gameObject.tag == "KeyObject" && drawerOpen)
        {
            other.gameObject.GetComponent<MeshRenderer>().material = keyMat;
            keySelected = false;
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
            if (selectable || door || keySelected && drawerOpen || drawer)
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
            if (Input.GetMouseButtonDown(0))
            {
                //player.transform.position = new Vector3(5.76f, 0.56f, 3f);
                if (selectable)
                {
                    selectedObject.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
                    player.GetComponent<MovementController>().DisableMovement();
                    player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
                    player.GetComponent<Rigidbody>().useGravity = false;
                    player.GetComponent<AudioSource>().Pause();
                    camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
                    selectedObject.GetComponentInChildren<Panel>().EnablePanel();
                    camera.GetComponent<RotationController>().DisableRotation(selectedObject);
                }
                if (door)
                {
                    doorObject.GetComponent<MeshRenderer>().material = oldMaterial;
                    doorObject.GetComponentInChildren<DoorController>().CheckDoor(key);
                }
                if (keySelected && drawerOpen)
                {
                    key = true;
                    drawer = false;
                    selectedObject.SetActive(false);
                    //Destroy(selectedObject);
                    keySelected = false;
                }
                if (drawer)
                {
                    drawerAnim.SetTrigger("OpenDrawer");
                    selectedObject.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
                    drawerOpen = true;
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
        if (selectable)
        {
            selectedObject.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            player.GetComponent<PlayerController>().DisableMovement();
            player.transform.position = selectedObject.GetComponent<PlayerPosition>().playerPosition;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<AudioSource>().Pause();
            camera.transform.rotation = Quaternion.Euler(selectedObject.GetComponent<PlayerPosition>().playerRotation);
            selectedObject.GetComponentInChildren<Panel>().EnablePanel();
            player.GetComponent<PlayerController>().DisableRotation(selectedObject);
            HelpText.SetActive(true);
            HelpText.GetComponent<TextMeshProUGUI>().SetText(INTRODUCE_KEY);
        }
        if (door)
        {
            doorObject.GetComponent<MeshRenderer>().material = oldMaterial;
            doorObject.GetComponentInChildren<DoorController>().CheckDoor(key);
        }
        if (keySelected && drawerOpen)
        {
            key = true;
            drawer = false;
            Destroy(selectedObject);
            keySelected = false;
            HelpText.SetActive(true);
            HelpText.GetComponent<TextMeshProUGUI>().SetText(TAKED_KEY);
        }
        if (drawer)
        {
            drawerAnim.SetTrigger("OpenDrawer");
            selectedObject.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
            drawerOpen = true;
            HelpText.SetActive(true);           
            HelpText.GetComponent<TextMeshProUGUI>().SetText(CHECK_KEY);
        }

        ActionButton.SetActive(false);        
    } 

    public void ReturnControl(GameObject selected)
    {
        selectable = false;
        player.GetComponent<PlayerController>().EnableMovement();

        selected.GetComponentInChildren<Panel>().DisablePanel();
        player.GetComponent<Rigidbody>().useGravity = true;

        if (SpawnerPlayer.ISDEVICE)
        {
            HelpText.SetActive(false);
        }
    }
}
