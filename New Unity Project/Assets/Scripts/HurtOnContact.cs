using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnContact : MonoBehaviour {


    private Player player;
    private Player health;
    public int dmg;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(dmg);
			if (player.transform.localScale.x == 1) {
				StartCoroutine (player.Knockback2 (0.02f, 1, player.transform.position));
			}
			if (player.transform.localScale.x == -1) {
				StartCoroutine (player.Knockback (0.02f, 1, player.transform.position));
			}
        }
    }
}
