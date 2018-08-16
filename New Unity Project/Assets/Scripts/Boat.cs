using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

	public float bulletSpeed;
	public float speed = 150f;
	public float maxSpeed = 75f;
	public float bulletTimer;
	public float shootInterval;
	public bool dead = false;
	public GameObject Bullet;
	public int currentHealth;
	public int maxHealth = 100;
	public float shoottimer;
	public bool checkpointReached;
	public bool canMove;
	public LevelManager levelManager;
	public bool crouch = false;
	public bool block = false;
	private Rigidbody2D rb2d;
	private Animator anim;
	private gameMaster gm;
	public Transform shootPoint;

	// Use this for initialization
	void Start()
	{

		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		currentHealth = maxHealth;
		levelManager = FindObjectOfType<LevelManager>();

	}

	// Update is called once per frame
	void Update()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();



		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		} else { 
			crouch = false;
		}

		if (currentHealth <= 0) {

			Die ();

		}
		if (Input.GetAxis ("Fire") < 0) {
			
			bulletTimer += Time.deltaTime;
			if (bulletTimer >= shootInterval) {
				{
					
					GameObject bulletClone;
					bulletClone = Instantiate (Bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
					bulletClone.GetComponent<Rigidbody2D> ().velocity = Vector2.right * bulletSpeed;
					bulletTimer = 0;




				}
			}
		}
	}
	


	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;


	}




	void Die()
	{

		dead = true;
		canMove = false;

	}

	public void Damage(int dmg)
	{

		if (block)
			currentHealth -= (dmg / 2);
		if (!block)
			currentHealth -= dmg;
		anim.SetTrigger("Hurt");
	}
	void OnTriggerEnter2D(Collider2D col)
	{ 
		if (col.CompareTag ("Coin")) {
			Destroy (col.gameObject);
			gameMaster.points += 10;
		}
		if (col.CompareTag ("Health")) {
			Destroy (col.gameObject);
			currentHealth += 15;
		}

	}
}





