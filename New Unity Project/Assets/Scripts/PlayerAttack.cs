using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Collider2D[] attackTrigger;


    private bool attacking = false;
	private int combo;
	private bool chain0 = false;
	private bool chain1 = false;
    private bool chain2 = false;
	private bool chain3 = false;
	private bool chain4 = false;
    public bool heavy = false;
    private float attackTimer = 0;
    private float attackCd = 0.5f;
    private Player player;
    private Rigidbody2D rb2d;
    public static bool[] unlocked;

    private Animator anim;

    
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        player = gameObject.GetComponent<Player>();
        for (int i = 0; i < attackTrigger.Length; i++)
        {
            attackTrigger[i].enabled = false;
        }

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Light Attack") && !attacking) {
            attacking = true;
            attackTimer = attackCd;
            chain0 = true;
            combo++;

            attackTrigger[0].enabled = true;

        }
        else if (Input.GetButtonDown("Light Attack") && attacking && combo == 1 && chain0 && player.grounded) {
            attacking = true;
            chain0 = false;
            chain1 = true;
            attackTimer = 1.5f;
            attackTrigger[0].enabled = false;
            attackTrigger[1].enabled = true;
            combo++;
        }
        else if (Input.GetButtonDown("Light Attack") && attacking && combo == 2 && chain1)
        {
            attacking = true;
            chain1 = false;
            chain2 = true;
            attackTimer = 2;
            attackTrigger[1].enabled = false;
            attackTrigger[2].enabled = true;
            combo++;
        }

        if (Input.GetButtonDown("Heavy Attack") && !attacking && combo == 0) {
            attacking = true;
            heavy = true;
            attackTimer = 0.6f;
            chain3 = true;
            combo++;

            attackTrigger[3].enabled = true;

        }
        else if (Input.GetButtonDown("Heavy Attack") && attacking && combo == 1 && chain3 && player.grounded) {
            attacking = true;
            heavy = true;
            chain3 = false;
            chain4 = true;
            attackTimer = 1f;
            attackTrigger[3].enabled = false;
            attackTrigger[4].enabled = true;
            combo++;
        }
		if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attackTimer = 0;
                    attacking = false;
                    combo = 0;
                    chain0 = false;
                    chain1 = false;
                    chain2 = false;
                    chain3 = false;
                    chain4 = false;
                    heavy = false;
                    for (int i = 0; i < attackTrigger.Length; i++)
                    {
                        attackTrigger[i].enabled = false;
                    }
                }

            }
            anim.SetBool("Attacking", attacking);
            anim.SetBool("AttackChain0", chain0);
            anim.SetBool("AttackChain1", chain1);
            anim.SetBool("AttackChain2", chain2);
            anim.SetBool("AttackChain3", chain3);
            anim.SetBool("AttackChain4", chain4);
            anim.SetInteger("Press", combo);
        }
    }

