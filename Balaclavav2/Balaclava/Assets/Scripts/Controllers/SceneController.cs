using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class SceneController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenUrlJs(string url);

    public void NextSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NextSceneById(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void LoadUrl(string url)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            OpenUrlJs(url);
        }
        else
        {
            Application.OpenURL(url);
        }
        
    }
}
