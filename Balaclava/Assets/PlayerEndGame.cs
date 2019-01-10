using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerEndGame : MonoBehaviour
{

    public GameObject rewardImage;
    public GameObject endGameImage;

    public int cofresPorNivel;

    private int cofresDesbloqueados;


    // Start is called before the first frame update
    void Start()
    {
        cofresDesbloqueados = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void rewardPlayer() {
        cofresDesbloqueados++;
        StartCoroutine(reward());
        if (cofresDesbloqueados == cofresPorNivel) {
            endGame();
        }
    }

    public void endGame() {
        StartCoroutine(end());
    }

    IEnumerator reward()
    {
        //rewardImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        rewardImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        rewardImage.SetActive(false);
    }

    IEnumerator end() {
        //endGameImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        endGameImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        endGameImage.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;

    }
}
