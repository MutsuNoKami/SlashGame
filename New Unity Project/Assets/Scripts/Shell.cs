using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

		public int dmg = 10;
		private Player player;

		// Use this for initialization
		void Start()
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
		void OnTriggerEnter2D(Collider2D col)
		{
		if (col.isTrigger != true || col.CompareTag("Boundary"))
			{
				if (col.CompareTag("Player"))
				{
					col.SendMessageUpwards("Damage", dmg);
					if(player.transform.localScale.x == -1)
					{ 
					StartCoroutine(player.Knockback(0.01f, 0.01f, player.transform.position));
					}
					if (player.transform.localScale.x == 1)
					{
					StartCoroutine(player.Knockback2(0.01f, 0.01f, player.transform.position));
					}
				}
			Destroy (gameObject);
			}
		}
	}