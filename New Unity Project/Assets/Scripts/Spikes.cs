using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Player player;
    private Animator anim;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = gameObject.GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
        if (col.CompareTag("Player"))
        {
            anim.SetTrigger("Touch");
            player.Damage(10);
            if (player.transform.localScale.x == 1)

                StartCoroutine(player.Knockback2(0.01f, 2, player.transform.position));
            else
                StartCoroutine(player.Knockback(0.01f, 2, player.transform.position));
        }
	}
}
