using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int character_LV;
    public int[] needExp;
    public int currentEXP;

    public int hp;
    public int currentHp;
    public int mp;
    public int currenMp;

    public int atk;
    public int def;

    public GameObject floating_Text;
    public GameObject parent;

    public Slider hpBar;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    private void Update()
    {
        hpBar.maxValue = hp;
        hpBar.value = currentHp;
    }

    public void Hit(int damaged)
    {
        int dmg;
        if(def >= damaged)
        {
            dmg = 1;
        }
        else
        {
            dmg = damaged - def;
        }

        currentHp -= dmg;

        if(currentHp<=0)
        {
            Debug.Log("게임오버");
        }

        Vector3 vector = this.transform.position;
        vector.y += 1.2f;

        GameObject clone = Instantiate(floating_Text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.transform.SetParent(parent.transform);
        clone.transform.localScale = new Vector3(2, 2, 1);

        StopAllCoroutines();
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.3f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.3f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.3f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.3f);
    }
}
