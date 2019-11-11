using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    //플레이어가 텔레포트 이용시(씬 이동시) 이 스크립트가 포함된
    //게임 오브젝트로 이동 인스펙터에서 맵의 이름 값을 넣어야함
    private PlayerManager player;
    private FadeManager fade;

    public string startPoint;

    private void Awake()
    {
        fade = FindObjectOfType<FadeManager>();
    }
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();

       if (startPoint == player.currentMapName)
        {
            Debug.Log(player.currentMapName);
            Debug.Log(startPoint);
            player.transform.position = this.transform.position;
 
        }
    }
}
