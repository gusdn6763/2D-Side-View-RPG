using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MovingObject
{
    protected BoxCollider2D box;
    protected CameraManager boxBound;

    //NPC의 상태를
    //1.가만히 있음   
    //2.인스펙터에서 받는 값에 따라 고정적인 움직임(구현), 이벤트 이동으로 활용가능
    //3.완전한 램덤움직임(미구현이지만 만들고 싶음)

    //가만히 있으면 플레이어 카메라 크기만큼의 박스콜라이더 크기를 만들고
    //박스콜라이더를 크게만들었으니 항상 플레이어 위치를 바라보는형식
    //이동 움직임이면 Filp()함수로 방향에 따라 이미지 반전과 박스크기를 
    //줄이고 특정 키(예를 들어 상점,상호작용)를 누르면 플레이어 위치를 바라보는 형식
    private int randomState;

    protected  void Awake()
    {
        sp = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        box = GetComponent<BoxCollider2D>();

        boxBound = GameObject.Find("Main Camera").GetComponent<CameraManager>();

        queue = new Queue<string>();

        CheckState();
    }

    private void Update()
    {
        if(randomState ==1)
        {
            Filp();
        }
    }

    private void CheckState()
    {
        //0 => 인스펙터에 받은 값에 따라 움직임
        //1 => 완전 고정 상태
        //2 => 미구현
        randomState = Random.Range(0, 1);

        if (randomState == 0)
        {
            box.size = new Vector2(2, 2);
            StartCoroutine(MoveCoroutine());
        }

        else if (randomState == 1)
        {
            box.size = new Vector2(boxBound.halfWidth * 2, boxBound.halfHeight * 2);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (randomState == 1)
            {
                if (collision.gameObject.transform.position.x > this.gameObject.transform.position.x)
                {
                    sp.flipX = false;
                }
                else if (collision.gameObject.transform.position.x < this.gameObject.transform.position.x)
                {
                    sp.flipX = true;
                }
            }

            else if (randomState == 0)
            {
                Filp();
            }

            else if(randomState == 0  && Input.GetKey(KeyCode.Z))
            {
                  if (collision.gameObject.transform.position.x > this.gameObject.transform.position.x)
                  {
                      sp.flipX = false;
                  }
                  else if (collision.gameObject.transform.position.x < this.gameObject.transform.position.x)
                  {
                      sp.flipX = true;
                  }

                //+  NPC 매니저를 상속받는 개별 NPC 스크립트를 
                //만들고 이 온트리거 함수를 상속받고
                //base.OnTriggerStay2D(collision) 실행후 NPC와의 상호
                //작용 후 bool타입변수를 바꾸어줌으로써 코루틴 다시실행 
            }
        }
    }

    IEnumerator MoveCoroutine()
    {
        if (eventMove.movingDirection.Length != 0)
        {
            for (int i = 0; i < eventMove.movingDirection.Length; i++)
            {
                yield return new WaitUntil(() => queue.Count<2);
                //따로 Moving Object에서 Move 함수를 만든 이유는 
                //NPC말고 몹,동료,플레이도 이용 할 수 있게하기위함

                //즉 MovingObject는 이벤트성 움직임 
                //이 MoveCoroutine함수는 인스펙터에 받은 값에따라
                //무한이 움직이는 규칙을 가진 움직임
                Move(eventMove.movingDirection[i], eventMove.movingFrequency);

                //무한반복
                //ex) LEFT, RIGHT를 받으면 좌우로 무한반복
                //ex) 이게 없으면 LEFT를 한번만 받으면 한번 이동
                if(i== eventMove.movingDirection.Length-1)
                {
                    i = -1;
                }
            }
        }
    }
}
