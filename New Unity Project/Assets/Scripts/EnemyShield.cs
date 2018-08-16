using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour {

	private int drop;
	public int currentHealth;
	public int maxHealth = 120;
	public float shootInterval;
	public float grenadeSpeed;
	public float grenadeTimer;
	public bool destroyed;
	public bool toss;
	public bool block;

	private Player player;
	private PlayerAttack attack;
	public Transform Target;
	private Rigidbody2D rb2d;
	private Animator anim;
	public GameObject grenade;
	public GameObject Loot;
	public Transform shootPoint;

	// Use this for initialization
	void Start () 
	{
		drop = Random.Range(1, 10);
		anim = gameObject.GetComponent<Animator>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		destroyed = true;
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
	}

	// Update is called once per frame
	void Update () {

		anim.SetBool ("Toss", toss);
		anim.SetBool ("Destroyed", destroyed);
		anim.SetBool ("Block", block);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if (currentHealth <= 0)
		{
			toss = false;
			Attack (false);
			Die();
		}

		if (Target.transform.position.x < transform.position.x) 
		{
			transform.localScale = new Vector3 (1, 1, 1);
		}
		else if (Target.transform.position.x > transform.position.x) 
		{
			transform.localScale = new Vector3 (-1, 1, 1);
		}

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
	public void Attack(bool inRange)
	{
		toss = true;
		grenadeTimer += Time.deltaTime;
		if (destroyed && transform.localScale.x == 1 && grenadeTimer >= 0.6f) {
			Vector2 direction = new Vector2 (-0.5f, 0.5f);
			GameObject grenadeClone;
			grenadeClone = Instantiate (grenade, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			grenadeClone.GetComponent<Rigidbody2D> ().velocity = direction * grenadeSpeed;
			toss = false;
			destroyed = false;

		}
		if (destroyed && transform.localScale.x == -1 && grenadeTimer >= 0.6f) {
			Vector2 direction = new Vector2 (0.5f, 0.5f);
		
			GameObject grenadeClone;
			grenadeClone = Instantiate (grenade, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			grenadeClone.GetComponent<Rigidbody2D> ().velocity = direction * grenadeSpeed;
			toss = false;
			destroyed = false;

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
		if (!attack.heavy) {
			block = true;
			currentHealth -= 0;
		}
		if (block && attack.heavy){
				block = false;
		}
			if (!block) {
				anim.SetTrigger ("Hurt");
				currentHealth -= damage;
				if (player.transform.localScale.x == 1) {
					StartCoroutine (Knockback1 (0.02f, 3, transform.position));
				}
				if (player.transform.localScale.x == -1) {
					StartCoroutine (Knockback2 (0.02f, 3, transform.position));
				}
				if (player.transform.localScale.x == 1 && attack.heavy) {
					StartCoroutine (Knockback1 (0.03f, 5, transform.position));
				}
				if (player.transform.localScale.x == -1 && attack.heavy) {
					StartCoroutine (Knockback2 (0.03f, 5, transform.position));
				}
			}
		}

	public void Damage2(int damage)
	{

		anim.SetTrigger("Hurt");
		currentHealth -= damage;
	}

	IEnumerator Dead()
	{
		anim.SetTrigger ("Dead");

		yield return new WaitForSeconds (2);
		if (drop == 7){
			Instantiate (Loot, shootPoint.transform.position, shootPoint.transform.rotation);
	}
		Destroy(gameObject);
	}
	void Die()
	{
		
		StartCoroutine("Dead");
	}
}