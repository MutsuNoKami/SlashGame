using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;

	public float speed;
	public float distance;
	public float shootInterval;
	public float shellSpeed;
	public float shellTimer;
	public bool fire;
	public bool block;

	private Player player;
	private PlayerAttack attack;
	public Transform Target;
	private Rigidbody2D rb2d;
	private Animator anim;
	public GameObject Shell;
	public Transform[] shootPoint;


	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		fire = false;
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
	}

	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Speed", speed);
		distance = Vector3.Distance (transform.position, Target.transform.position);
		anim.SetBool ("Fire", fire);
		anim.SetBool ("Block", block);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		if (currentHealth <= 0) {
			Destroy (gameObject);
		}


		if (Target.transform.position.x < transform.position.x && distance < 12) {
			transform.localScale = new Vector3 (1, 1, 1);
			rb2d.AddForce (Vector2.left * speed);
			block = false;
			}
		 else if (Target.transform.position.x > transform.position.x && distance < 12) {
			transform.localScale = new Vector3 (-1, 1, 1);
			block = false;
			rb2d.AddForce (Vector2.right * speed);
			}
		}

	public IEnumerator Knockback1(float knockDuration, float knockPower, Vector3 knockDirection)
	{
		float timer = 0;
		rb2d.velocity = new Vector2(0, 0);
		while (knockDuration > timer)
		{
			timer += Time.deltaTime;
			rb2d.AddForce(new Vector3(knockDirection.x * 1, knockDirection.y * knockPower, transform.position.z));
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
			rb2d.AddForce(new Vector3(knockDirection.x * -1, knockDirection.y * knockPower, transform.position.z));
		}

		yield return 0;

	}



	public void Attack(bool inRange)
	{
		speed = 0;
		fire = true;
		block = false;
		shellTimer += Time.deltaTime;

		if (shellTimer >= shootInterval) {
			Vector2 direction = Target.transform.position - transform.position;
			direction.Normalize ();
			GameObject shellClone;
			shellClone = Instantiate(Shell, shootPoint[0].transform.position, shootPoint[0].transform.rotation) as GameObject;
			shellClone.GetComponent<Rigidbody2D>().velocity = direction * shellSpeed;
			GameObject shellClone2;
			shellClone2 = Instantiate(Shell, shootPoint[1].transform.position, shootPoint[1].transform.rotation) as GameObject;
			shellClone2.GetComponent<Rigidbody2D>().velocity = direction * shellSpeed;
			shellTimer = 0;
		}
	}	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;
		rb2d.velocity = easeVelocity;
	}
	public void Damage(int damage)
	{
		shellTimer = 0;
		fire = false;
		anim.SetTrigger("Hurt");
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
	public void Damage2(int damage)
	{
		fire = false;
		block = true;
	}

	IEnumerator Dead()
	{
		yield return new WaitForSeconds(10);
		Destroy(gameObject);

	}
	void Die()
	{
		anim.SetTrigger("Dead");
		StartCoroutine("Dead");
	}
}