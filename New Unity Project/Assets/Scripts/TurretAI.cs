using System.Collections;
using UnityEngine;

public class TurretAI : MonoBehaviour {

	//Ints
	public int currentHealth;
	public int maxHealth;

	//Floats
	public float distance;
	public float riseRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	//Booleans
	public bool awake = false;
	public bool lookingRight = true;

	//References
	public Transform Target;
	public GameObject Bullet;
	private Animator anim;
	public Transform shootPointLeft;
	public Transform shootPointRight;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start ()
	{
		currentHealth = maxHealth;
		anim = gameObject.GetComponent<Animator> ();
	}		

	void Update()
	{
		anim.SetBool ("awake", awake);
		anim.SetBool("lookingRight", lookingRight);
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}

		RangeCheck();

		if (Target.transform.position.x > transform.position.x) 
		{
			lookingRight = true;
		}
		else if(Target.transform.position.x < transform.position.x) 
		{
			lookingRight = false;
		}

	}

	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, Target.transform.position);

		if (distance < riseRange) 
		{
			awake = true;
		}

		if (distance > riseRange) 
		{
			awake = false;
		}
	}

	public void Attack(bool attackingRight)
	{
		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shootInterval) 
		{
			Vector2 direction = Target.transform.position - transform.position;
			direction.Normalize ();

			if (!attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (Bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			}
			if (attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (Bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
				bulletTimer = 0;

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