using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4 : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;
	public float distance;
	public float speed = 35;
	public float maxSpeed = 10;
	public bool attacking;
	public Transform Target;
	private Player player;
	private Rigidbody2D rb2d;
	private Animator anim;
	public float reactTimer;
	// Use this for initialization
	void Start()
	{
		anim = gameObject.GetComponent<Animator>();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		
		anim.SetFloat ("Distance", distance);
		distance = Vector3.Distance (transform.position, Target.transform.position);

		if (currentHealth <= 0) {

			Die ();

		}

		if (Target.transform.position.x < transform.position.x && distance < 20) {
			
			transform.localScale = new Vector3 (1, 1, 1);
			rb2d.AddForce (Vector2.left * speed);
			if (rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
			}
		} else if (Target.transform.position.x > transform.position.x && distance < 20) {
			
			transform.localScale = new Vector3 (-1, 1, 1);
			rb2d.AddForce (Vector2.right * speed);
			if (rb2d.velocity.x > maxSpeed) {
				rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
			}
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
	public void Damage(int damage)
	{
		speed = 0;
		currentHealth -= damage;
		anim.SetTrigger("Hurt");
		reactTimer += Time.deltaTime;
		if(reactTimer >= 0.02f)
		{
			reactTimer = 0;
			speed = 35;

		}
	}
	public void Damage2(int damage)
	{
		speed = 0;
		anim.SetTrigger("Defend");
		currentHealth -= 0;
		reactTimer += Time.deltaTime;
			if(reactTimer >= 0.6f)
			{
			reactTimer = 0;
			speed = 35;

			}

	}
	IEnumerator Dead()
	{
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
	void Die()
	{
		anim.SetTrigger("Dead");
		StartCoroutine ("Dead");
	}




}
