using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
	public int drop;
	public int currentHealth;
	public int maxHealth = 70;
	private Rigidbody2D rb2d;
	private Animator anim;
	public GameObject[] Loot;
	private Player player;
	public PlayerAttack attack;
	// Use this for initialization
	void Start()
	{
		drop = Random.Range(1, 10);
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
	}

	public IEnumerator Knockback1(float knockDuration, float knockPower, Vector3 knockDirection)
	{
		float timer = 0;
		rb2d.velocity = new Vector2(0, 0);
		while (knockDuration > timer)
		{
			timer += Time.deltaTime;
			rb2d.AddForce(new Vector3(knockDirection.x * 10, knockDirection.y * knockPower, transform.position.z));
		}

		yield return 0;

	}
	public IEnumerator Knockback2(float knockDuration, float knockPower, Vector3 knockDirection)
	{
		float timer = 0;
		rb2d.velocity = new Vector2(0, 0);
		while (knockDuration > timer)
		{
			timer += Time.deltaTime;
			rb2d.AddForce(new Vector3(knockDirection.x * -10, knockDirection.y * knockPower, transform.position.z));
		}

		yield return 0;
	}
	void Update()
	{
		if (currentHealth <= 0) { //this if statement activates the loot random drop
			if (drop == 10) {
				Instantiate (Loot [0], transform.position, transform.rotation);
			} else if (drop == 7) {
				Instantiate (Loot [1], transform.position, transform.rotation);
			} else if (drop == 4) {
				Instantiate (Loot [2], transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;
		rb2d.velocity = easeVelocity;
	}
	public void Damage(int damage)
	{
		currentHealth -= damage;
		if (player.transform.localScale.x == 1)
		{
			StartCoroutine(Knockback1(0.02f, 3, transform.position));
		}
		if (player.transform.localScale.x == -1)
		{
			StartCoroutine(Knockback2(0.02f, 3, transform.position));
		}
		if (player.transform.localScale.x == 1 && attack.heavy)
		{
			StartCoroutine(Knockback1(0.03f, 5, transform.position));
		}
		if (player.transform.localScale.x == -1 && attack.heavy)
		{
			StartCoroutine(Knockback2(0.03f, 5, transform.position));
		}

	}
}