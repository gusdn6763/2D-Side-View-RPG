using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventMove
{
    [Tooltip("딜레이 시간, 3초 ,2초, 1초, 0.5초, 없음")]
    [Range(0, 5)]
    public int movingFrequency;

    [Tooltip("RIGHT-오른쪽, LEFR-왼쪽")]
    public string[] movingDirection;

    [Tooltip("이동 거리")]
    public float[] movingDistance;
}


public class MovingObject : MonoBehaviour
{
    [Header("이동관련")]
    [SerializeField]
    [Tooltip("인스펙터에서 받는 값은 무한 반복하는 움직임이고" +
        "theOrder스크립트를 통해서 받는 값은정해진만큼 " +
        "움직이는 움직임")]
    public EventMove eventMove;

    internal Animator animator;
    protected SpriteRenderer sp;

    //자기 자신, 빠른 접근과 편의성
    protected Transform trans;

    public Queue<string> queue;

    protected Vector3 vector;
    public LayerMask layerMask;

    public float speed;
    public string ObjectName;         //씬 변경 및 이벤트 이동확인시 확인하기 위한 변수
    internal bool notMove = false;    //나중에 이벤트 및 상점이동시 플레이어 이동제한 하기 위한 변수

    protected void Filp()             //이동하는 방향에 따라 이미지 반전
    {
        if (vector.x > 0)
        {
            sp.flipX = true;
        }

        else if (vector.x < 0)
        {
            sp.flipX = false;
        }
    }

    protected void CheckCollsion()
    {
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x+vector.x, vector.y * speed );

        hit = Physics2D.Linecast(start, end, layerMask);

        if(hit.transform !=null)
        {
            vector.Set(0, 0, 0);
            animator.SetBool("Walking", false);
        }
    }

    public void Move(string dir, float distance, int frequency=5)    //MovingObject 가 포함된 스크립트는 이 함수를 통해 이벤트성 움직임을 제어 할 수있음
    {
        //이동 명령 수만큼 큐에 삽입
        //ex) 1번 타자 -> LEFT  2번 타자 -> RIGHT
        queue.Enqueue(dir);

        if (!notMove)
        {
            notMove = true;
            StartCoroutine(MoveCoroutine(dir, distance, frequency));
        }
    }


    IEnumerator MoveCoroutine(string dir,float distance,int frequency)
    {
        while (queue.Count !=0)
        {
            string direction = queue.Dequeue();

            //초기화
            vector.Set(0, 0, vector.z);

            //대기시간
            switch (frequency)
            {
                case 1:
                    yield return new WaitForSeconds(3f);
                    break;
                case 2:
                    yield return new WaitForSeconds(2f);
                    break;
                case 3:
                    yield return new WaitForSeconds(1f);
                    break;
                case 4:
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 5:
                    break;
            }

            switch (dir)
            {
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }

            animator.SetBool("Walking", true);

            CheckCollsion();
            Filp();
 
            for (int i = 0; i < distance*10 ; i++)
            {
                transform.Translate(vector.x * speed * 0.1f, 0, 0);
                yield return new WaitForSeconds(0.01f);
            }

            //5값을 받으면 항상 이동을 하니까 이동모션을 끄지않음
            if (frequency != 5)
            {
                animator.SetBool("Walking", false);
            }

        }
        animator.SetBool("Walking", false);
        notMove = false;
    }
}