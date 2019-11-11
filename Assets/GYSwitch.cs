using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IGYSwitch
{
    void SwitchIsOn(bool isOn);
}

public class GYSwitch : MonoBehaviour
{
    public RectTransform trans;

    public IGYSwitch switchController;

    bool isOn;

    public void onClick()
    {
        isOn = !isOn;

        if (isOn == true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
        }

        switchController.SwitchIsOn(isOn);
    }
    //클래스 3가지?

    //1.눈에 보이는 동작
    //2.눈에 보이지 않는 동작 ex)효과음 꺼짐
    //3.데이터 저장
    //mvc 패턴
}

/*
public class GYSwitch :MonoBehaviour
{
public delegate void SwitchIsOn(bool isOn);
public SwitchIsOn switchIsOn;
public RectTransform trans;

bool isOn;

public void onClickSwitch()
{
    isOn = !isOn;

    if (isOn == true)
    {
        trans.anchoredPosition = new Vector3(8, 0, 0);
    }
    else
    {
        trans.anchoredPosition = new Vector3(-8, 0, 0);
    }

    switchIsOn(isOn);
}
}
*/

    /*
public class GYSwitch : MonoBehaviour
{
    public Action<bool> switchIOnAction;

    public RectTransform trans;

    bool isOn;

    public void onClickSwitch()
    {
        isOn = !isOn;

        if (isOn == true)
        {
            trans.anchoredPosition = new Vector3(8, 0, 0);
        }
        else
        {
            trans.anchoredPosition = new Vector3(-8, 0, 0);
        }

        switchIOnAction(isOn);
    }
*/