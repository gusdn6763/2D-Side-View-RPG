using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    //아이템 구별을 위한 아이디
    public int itemID;
    //아이템 이름
    public string itemName;
    //아이템 설명
    public string itemDescription;
    //아이템 소지개수
    public int itemCount;
    //아이템 아이콘
    public Sprite itemIcon;
    //아이템 종류
    public ItemType itemType;

    public int atk;
    public int def;
    public int addHp;
    public int addMp;


    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC
    }

    public Item(int itemID, string itemName, string itemDescription, ItemType itemType, 
        int atk = 0,int def=0, int addHp=0,int addMp=0, int itemCount = 1)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.itemType = itemType;
        this.itemCount = itemCount;
        this.itemIcon = Resources.Load("ItemIcon/" + itemID.ToString(),typeof(Sprite)) as Sprite;
        this.atk = atk;
        this.def = def;
        this.addHp = addHp;
        this.addMp = addMp;
    }
}
