using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSymbolController : MonoBehaviour
{
    public GameObject[] symbols = new GameObject[4];
    public GameObject model;

    private int value;
    private string code;
    private int key;
    private int tens;

    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        for (int i = 0; i < symbols.Length; i++)
        {
            value += symbols[i].GetComponent<Symbol>().value;
        }
        code = model.GetComponent<DoorModel>().code;


        switch (code)
        {
            case "A":
            case "a":
                key = 0905;
                break;
            case "S":
            case "s":
                key = 1501;
                break;
            case "E":
            case "e":
                key = 2008;
                break;
            case "X":
            case "x":
                key = 2201;
                break;
            case "P":
            case "p":
                key = 2204;
                break;
            case "J":
            case "j":
                key = 2612;
                break;
            default:
                tens = Dozens(value);
                if (tens == 2)
                {
                    key = value + value * 100;
                }
                else if (tens == 1)
                {
                    key = value + value * 10 + value * 100 + value * 1000;
                }
                else
                {
                    key = value;
                }
                    break;
        }
        

        GetComponent<PanelController>().key = key;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int Dozens(int number)
    {
        int i = 0;
        while(number%10 != 0)
        {
            number = number / 10;
            i++;
        }
        return i;
    }
}
