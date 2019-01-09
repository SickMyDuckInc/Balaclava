using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskTest : MonoBehaviour
{    
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("OpenDrawer");
        StartCoroutine(waitToClose());
    }

    IEnumerator waitToClose()
    {
        yield return new WaitForSeconds(3);
        anim.SetTrigger("CloseDrawer");
    }
}
