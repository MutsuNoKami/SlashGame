using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange4 : MonoBehaviour {

	public Tank enemyAI;
	public float timer;
	// Use this for initialization
	void Start()
	{
		enemyAI = gameObject.GetComponentInParent<Tank>();
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag ("Player") && timer < 3) {
			timer += Time.deltaTime;
		}
		if (col.CompareTag("Player") && timer >= 3)
		{

			enemyAI.Attack(true);


		}
		if (!col.CompareTag("Player"))
		{
			enemyAI.fire = false;
			timer = 0;
		}

	}
}

