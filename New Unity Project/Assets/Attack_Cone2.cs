using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone2 : MonoBehaviour {

    public FlyingEnemy enemyAI;

    // Use this for initialization
    void Start()
    {
        enemyAI = gameObject.GetComponentInParent<FlyingEnemy>();
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
                enemyAI.Attack(true);
            }
        }
    }
