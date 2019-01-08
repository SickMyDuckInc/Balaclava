using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    public int index;
    public PanelController parent;
    public Material selected;
    public float time;

    private Material oldMaterial;
    private IEnumerator coroutine;

    public void Start()
    {
        oldMaterial = GetComponent<Renderer>().material;
    }
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Debug.Log("si");
        parent.ButtonClicked(index);
        GetComponent<Renderer>().material = selected;

        coroutine = WaitAndPrint(time);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
       
        yield return new WaitForSeconds(waitTime);
        GetComponent<Renderer>().material = oldMaterial;
    }
}
