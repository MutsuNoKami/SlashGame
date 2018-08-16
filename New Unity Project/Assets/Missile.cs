using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	public Transform target;
	private EnemyShootHoming enemy;
	public float speed;
	private Rigidbody2D rb2d;
	public bool hit = false;
	public bool v =false;
	public float missileTimer;
	public float rotatingSpeed = 200f;
	public GameObject Explosion;
	private Player player;

	// Use this for initialization
	void Start () {

		enemy = FindObjectOfType<EnemyShootHoming> ();
		target = enemy.Target;
		speed = enemy.missileSpeed;
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {
		if (enemy.transform.localScale.x == 1 || v) 
		{
			transform.localScale = new Vector3 (1, 1, 1);
		}
		else if (enemy.transform.localScale.x == -1 && !v) 
		{
			transform.localScale = new Vector3 (-1, 1, 1);
		}
		missileTimer += Time.deltaTime;
		if (missileTimer >= 2) {
			v = true;
		}
		if (hit) {
			target = enemy.transform;
		}
	}
	void FixedUpdate () { //controls rotation of missile towards player
		if (v) {
			Vector2 direction = transform.position - target.transform.position;
			direction.Normalize ();
			float value = Vector3.Cross (direction, transform.right).z;
			if (value < 0)
				rb2d.angularVelocity = rotatingSpeed;
			else if (value > 0)
				rb2d.angularVelocity = -rotatingSpeed;
			else
				rotatingSpeed = 0;
			rb2d.velocity = transform.right * -speed;
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.isTrigger && col.CompareTag ("Trigger")) {
			target = enemy.transform;
		} 
		else if (!col.isTrigger && col.CompareTag ("Player")) {
			col.GetComponent<Player> ().Damage (15);
			StartCoroutine (player.Knockback (0.02f, 2, player.transform.position));
			enemy.destroyed = true;//calls the destroyed boolean
			GameObject ExplosionClone;
			ExplosionClone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
			Destroy(gameObject);
		}
		else if (!col.isTrigger && col.CompareTag("Enemy"))
		{
			col.SendMessageUpwards("Damage", 50);
			enemy.destroyed = true;
			GameObject ExplosionClone;
			ExplosionClone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
			Destroy(gameObject);
		}

	}
}
