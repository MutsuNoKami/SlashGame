using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour {

	public int currentHealth;
	public int maxHealth;
	public float distance;
	public float speed = 1;
	private Player player;
	public Transform Target;
	public GameObject Explosion;
	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Distance", distance);
		distance = Vector3.Distance (transform.position, Target.transform.position);

		if (currentHealth <= 0) {
			Destroy (gameObject);
		}

		if (Target.transform.position.x < transform.position.x && distance < 35) {
			transform.localScale = new Vector3 (1, 1, 1);
			rb2d.AddForce (Vector2.left * speed);
		}
		if (Target.transform.position.y >= (transform.position.y - 1) && distance < 35) {
			rb2d.AddForce (Vector2.up * speed * 3);
		}
		if (Target.transform.position.x > transform.position.x && distance < 35) {
			transform.localScale = new Vector3 (-1, 1, 1);
			rb2d.AddForce (Vector2.right * speed);
		}
		if (Target.transform.position.y <= transform.position.y && distance < 35) {
			rb2d.AddForce (Vector2.down * speed);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.CompareTag("Player"))
		{
			GameObject ExplosionClone;
			ExplosionClone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
			Destroy(gameObject);
			col.GetComponent<Player>().Damage(45);
		
			if (Target.transform.position.x > transform.position.x) {
				StartCoroutine(player.Knockback(0.02f, 0.2f, player.transform.position));
			}
			if (Target.transform.position.x < transform.position.x) {
				StartCoroutine(player.Knockback2(0.02f, 0.2f, player.transform.position));
			}
		}
	}
	public void Damage(int damage)
	{
		currentHealth -= damage;
	}
	public void Damage2(int damage)
	{
		currentHealth -= damage;
	}
}


