using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumExplosion : MonoBehaviour {

	public float delay = 1;
	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && col.CompareTag ("Player")) {
				col.GetComponent<Player> ().Damage (15);
				StartCoroutine (player.Knockback (0.02f, 2, player.transform.position));

			}
		}
	}

