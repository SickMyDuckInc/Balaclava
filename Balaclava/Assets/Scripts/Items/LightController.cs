using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //rojo amarillo y azul
    public GameObject []colors = new GameObject[3];
    public Material[] colorsMaterialOn = new Material[3];
    private Material[] colorsMaterialOff = new Material[3];

    public int []key = new int[4];
    private List<string[]> blink = new List<string[]>()
    {
        new string[]{"red","yellow","blue"},
        new string[]{"red"},
        new string[]{"red","red"},
        new string[]{"yellow"},
        new string[]{"yellow","yellow"},
        new string[]{"blue"},
        new string[]{"blue", "blue"},
        new string[]{"red","yellow"},
        new string[]{"blue","red"},
        new string[]{"yellow","blue"}
    };


    public float timeBlink = 0.5f;
    public float timeBetweenDigit= 1;
    public float timeBetweenBlink = 0.2f;
    public float timeFinish = 2;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        colorsMaterialOff[0] = colors[0].GetComponent<Renderer>().material;
        colorsMaterialOff[1] = colors[1].GetComponent<Renderer>().material;
        colorsMaterialOff[2] = colors[2].GetComponent<Renderer>().material;


        coroutine = WaitSequence();
        StartCoroutine(coroutine);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitSequence()
    {
        while (true)
        {
            //Comenzamos la corrutina de la secuencia completa
            yield return StartCoroutine(WaitBetweenDigit());
            //Esperamos el tiempo de la secuencia completa
            yield return new WaitForSeconds(timeFinish);

        }
    }

    private IEnumerator WaitBetweenDigit()
    {
        for(int i = 0; i<key.Length; i++)
        {
            //Comenzamos la corrutina de cada parte de la secuencia
            yield return StartCoroutine(WaitBetweenBlink(i));

            yield return new WaitForSeconds(timeBetweenDigit);
            
        }
    }

    private IEnumerator WaitBetweenBlink(int index)
    {
        for (int i = 0; i < blink[key[index]].Length; i++)
        {
            
            yield return StartCoroutine(WaitNextBlink(blink[key[index]][i]));
            yield return new WaitForSeconds(timeBetweenBlink);
        }
    }

    private IEnumerator WaitNextBlink(string color)
    {
        int index = 0;
        switch (color)
        {
            case "red":
                index = 0;
                break;
            case "yellow":
                index = 1;
                break;
            case "blue":
                index = 2;
                break;
        }
        colors[index].GetComponent<Renderer>().material = colorsMaterialOn[index];
        yield return StartCoroutine(WaitLightsOff(index));
    }

    private IEnumerator WaitLightsOff (int index)
    {
        yield return new WaitForSeconds(timeBlink);
        colors[index].GetComponent<Renderer>().material = colorsMaterialOff[index];        
    }
}
