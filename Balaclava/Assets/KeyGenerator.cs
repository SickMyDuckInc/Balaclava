using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviour
{
    int[] symbolsBox = new int[4]{
        1,3,6,10 };
    int[] codeBox = new int[6]
    {
        15,13,19,15,19,15
    };
    public Material[] symbolMaterial = new Material[4];
    public Material[] codeMaterials = new Material[6];
    public Material[] symbolsDoor = new Material[28];
    public Material[] letterDoor = new Material[6];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateKey()
    {
        if (this.name == "SafeLockerMove56")
        {
            GenerateKey(50);
        }
        else if(this.name == "SafeLockerMove01")
        {
            GenerateKey(10);
        }
        else if(this.name == "panel56")
        {
            GeneratekeyLights();
        }
        else if(this.name == "SafeDoor555")
        {
            GenerateKeySymbolBox();
        }
        else if(this.name == "panelSymbol")
        {
            GenerateKeyPanelSymbol();
        }

    }

    private void GenerateKey(int number)
    {
        int[] totalKey = new int[4];
        int count = 0;
        while (count < 4)
        {
            int key = Random.Range(0, number);
            if(count == 0)
            {
                totalKey[count] = key;
                count++;
            }
            else
            {
                if(totalKey[count-1] != key)
                {
                    totalKey[count] = key;
                    count++;
                }
            }
        }

        GetComponent<LockController>().keyNumber = totalKey;
    }

    private void GeneratekeyLights()
    {
        int key = Random.Range(0, 10);
        LightController light = GetComponent<LightController>();
        light.key[0] = key;
        for(int i = 1; i<4; i++)
        {
            int rand = Random.Range(0, 10);
            light.key[i] = rand;
            key = key * 10 + rand; 
        }
        GetComponent<PanelController>().key = key;
        //GetComponent<LightController>().key[0] 
    }

    private void GenerateKeySymbolBox()
    {
        int[] numbers = new int[4];
        int[] index = new int[4];
        for (int i = 0; i < 4; i++)
        {
            int x = Random.Range(0, 4);
            numbers[i] = symbolsBox[x];
            index[i] = x;
        }
        AddController add = GetComponent<AddController>();
        add.symbols[0].GetComponent<Symbol>().value = numbers[0];
        add.symbols[1].GetComponent<Symbol>().value = numbers[1];
        add.symbols[2].GetComponent<Symbol>().value = numbers[2];
        add.symbols[3].GetComponent<Symbol>().value = numbers[3];
        add.symbols[0].GetComponent<Renderer>().material = symbolMaterial[index[0]];
        add.symbols[1].GetComponent<Renderer>().material = symbolMaterial[index[1]];
        add.symbols[2].GetComponent<Renderer>().material = symbolMaterial[index[2]];
        add.symbols[3].GetComponent<Renderer>().material = symbolMaterial[index[3]];

        int cod = Random.Range(0, 6);
        add.model.GetComponent<BoxModel>().code = codeBox[cod];
        add.model.GetComponent<Renderer>().material = codeMaterials[cod];

        add.initiateBoxModel();

    }

    private void GenerateKeyPanelSymbol()
    {

    }
}
