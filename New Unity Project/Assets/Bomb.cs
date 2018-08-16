using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private Player player;
    public GameObject Explosion;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.isTrigger != true && !col.CompareTag("Enemy"))
        {
            GameObject ExplosionClone;
            ExplosionClone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Player>().Damage(15);
                StartCoroutine(player.Knockback(0.02f, 2, player.transform.position));

            }
            

        }

    }
}