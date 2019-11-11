using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private FadeManager fade;

    public GameObject playerActive;
    private PlayerManager player;
    private GameManager theGM;


    private void Awake()
    {
        fade = FindObjectOfType<FadeManager>();
        theGM = FindObjectOfType<GameManager>();
        player = playerActive.GetComponent<PlayerManager>();
    }


    public void StartGame()
    {
        StartCoroutine(WaitForGame());
    }

    IEnumerator WaitForGame()
    {
        fade.FadeOut();
        yield return new WaitUntil(() => fade.fadeOutCheck == true);
        playerActive.SetActive(true);

        theGM.LoadStart();
        player.currentSceneName = "Tutorial";
        SceneManager.LoadScene("Tutorial");
    }

    public void MenuButton()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
