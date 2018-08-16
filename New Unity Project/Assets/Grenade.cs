using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	private Player player;
	public GameObject Explosion;
	public EnemyShield enemy;
	public float grenadeTimer;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		enemy = FindObjectOfType<EnemyShield> ();
	}
	void Update ()
	{
		grenadeTimer += Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (grenadeTimer >= 4) {
				GameObject ExplosionClone;
				ExplosionClone = Instantiate (Explosion, transform.position, transform.rotation) as GameObject;
				Destroy (gameObject);

				enemy.destroyed = true;


		}
	}
}
