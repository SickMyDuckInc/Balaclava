using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddController : MonoBehaviour
{
    public GameObject[] symbols = new GameObject[3];
    public GameObject model;

    private int value;
    private int code;
    private int key;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initiateBoxModel()
    {
        value = 0;
        for (int i = 0; i < symbols.Length; i++)
        {
            value += symbols[i].GetComponent<Symbol>().value;
        }
        code = model.GetComponent<BoxModel>().code;

        int lastDigit = code % 10;
        bool par = value % 2 == 0;

        if (lastDigit == 5)
        {
            if (par)
            {
                key = 6567;
            }
            else
            {
                key = 2394;
            }
        }
        else if (lastDigit == 3 && par)
        {
            key = 1107;
        }
        else if (lastDigit == 9 && par)
        {
            key = 2209;
        }
        else
        {
            key = 0040;
        }

        GetComponent<PanelController>().key = key;
    }
}
