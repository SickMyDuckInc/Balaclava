using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProLasserController : MonoBehaviour
{
    public float time = 3;

    public GameObject []lights = new GameObject[4];

    public Material lightOn;
    public Material lightOff;

    private int selected;
    private bool laserIsActive;
    private bool finish = false;

    private IEnumerator coroutine;

    private List<bool[]> possibilities = new List<bool[]>()
    {
        new bool[]{true,true,false,false },
        new bool[]{true,false,true,false },
        new bool[]{true,false,false,true},
        new bool[]{false,true,true,false},
        new bool[]{false,true,false,true},
        new bool[]{false,false,true,true}
    };
    
    // Start is called before the first frame update
    void Start()
    {
        laserIsActive = true;

        selected = 0;

        coroutine = WaitAndPrint(time);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            EnableLasers();
            selected = Random.Range(0, 6);
            bool[] selection = possibilities[selected];

            for(int i= 0; i< selection.Length; i++)
            {
                if (selection[i])
                {
                    lights[i].GetComponent<Renderer>().material = lightOn;
                }
                else
                {
                    lights[i].GetComponent<Renderer>().material = lightOff;
                }
            }
            if(selected == 0 || selected == 3 || selected == 5)
            {
                DisableLasers();
            }

        }
    }

    private void EnableLasers()
    {
        laserIsActive = true;
    }

    private void DisableLasers()
    {
        laserIsActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (laserIsActive)
        {
            if (other.gameObject.tag == "Player" && !finish)
            {
                GameObject.Find("PlayManager").GetComponent<PlayerEndGame>().endGame();
                finish = true;
            }
        }
    }
}
