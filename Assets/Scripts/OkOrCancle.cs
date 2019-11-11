using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OkOrCancle : MonoBehaviour
{
    public GameObject upPanel;
    public GameObject downPanel;

    public Text okText;
    public Text cancleText;



    public bool activated;      //활성화 확인 유무
    private bool keyInput;      //키입력 중복 방지
    private bool result;        //확인 ,취소 결과 반환

    public void Selected()
    {
        result = !result;
        if(result)
        {
            upPanel.gameObject.SetActive(false);
            downPanel.gameObject.SetActive(true);
        }
        else
        {
            upPanel.gameObject.SetActive(true);
            downPanel.gameObject.SetActive(false);
        }
    }

    //중복키 방지
    IEnumerator ShowTwoChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        keyInput = true;
    }

    public void ShowTwoChoice(string upText,string downText)
    {
        activated = true;
        result = true;
        okText.text = upText;
        cancleText.text = downText;
        this.upPanel.gameObject.SetActive(false);
        this.downPanel.gameObject.SetActive(true);
        StartCoroutine(ShowTwoChoiceCoroutine());
    }

    public bool GetResult()
    {
        return result;
    }

    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selected();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                keyInput = false;
                activated = false;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                keyInput = false;
                activated = false;
                result = false;
            }
        }
    }
}
