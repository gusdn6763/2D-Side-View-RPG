using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//텔레포트
public class TransferMap : MonoBehaviour
{
    private PlayerManager player;

    private FadeManager fade;

    public string TeleportmapName;

    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        fade = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && Input.GetAxisRaw("Vertical") == 1)
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        fade.FadeOut();
        yield return new WaitUntil(() => fade.fadeOutCheck = true);
        fade.FadeIn();
        player.currentMapName = TeleportmapName;
        SceneManager.LoadScene(TeleportmapName);
    }
}
