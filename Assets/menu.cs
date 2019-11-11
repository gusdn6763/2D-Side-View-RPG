using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public static menu instance;
    public GameObject go;
    public OrderManager theOrder;
    private bool activated;
    public GameObject[] gos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToTitle()
    {
        for(int i=0;i<gos.Length;i++)
        {
            Destroy(gos[i]);
        }
        go.SetActive(false);
        activated = false;
        SceneManager.LoadScene("Title");
    }
    public void Continue()
    {
        activated = false;
        go.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;


            if(activated)
            {
                go.SetActive(true);
                theOrder.PlayerMove();
            }

            else
            {
                go.SetActive(false);
                theOrder.PlayerNotMove();
            }
        }
    }
}
