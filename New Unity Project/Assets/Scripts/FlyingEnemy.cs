using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {
    
    //Ints
    public int currentHealth;
	public int maxHealth;

    //Floats
    public float distance;
    public float shootInterval;
    public float bombSpeed = 1;
    public float bombTimer;
    public float speed = 30;
	
    //Booleans
	private bool abovePlayer;

    //References
    public Transform Target;
    public GameObject Bomb;
	private Animator anim;
    public Transform shootPoint;
	private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", distance);
		anim.SetBool("Above", abovePlayer);
		distance = Vector3.Distance(transform.position, Target.transform.position);
		abovePlayer = false;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
		if (Target.transform.position.x < transform.position.x && distance < 12)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb2d.AddForce(Vector2.left * speed);
			abovePlayer = false;
        }
		else if (Target.transform.position.x > transform.position.x && distance < 12)
        {
            transform.localScale = new Vector3(-1, 1, 1);
			rb2d.AddForce(Vector2.right * speed);
			abovePlayer = false;
        }
		if (Target.transform.position.x - transform.position.x <= 1 && Target.transform.position.x - transform.position.x >= -1) {
			abovePlayer = true;
			transform.localScale = new Vector3(1, 1, 1);
		}

	}


    public void Attack(bool inRange)
	{
		bombTimer += Time.deltaTime;
		if (bombTimer >= shootInterval) {
			Vector2 direction = Target.transform.position - transform.position;

			direction.Normalize ();

			if (inRange) {
				GameObject bombClone;
				bombClone = Instantiate (Bomb, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
				bombClone.GetComponent<Rigidbody2D> ().velocity = direction * bombSpeed;
				bombTimer = 0;

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

	public void Damage(int damage)
	{
		currentHealth -= damage;
	}
}