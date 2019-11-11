using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public GameObject floatingText;
    public GameObject parent;

    private PlayerStat thePlayerStat;
    private SpriteRenderer sprite;

    private bool attackCheck;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
        sprite = GetComponent<SpriteRenderer>();
        attackCheck = false;
    }

    private void Update()
    {
        if(attackCheck)
        {
            attackCheck = false;
            sprite.flipX = FindObjectOfType<PlayerManager>().GetComponent<SpriteRenderer>().flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="enemy")
        {
            attackCheck = true;
            int dmg = collision.gameObject.GetComponent<EnemyStat>().Hit(thePlayerStat.atk);
            GameObject clone = Instantiate(floatingText, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = dmg.ToString();
            clone.GetComponent<FloatingText>().text.color = Color.white;
            clone.transform.SetParent(parent.transform);
            clone.transform.localScale = new Vector3(2, 2, 1);
        }
    }
}
