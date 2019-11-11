using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public Text text;

    private PlayerManager thePlayer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            PlayerManager.instance.ObjectName = text.text;
            Destroy(this.gameObject);
        }
    }
}
