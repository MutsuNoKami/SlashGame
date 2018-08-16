﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange2 : MonoBehaviour {

	public EnemyShootHoming enemyAI;

	// Use this for initialization
	void Start()
	{
		enemyAI = gameObject.GetComponentInParent<EnemyShootHoming>();
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{

			enemyAI.Attack(true);
			enemyAI.fire = true;
		}
		if (!col.CompareTag("Player"))
		{
			
		}
	}
}

