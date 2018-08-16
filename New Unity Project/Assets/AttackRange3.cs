using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange3 : MonoBehaviour {

	public EnemyShield enemyAI;

	// Use this for initialization
	void Start()
	{
		enemyAI = gameObject.GetComponentInParent<EnemyShield>();
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Player") && !enemyAI.destroyed)
			{
			enemyAI.toss = false;	

			}

		if (col.CompareTag("Player") && enemyAI.destroyed)
		{
			
			enemyAI.Attack(true);


		}
		if (!col.CompareTag("Player"))
		{
			enemyAI.toss = false;
			enemyAI.grenadeTimer = 0;

		}
	}
}

