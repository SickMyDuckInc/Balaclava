using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSymbolController : MonoBehaviour
{
    public GameObject[] symbols = new GameObject[4];


    private int value;
    private int key;
    private int tens;
    private bool NoLetter = true;

    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        for (int i = 0; i < symbols.Length; i++)
        {
            
        }

        for (int i = 0; i < symbols.Length; i++)
        {
            switch (symbols[i].GetComponent<SymbolString>().value)
            {
                case "A":
                case "a":
                    key = 0905;
                    NoLetter = false;
                    break;
                case "S":
                case "s":
                    key = 1501;
                    NoLetter = false;
                    break;
                case "E":
                case "e":
                    key = 2008;
                    NoLetter = false;
                    break;
                case "X":
                case "x":
                    key = 2201;
                    NoLetter = false;
                    break;
                case "P":
                case "p":
                    key = 2204;
                    NoLetter = false;
                    break;
                case "J":
                case "j":
                    key = 2612;
                    NoLetter = false;
                    break;
                default:
                    int aux;
                    int.TryParse(symbols[i].GetComponent<SymbolString>().value, out aux);
                    value += aux;   
                    break;
            }
        }

        if (NoLetter)
        {
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
        }
        

        GetComponent<PanelDoorSymbolController>().key = key;
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
