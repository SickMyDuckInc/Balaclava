using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : Panel
{
    public int key;
    private int numberIntroduce;
    private int count;
    private int tries;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        numberIntroduce = 0;
        tries = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked(int button)
    {
        if (panelActive)
        {
            count++;
            numberIntroduce *= 10;
            numberIntroduce += button;

            if (count == 4)
            {
                if (numberIntroduce == key)
                {
                    Debug.Log("Exito");
                    //Abrir puerta o lo que sea
                    transform.parent.gameObject.tag = "Untagged";
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RotationController>().EnableRotation();
                }
                else
                {
                    tries++;
                    if (tries == 3)
                    {
                        Debug.Log("has perdido");
                    }
                    else
                    {
                        count = 0;
                        numberIntroduce = 0;
                        Debug.Log("Error, intentalo de nuevo");
                    }
                }
            }
        }
        
    }
}
