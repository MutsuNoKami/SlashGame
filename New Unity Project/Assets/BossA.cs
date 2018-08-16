using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossA : MonoBehaviour {


	//Ints
	public int currentHealth;
	public int maxHealth = 500;
	public int dmg = 10;
	public int strikeCount;
	private int def;

	//Floats
	public float speed = 50;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;
	public float distance;
	public float chargeTimer;
	public float photonTimer;
	//Booleans
	public bool down;
	public bool rise;
	public bool fire;
	public bool charge = false;
	public bool reached = false;
	public bool laser = false;
	//References
	public Transform Target;
	public Transform reach;
	public GameObject Bullet;
	public GameObject Energy;
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
		distance = Vector3.Distance (transform.position,Target.transform.position);
		anim.SetFloat ("Distance", distance);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		anim.SetBool ("Fire", fire);
		anim.SetBool ("Down", down);
		anim.SetBool ("Charge", charge);
		anim.SetFloat ("Timer", photonTimer);


		if (!down && currentHealth > (maxHealth * 0.67)) {
			Gun ();
		}
		if (currentHealth <= (maxHealth * 0.67) && currentHealth >= (maxHealth * 0.33) && !down){

			fire = false;
			StopCoroutine ("Gun");
			Charge ();
		}
		if (currentHealth < (maxHealth * 0.33) && !down) {
			fire = false;
			StopCoroutine ("Charge");
			StopCoroutine ("Gun");
			Beam ();
		}
		if (down) {
			StartCoroutine("Down");
		}

		if (currentHealth <= 0) {
			anim.SetTrigger ("Die");
			GameObject Transporter;
			Transporter = Instantiate(Move, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			Destroy (gameObject);
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
	IEnumerator Down()
	{
		speed = 0;
		yield return new WaitForSeconds(20);
		anim.SetTrigger("Rise");
		down = false;
		StopCoroutine ("Down");
	}
	IEnumerator Wait()
	{
		transform.localScale = new Vector3 (1, 1, 1);
		yield return new WaitForSeconds(1);


		charge = true;

		speed = 0;
		if (charge && !down)
			attackTrigger.enabled = true;{	
			speed = 3000;
			rb2d.AddForce (Vector2.left * speed);
			yield return new WaitForSeconds (1);
			strikeCount += 1;
			rb2d.AddForce (Vector2.left * -speed);
			chargeTimer += Time.deltaTime;
			if (strikeCount >= 400 || down) {
				speed = 0;
				charge = false;
			} else {
				StartCoroutine ("Wait2");
			}
		}
	}

	IEnumerator Wait2()
	{
		transform.localScale = new Vector3 (-1, 1, 1);
		yield return new WaitForSeconds(0.5f);

		charge = true;

		speed = 0;
		if (charge && !down)
			attackTrigger.enabled = true;{	
			speed = 3000;
			rb2d.AddForce (Vector2.right * speed);

			yield return new WaitForSeconds (1);
			strikeCount += 1;
			rb2d.AddForce (Vector2.right * -speed);
			chargeTimer += Time.deltaTime;
			if (strikeCount >= 400) {
				speed = 0;
				charge = false;
				StartCoroutine ("Charge");
			} else {
				StartCoroutine ("Wait");

			}
		}		
	}


	void Gun()
	{
		if (Target.transform.position.x < transform.position.x ) {
			transform.localScale = new Vector3 (1, 1, 1);

		}
		if (Target.transform.position.x > transform.position.x ) {
			transform.localScale = new Vector3 (-1, 1, 1);

		}

		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shootInterval) 
		{
			Vector2 direction = new Vector2(-1,-0.35f);
			direction.Normalize ();
			fire = true;
			GameObject bulletClone;
			bulletClone = Instantiate (Bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
			bulletTimer = 0;
		}
	}
	void Charge()
	{
		speed = 50;
		if (!charge) {
			
			if (reach.transform.position.x < transform.position.x) {
				transform.localScale = new Vector3 (1, 1, 1);
				rb2d.AddForce (Vector2.left * speed);
			}
			if (reach.transform.position.x > transform.position.x) {
				transform.localScale = new Vector3 (-1, 1, 1);
				rb2d.AddForce (Vector2.right * speed);
			}
			if (reach.transform.position.x - transform.position.x <= 6 && reach.transform.position.x - transform.position.x >= -6 && !charge) {
				reached = true;
				rb2d.AddForce (Vector2.zero);

				StartCoroutine ("Wait");
				}

			}

	}
	void Beam()
	{
		

		anim.SetTrigger ("Beam");

		photonTimer += Time.deltaTime;
		if (photonTimer >= 3.7f && photonTimer <= 14) {
			Energy.SetActive (true);
		}
		if (photonTimer >= 14 && photonTimer <= 18) {
			Energy.SetActive (false);
		}
		if (photonTimer > 18) {
			photonTimer = 0;
		}
	}
	public void Damage(int damage)
	{
		if (down) {
			speed = 0;
			Energy.SetActive (false);
			currentHealth -= damage / def;
			attackTrigger.enabled = false;
			photonTimer = 0;
		}
		else
		{
			currentHealth -= 0;
		}
	}
	public void Damage2(int damage)
	{
		if (down) {
			speed = 0;
			Energy.SetActive (false);
			currentHealth -= damage / def;
			attackTrigger.enabled = false;
			photonTimer = 0;
		}
		else
		{
			currentHealth -= 0;
		}
	}
}

