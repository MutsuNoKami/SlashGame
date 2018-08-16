using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoss : MonoBehaviour {

	//Ints
	public int currentHealth;
	public int maxHealth = 625;
	public int dmg = 10;
	private int def;
	private int launchCount;
	//Floats
	public float speed = 200;
	public float maxSpeed = 75;
	public float shootInterval;
	public float projectileSpeed = 100;
	public float shotTimer;
	public float blockTimer;
	public float attackTimer;
	public float distance;
	public float beamTimer;


	//Booleans

	public bool block = false;
	public bool shooting = false;
	public bool attack = false;
	public bool phase1 = false;
	public bool phase2 = false;
	private bool over;
	//References
	public Transform Target;
	public GameObject Projectile;
	public GameObject Barrier;
	public GameObject Cannon;
	private Animator anim;
	private Rigidbody2D rb2d;
	public Transform shootPoint;
	private Player player;
	public Collider2D attackTrigger;

	// Use this for initialization
	void Start()
	{
		def = Random.Range(1, 5);
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
		anim.SetBool ("Attack", attack);
		anim.SetBool ("Shooting", shooting);
		anim.SetInteger ("LaunchCount", launchCount);
		if (block) {
			blockTimer += Time.deltaTime;
		}
		if (!phase2 && currentHealth > (maxHealth * 0.6)) {
			
			phase1 = true;
			if (distance < 10) {
				block = true;

			}
			if (blockTimer >= 5 || over) {
				block = false;
				blockTimer = 0;
			}
			Strike ();
		}

		if (currentHealth < (maxHealth * 0.6) && currentHealth > (maxHealth * 0.2)) {
			phase2 = true;
			phase1 = false;
			StopCoroutine ("Strike");
			Assault ();
		}
		if (currentHealth < (maxHealth * 0.2)) {
			phase2 = true;
			phase1 = false;
			StopCoroutine ("Assault");
			TheEnd ();
		}

		if (currentHealth <= 0) {
			Application.LoadLevel (11);

		}
		if (Target.transform.position.x < transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);

		}
		if (Target.transform.position.x > transform.position.x) {
			transform.localScale = new Vector3 (-1, 1, 1);

		}
		if (block) {
			Barrier.SetActive (true);
		} else {
			Barrier.SetActive (false);
		}
		if (attack){
			speed = 200;
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
	IEnumerator Wait3()
	{
		yield return new WaitForSeconds(3);
		beamTimer = 0;
		TheEnd ();
	}
	IEnumerator Wait2()
	{
		attack = false;
		speed = 0;
		yield return new WaitForSeconds(6);
		attackTimer = 0;
		StopCoroutine ("Wait2");
		Assault ();
	}
	IEnumerator Wait()
	{
		shooting = false;
		over = true;
		yield return new WaitForSeconds (9);
		over = false;
		launchCount = 0;
		Strike ();
	}
	void Strike()
	{
		shooting = true;
		if (launchCount < 10) { 
			shotTimer += Time.deltaTime;
			if (shotTimer >= shootInterval) {
				shooting = true;
				Vector2 direction = Target.transform.position - transform.position;
				direction.Normalize ();

				GameObject projectileClone;
				projectileClone = Instantiate (Projectile, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
				projectileClone.GetComponent<Rigidbody2D> ().velocity = direction * projectileSpeed;
				shotTimer = 0;
				launchCount += 1;

				if (launchCount >= 10) {
					StartCoroutine ("Wait");
				}

			}

		}
	}
	void Assault()
	{
		
		attack = true;
		attackTimer += Time.deltaTime;

		if (launchCount <= 10) {
			shooting = false;

			if (Target.transform.position.x < transform.position.x) {
				transform.localScale = new Vector3 (1, 1, 1);
				rb2d.AddForce (Vector2.left * speed);
			}
			if (Target.transform.position.x > transform.position.x) {
				transform.localScale = new Vector3 (-1, 1, 1);
				rb2d.AddForce (Vector2.right * speed);

			}
			if (distance < 3)
			{
				attackTrigger.enabled = true;
			}
			if (attackTimer >= 0.8) {
				attack = false;
				StartCoroutine ("Wait2");
			}
		}
	}
	void TheEnd(){
	
		speed = 0;
		anim.SetTrigger ("Beam");
		beamTimer += Time.deltaTime;
	if (beamTimer >= 3) {
		Cannon.SetActive (true);
	}
	if (beamTimer >= 10) {
		Cannon.SetActive (false);
		StartCoroutine ("Wait3");
		}
	}
	public void Damage(int damage)
	{
			
			anim.SetTrigger ("Hurt");
			currentHealth -= damage / def;
		}

	public void Damage2 (int damage)
	{
		anim.SetTrigger ("Hurt");
		currentHealth -= damage;
	}
}


