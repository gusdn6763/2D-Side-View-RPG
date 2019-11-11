using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCanvas : MonoBehaviour
{
    static public SystemCanvas instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }
}
