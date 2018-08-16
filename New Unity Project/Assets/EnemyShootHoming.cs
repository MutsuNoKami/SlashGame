using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootHoming : MonoBehaviour {


	public int currentHealth;
	public int maxHealth = 120;


	public float shootInterval;
	public float missileSpeed;
	public float missileTimer;
	public bool destroyed;
	public bool fire;


	private Player player;
	private PlayerAttack attack;
	public Transform Target;
	private Rigidbody2D rb2d;
	private Animator anim;
	public GameObject missile;
	public Transform shootPoint;


	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		destroyed = true;
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
	}

	// Update is called once per frame
	void Update () {

		anim.SetBool ("Fire", fire);
		anim.SetBool ("Destroyed", destroyed);
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		if (currentHealth <= 0)
		{

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
		if(destroyed && transform.localScale.x == 1)	
		{
			Vector2 direction = new Vector2 (-0.35f, 0.5f);
			fire = true;
			Wait ();
			destroyed = false;
			GameObject missileClone;
			missileClone = Instantiate(missile, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			missileClone.GetComponent<Rigidbody2D>().velocity = direction * missileSpeed;
			fire = false;
		}
		if(destroyed && transform.localScale.x == -1)	
		{
			Vector2 direction = new Vector2 (0.35f, 0.5f);
			fire = true;
			Wait ();
			destroyed = false;
			GameObject missileClone;
			missileClone = Instantiate(missile, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			missileClone.GetComponent<Rigidbody2D>().velocity = direction * missileSpeed;
			fire = false;
		}
	}
	public IEnumerator Wait()
	{
		
		yield return new WaitForSeconds(0.6f);

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
		
		anim.SetTrigger("Hurt");
		currentHealth -= damage;
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