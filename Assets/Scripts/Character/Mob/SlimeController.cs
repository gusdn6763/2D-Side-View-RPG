using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MovingObject
{
    public float attackDelay; // 공격 유예.

    public float inter_MoveWaitTime; // 대기 시간.
    private float current_interMWT;

    public string atkSound;

    private Vector2 playerPos; // 플레이어의 좌표값.

    private int random_int;
    private string direction;

    // Use this for initialization
    void Start()
    {
        queue = new Queue<string>();
        current_interMWT = inter_MoveWaitTime;
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        current_interMWT -= Time.deltaTime;

        if (current_interMWT <= 0)
        {
            current_interMWT = inter_MoveWaitTime;

            if (NearPlayer())
            {
                Flip();
                return;
            }

            RandomDirection();

            base.Move(direction,1);
        }

    }

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        if (playerPos.x > this.transform.position.x)
            flip.x = -1f;
        else
            flip.x = 1f;
        this.transform.localScale = flip;
        animator.SetTrigger("Attack");
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        if (NearPlayer())
            PlayerStat.instance.Hit(1);
    }

    private bool NearPlayer()
    {
        playerPos = PlayerManager.instance.transform.position;

        if (Vector2.Distance(this.transform.position, playerPos) <=2 )
        {
            return true;
        }

        return false;
    }

    private void RandomDirection()
    {
        vector.Set(0, 0, vector.z);
        random_int = Random.Range(0, 2);
        switch (random_int)
        {
            case 0:
                vector.x = 1f;
                direction = "RIGHT";
                break;
            case 1:
                vector.x = -1f;
                direction = "LEFT";
                break;
        }
    }

}
