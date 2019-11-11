using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    private PlayerStat thePlayerStat;

    public GameObject floating_Text;
    public GameObject parent;

    private void Awake()
    {
        if(instance !=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    private void FloatingText(int number,string color)
    {
        GameObject clone = Instantiate(floating_Text, PlayerManager.instance.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = number.ToString();
        if (color =="RED")
        {
            clone.GetComponent<FloatingText>().text.color = Color.red;
        }

        else if(color =="BLUE")
        {
            clone.GetComponent<FloatingText>().text.color = Color.blue;
        }
        clone.transform.SetParent(parent.transform);
        clone.transform.localScale = new Vector3(1, 1, 1);
    }

    public void UseItem(int itemID)
    {
        switch(itemID)
        {
            case 10001:
                if(thePlayerStat.hp>=thePlayerStat.currentHp +10)
                {
                    thePlayerStat.currentHp += 10;
                }
                else
                {
                    thePlayerStat.currentHp = thePlayerStat.hp;
                }
                FloatingText(50, "RED");

                break;
        }
    }

    private void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        itemList.Add(new Item(10001, "체리", "체력회복 +10", Item.ItemType.Use));
        itemList.Add(new Item(20001, "검", "기본검", Item.ItemType.Equip,3));
    }
}
