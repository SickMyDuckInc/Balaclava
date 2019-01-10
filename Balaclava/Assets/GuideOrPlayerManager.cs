using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideOrPlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUrldefined()
    {
        Application.OpenURL("https://sickmyduckinc.github.io/BalaclavaManual/");
    }
    
    public void LevelSelector()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
