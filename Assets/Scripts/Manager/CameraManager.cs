using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventCamera
{
    internal bool eventCameraMoveFlag = false;
    public string camerDirection;
    public float cameraSpeed;
    public float cameraDistance;
    //waitForUntil (()=>CameraMoved=true로 사용할려고
    internal bool CameraMoved = false;
}


public class CameraManager : MonoBehaviour
{
    [Header("이벤트성 카메라 이동관련")]
    [SerializeField]
    public EventCamera eventCamera;

    static public CameraManager instance;

    internal Transform trans;  //카메라 자신

    //플레이어, 타깃
    public GameObject target;
    //맵밖을 볼수 없도록 하는 경계선
    public BoxCollider2D bound;
    //카메라 크기를 얻기위한 클래스
    private Camera theCamera;

    //카메라 크기
    private Vector3 targetPosition;
    private Vector3 minBound;
    private Vector3 maxBound;

    public float moveSpeed; // 카메라 속도
    internal float halfWidth;
    internal float halfHeight;

    private void Awake()
    {
        trans = GetComponent<Transform>();
        theCamera = GetComponent<Camera>();

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    //대충 씬뷰에서 보고있는 카메라 크기의 절반
    private void Start()
    {
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        //이벤트성 카메라움직임이 시작되면 멈춤
        if (!eventCamera.eventCameraMoveFlag)
        {
            if (target.gameObject != null)
            {
                targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

                trans.position = Vector3.Lerp(trans.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 movespeed만큼 이동.

                float clampedX = Mathf.Clamp(trans.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
                float clampedY = Mathf.Clamp(trans.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

                trans.position = new Vector3(clampedX, clampedY, trans.position.z);

            }
        }
    }

    public void EventCameraMove(string _dir, float _speed, float _distance)
    {
        eventCamera.eventCameraMoveFlag = true;
        eventCamera.CameraMoved = false;
        StartCoroutine(EventCaameraMove(_dir, _speed, _distance));
    }

    IEnumerator EventCaameraMove(string _dir, float _speed, float _distance)
    {
        Vector3 vector = new Vector3(0, 0, 0);

        for (int i = 0; i < _distance * 10; i++)
        {
            switch (_dir)
            {
                case "UP":
                    vector.Set(0, 0.1f, 0);
                    break;

                case "DOWN":
                    vector.Set(0, -0.1f, 0);
                    break;

                case "LEFT":
                    vector.Set(-0.1f, 0, 0);
                    break;

                case "RIGHT":
                    vector.Set(0.1f, 0, 0);
                    break;
            }

            trans.Translate(vector.x, vector.y, vector.z);
            yield return new WaitForSeconds(0.1f / _speed);
        }
        eventCamera.CameraMoved = true;
    }

    public void CameraMove()
    {
        eventCamera.eventCameraMoveFlag = false;
    }

    //새로운 씬뷰로 이동시 박스콜라이더 있는 오브젝트에서 Bound라는 스크립트가
    //존재시 맵밖을 볼수 없도록 하는 경계선을 메인 카메라에 대입함
    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}