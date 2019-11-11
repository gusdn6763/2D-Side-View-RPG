using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//페이드 인, 아웃 효과
public class FadeManager : MonoBehaviour
{
    public SpriteRenderer black;
    private Color color;
    internal bool fadeInCheck;
    internal bool fadeOutCheck;

    private void Awake()
    {

    }

    private void Start()
    {
        fadeInCheck = false;
        fadeOutCheck = false;
    }

    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));
    }

    IEnumerator FadeOutCoroutine(float _speed)
    {
        fadeOutCheck = false;
        color = black.color;

        while (color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeOutCheck = true;
    }

    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }

    IEnumerator FadeInCoroutine(float _speed)
    {
        fadeInCheck = false;
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        fadeInCheck = true;
    }

}