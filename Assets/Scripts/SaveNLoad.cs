using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Data
{
    public float playerX;
    public float playerY;
    public float playerZ;

    public int playerHP;
    public int playerMP;

    public int playerCurrentHP;
    public int playerCurrentMP;
    public int playerCurrentEXP;

    public int playerATK;
    public int playerDEF;

    public int added_atk;
    public int added_def;
    public int added_hpr;
    public int added_mpr;

    public List<int> playerItemInventory;           //자료형만 됨, Item 클래스 사용 불가
    public List<int> playerItemInventoryCount;
    public List<int> playerEquipItem;

    public string mapName;
    public string sceneName;

    public List<bool> swList;
    public List<string> swNameList;
    public List<string> varNameList;
    public List<float> varNumberList;
}

public class SaveNLoad : MonoBehaviour
{

    private PlayerManager thePlayer;
    private PlayerStat thePlayerStat;
    private DatabaseManager theDatabase;
    private Inventory theInven;
    private Equipment theEquip;

    public Data data;

    private FadeManager theFadeManager;

    private Vector3 vector;


    public void CallSave()
    {
        theDatabase = FindObjectOfType<DatabaseManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        thePlayerStat = FindObjectOfType<PlayerStat>();
        theEquip = FindObjectOfType<Equipment>();
        theInven = FindObjectOfType<Inventory>();
        

        data.playerX = thePlayer.transform.position.x;
        data.playerY = thePlayer.transform.position.y;
        data.playerZ = thePlayer.transform.position.z;

        data.playerHP = thePlayerStat.hp;
        data.playerMP = thePlayerStat.mp;
        data.playerCurrentHP = thePlayerStat.currentHp;
        data.playerCurrentMP = thePlayerStat.currenMp;
        data.playerCurrentEXP = thePlayerStat.currentEXP;
        data.playerATK = thePlayerStat.atk;
        data.playerDEF = thePlayerStat.def;
        data.added_atk = theEquip.addedAtk;
        data.added_def = theEquip.addedDef;
        data.added_hpr = theEquip.addedHp;
        data.added_mpr = theEquip.addMp;

        data.mapName = thePlayer.currentMapName;
        data.sceneName = thePlayer.currentSceneName;

        Debug.Log("기초 데이터 성공");

        data.playerItemInventory.Clear();
        data.playerItemInventoryCount.Clear();
        data.playerEquipItem.Clear();

        for (int i = 0; i < theDatabase.var_name.Length; i++)
        {
            data.varNameList.Add(theDatabase.var_name[i]);
            data.varNumberList.Add(theDatabase.var[i]);
        }
        for (int i = 0; i < theDatabase.switch_name.Length; i++)
        {
            data.swNameList.Add(theDatabase.switch_name[i]);
            data.swList.Add(theDatabase.switches[i]);
        }

        List<Item> itemList = theInven.SaveItem();

        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log("인벤토리의 아이템 저장 완료 : " + itemList[i].itemID);
            data.playerItemInventory.Add(itemList[i].itemID);
            data.playerItemInventoryCount.Add(itemList[i].itemCount);
        }

        for (int i = 0; i < theEquip.equipItemList.Length; i++)
        {
            data.playerEquipItem.Add(theEquip.equipItemList[i].itemID);
            Debug.Log("장착된 아이템 저장 완료 : " + theEquip.equipItemList[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();                             //파일 변환
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");  //파일 입출력

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");

    }

    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            theDatabase = FindObjectOfType<DatabaseManager>();
            thePlayer = FindObjectOfType<PlayerManager>();
            thePlayerStat = FindObjectOfType<PlayerStat>();
            theEquip = FindObjectOfType<Equipment>();
            theInven = FindObjectOfType<Inventory>();
            theFadeManager = FindObjectOfType<FadeManager>();

            theFadeManager.FadeOut();

            thePlayer.currentMapName = data.mapName;
            thePlayer.currentSceneName = data.sceneName;

            vector.Set(data.playerX, data.playerY, data.playerZ);
            thePlayer.transform.position = vector;

            thePlayerStat.hp = data.playerHP;
            thePlayerStat.mp = data.playerMP;
            thePlayerStat.currentHp = data.playerCurrentHP;
            thePlayerStat.currenMp = data.playerCurrentMP;
            thePlayerStat.currentEXP = data.playerCurrentEXP;
            thePlayerStat.atk = data.playerATK;
            thePlayerStat.def = data.playerDEF;

            theEquip.addedAtk = data.added_atk;
            theEquip.addedDef = data.added_def;
            theEquip.addedHp = data.added_hpr;
            theEquip.addMp = data.added_mpr;

            theDatabase.var = data.varNumberList.ToArray();
            theDatabase.var_name = data.varNameList.ToArray();
            theDatabase.switches = data.swList.ToArray();
            theDatabase.switch_name = data.swNameList.ToArray();

            for (int i = 0; i < theEquip.equipItemList.Length; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerEquipItem[i] == theDatabase.itemList[x].itemID)
                    {
                        theEquip.equipItemList[i] = theDatabase.itemList[x];
                        Debug.Log("장착된 아이템을 로드했습니다 : " + theEquip.equipItemList[i].itemID);
                        break;
                    }
                }
            }


            List<Item> itemList = new List<Item>();

            for (int i = 0; i < data.playerItemInventory.Count; i++)
            {
                for (int x = 0; x < theDatabase.itemList.Count; x++)
                {
                    if (data.playerItemInventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        Debug.Log("인벤토리 아이템을 로드했습니다 : " + theDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }

            for (int i = 0; i < data.playerItemInventoryCount.Count; i++)
            {
                itemList[i].itemCount = data.playerItemInventoryCount[i];
            }

            theInven.LoadItem(itemList);
            theEquip.ShowText();

            GameManager theGM = FindObjectOfType<GameManager>();
            theGM.LoadStart();

            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }


        file.Close();
    }
}
