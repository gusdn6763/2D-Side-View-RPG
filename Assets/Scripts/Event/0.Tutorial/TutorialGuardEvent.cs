using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuardEvent : MonoBehaviour
{
    public Dialogue dialogue1;
    private DialogueManager theDM;

    private OrderManager theOrder;
    private PlayerManager thePlayer;

    public GameObject see;

    private bool flag;

    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        flag = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!flag && Input.GetKey(KeyCode.Z))   
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {

        theOrder.PlayerNotMove();

        yield return new WaitForSeconds(1f);

        see.SetActive(true);


        theOrder.PlayerMove();

        gameObject.GetComponent<TutorialGuardEvent>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
