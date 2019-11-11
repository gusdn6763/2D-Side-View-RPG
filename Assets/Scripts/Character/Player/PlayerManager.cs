using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;

    public string currentMapName;
    public string currentSceneName;

    public float jumpPower;

    private bool attacking = false;
    public float attackDelay;
    private float currentAttackDelay;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        trans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        speed = 10f;
        jumpPower = 1f;
    }

    void Update()
    {


        if (!notMove && !attacking)
        {
            StartCoroutine(moving());
        }

        if(!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                currentAttackDelay = attackDelay;
                animator.SetTrigger("Attack");
                attacking = true;
            }
        }
        if(attacking)
        {
            currentAttackDelay -= Time.deltaTime;
            if(currentAttackDelay <=0)
            {
                attacking = false;
            }
        }
    }

    private IEnumerator moving()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !attacking)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), 0, 0);

            animator.SetBool("Walking", true);

            CheckCollsion();

            Filp();

            trans.Translate(vector.x * speed * Time.deltaTime, 0, 0);

            yield return new WaitForSeconds(0.02f);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }


}

