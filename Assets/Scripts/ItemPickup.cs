using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public int itemID;

    public int itemCount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            //자주 호출이 이루어지는 경우엔
            //변수로 만들어서 호출하는 경우가 좋음
            //이 경우는 단 한번만 쓰이고 버릴거므로 괜찮음
            //AudioManager.instance;
            Inventory.instance.GetAnItem(itemID, itemCount);
            Destroy(this.gameObject);
        }
    }
}
