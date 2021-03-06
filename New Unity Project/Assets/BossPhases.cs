using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPhases : MonoBehaviour
{

	//Ints
	public int currentHealth;
	public int maxHealth = 500;
	public int dmg = 10;
	private int def;
	private int launchCount;
	//Floats
	public float speed = 200;
	public float maxSpeed = 20;
	public float shootInterval;
	public float projectileSpeed = 100;
	public float shootTimer;
	public float jumpSpeed;
	public float distance;
	private float jumpTimer;
	private float missileTimer;
	public float attackTimer;
	public float attackCd = 1;

	//Booleans
	public bool grounded = false;
	public bool jumping = false;
	public bool shooting = false;
	public bool still = false;
	public bool phase1 = false;
	public bool phase2 = false;
	public bool saw = false;
	//References
	public Transform Target;
	public GameObject Projectile;
	private Animator anim;
	private Rigidbody2D rb2d;
	public Transform shootPoint;
	private Player player;
	public Collider2D attackTrigger;
	public GameObject Move;
	// Use this for initialization
	void Start()
	{
		def = Random.Range(5, 10);
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		currentHealth = maxHealth;
		attackTrigger.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		distance = Vector3.Distance (transform.position, Target.transform.position);
		anim.SetFloat ("Distance", distance);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		anim.SetBool ("Grounded", grounded);
		anim.SetBool ("Shooting", shooting);
		anim.SetInteger ("LaunchCount", launchCount);
		anim.SetBool ("Still", still);
		anim.SetBool ("Saw", saw);

		if (!phase2 ) {
			phase1 = true;
			Missiles ();
		}

		if (currentHealth <= (maxHealth * 0.6)) {
			phase2 = true;
			phase1 = false;
			Saw ();
		}

		if (currentHealth <= 0) {
			anim.SetTrigger ("Die");
			GameObject Transporter;
			Transporter = Instantiate(Move, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			Destroy (gameObject);

		}
		if (Target.transform.position.x < transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);

		}
		if (Target.transform.position.x > transform.position.x) {
			transform.localScale = new Vector3 (-1, 1, 1);

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

	IEnumerator Wait2()
	{

		yield return new WaitForSeconds(6); //boss has to prepare attack
		speed = 0;
		saw = true;
		attackTimer = attackCd;
		attackTimer -= Time.deltaTime;
		attackTrigger.enabled = true;
		// attack commences. Attack trigger is enabled.
		yield return new WaitForSeconds(0.5f);//time taken to finish attack
		attackTrigger.enabled = false;
		saw = false;//attack ends
		Saw();//return to original subroutine


	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);//amount of time the script stops for
		launchCount = 0;//counter s reset and returns to subroutine
		Missiles ();
	}
	void Missiles()
	{

		if (launchCount < 5)
		{ 
			missileTimer += Time.deltaTime;
			if (missileTimer >= shootInterval)
			{
				shooting = true;
				Vector2 direction = Target.transform.position - transform.position;
				direction.Normalize();

				GameObject projectileClone;
				projectileClone = Instantiate(Projectile, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
				projectileClone.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
				missileTimer = 0;
				launchCount += 1;
				if (launchCount >= 5)
				{
					StartCoroutine("Wait");
				}

			}
		}	
	}
	void Saw()
	{
		speed = 37.5f;    
		still = true;
		saw = false;
		if (launchCount <= 5) {
			shooting = false;
			if (Target.transform.position.x < transform.position.x && distance > 5 && !saw) {
				transform.localScale = new Vector3 (1, 1, 1);
				rb2d.AddForce (Vector2.left * speed);
			}
			if (Target.transform.position.x > transform.position.x && distance > 5 && !saw) {
				transform.localScale = new Vector3 (-1, 1, 1);
				rb2d.AddForce (Vector2.right * speed);
			}
			if (distance <= 5 ) {
				speed = 0;
				StartCoroutine("Wait2");

			} 
			else if (distance > 3 || attackTimer <= 0)
			{
				attackTrigger.enabled = false;
			}
		}
	}
	public void Damage(int damage)
	{
		gameObject.GetComponent<Animation> ().Play ("Boss_Damage");
		currentHealth -= damage / def;
	}
	public void Damage2(int damage)
	{

		gameObject.GetComponent<Animation> ().Play ("Boss_Damage");
		currentHealth -= damage;
	}
}


