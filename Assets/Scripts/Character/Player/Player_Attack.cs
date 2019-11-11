using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public static Player_Attack instance;

    private Animator animator;

    public Transform trans;

    public Vector2 boxsize;

    public float curTime;

    [Header("공격딜레이")] public float coolTime;

    public GameObject Effect;

    public int damage;

    void Start()
    {
        animator = GetComponent<Animator>();

    }


    void Update()
    {
      
        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(trans.position, boxsize, 0);
                foreach (Collider2D col in collider2Ds)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        var save = Instantiate(Effect, col.gameObject.transform.position, Quaternion.identity);
                        Destroy(save, 0.3f);
                    }
                }
                curTime = coolTime;
                animator.SetBool("attack", true);
            }

        }
        else
        {
            animator.SetBool("attack", false);
            curTime -= Time.deltaTime;
        }





    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(trans.position, boxsize);
    }

}
