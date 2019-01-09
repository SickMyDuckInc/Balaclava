using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool panelActive = false;

    public void EnablePanel()
    {
        panelActive = true;
    }

    public void DisablePanel()
    {
        panelActive = false;
    }

}
