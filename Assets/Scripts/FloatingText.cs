using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public float destoryTime;

    public Text text;

    private Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        vector.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime*0.1f), text.transform.position.z);
        text.transform.position = vector;

        destoryTime -= Time.deltaTime;

        if(destoryTime<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
