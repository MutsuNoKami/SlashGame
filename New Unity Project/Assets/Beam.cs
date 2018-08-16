using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

	public int dmg = 1;
	private Player player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.isTrigger != true)
		{
			if (col.CompareTag("Player"))
			{
				col.SendMessageUpwards("Damage", dmg);
				StartCoroutine(player.Knockback2(0.01f, 2, player.transform.position));

			}
		}
	}
}