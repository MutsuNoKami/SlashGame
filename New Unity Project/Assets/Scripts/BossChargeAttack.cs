using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAttack : MonoBehaviour {


	public int dmg = 25;
	private Player player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true)
		{
			if (col.CompareTag("Player"))
			{
				col.SendMessageUpwards("Damage", dmg);
				if(player.transform.localScale.x == -1)
				{ 
					StartCoroutine(player.Knockback2(1.2f, 3, player.transform.position));
				
				}
			}
		}
	}
}