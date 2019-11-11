using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private BoxCollider2D border;
    private CameraManager bordercamera;


    void Awake()
    {
        border = GetComponent<BoxCollider2D>();
        bordercamera = FindObjectOfType<CameraManager>();

    }

    private void Start()
    {
        bordercamera.SetBound(border);
    }
}
