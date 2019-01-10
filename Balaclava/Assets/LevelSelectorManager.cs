using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelector(int level)
    {
        SceneManager.LoadScene("Scene0" + level);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("GuideOrPlayer");
    }
}
