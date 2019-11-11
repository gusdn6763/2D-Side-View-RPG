using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OrderManager는 현재 씬뷰의 하이브러리에서 MovingObject를 상속
//받고 있는 NPC, 몹, 플레이어, 동료의 정보를 받고 각각의 이벤트성
//움직임을 제어하기 위함

public class OrderManager : MonoBehaviour
{
    //이벤트도중 키입력 방지
    internal PlayerManager thePlayer;

    private List<MovingObject> characters;

    //private CameraManager cameraMove;

    public void Awake()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        PreLoadCharacter();
    }

    //리스트 타입은 하이브러리에 있는 MovingObject(s)OfType 을(를)
    //탐색 할 수 없으니 배열로 받을려고 함수를 2개 만듬
    //반환타입 List<MovingObject>
    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void PreLoadCharacter()
    {
        characters = ToList();
    }


    //MovingObject의 Move 함수를 쓰기위함
    public void Move(string _name, string _dir, float distance, int frequency)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].ObjectName)
            {
                characters[i].Move(_dir, distance, frequency);
            }
        }
    }

    //이거 왜했는지 나도 모름
    public void SetTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].ObjectName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].ObjectName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }

    public void PlayerNotMove()
    {
        thePlayer.notMove = true;
    }

    public void PlayerMove()
    {
        thePlayer.notMove = false;
    }

}

