using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBasic : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;
	private int counter;
	public float distance;
	public float shootInterval;
    public float shotSpeed;
	public float shotTimer;
	public float waitTimer;
    public bool fire;

    private Player player;
    private PlayerAttack attack;
    public Transform Target;
	private Rigidbody2D rb2d;
	private Animator anim;
	public GameObject Shot;
	public Transform shootPoint;


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
		anim.SetFloat ("Distance", distance);
		distance = Vector3.Distance (transform.position, Target.transform.position);
		anim.SetBool ("Fire", fire);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (currentHealth <= 0) {
			Destroy (gameObject);
		}

		if (Target.transform.position.x < transform.position.x && distance < 20) 
		{
			transform.localScale = new Vector3 (1, 1, 1);
		}
		else if (Target.transform.position.x > transform.position.x && distance < 20) 
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
		if(waitTimer <=5)
        shotTimer += Time.deltaTime;

		if (shotTimer >= shootInterval && counter <= 10) {
			Vector2 direction = Target.transform.position - transform.position;
			direction.Normalize ();
			fire = true;

                GameObject shotClone;
                shotClone = Instantiate(Shot, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                shotClone.GetComponent<Rigidbody2D>().velocity = direction * 25;
                shotTimer = 0;
			counter++;
        		}
		if (counter >= 10){
			waitTimer += Time.deltaTime;
			}
		if (waitTimer >= 2)
		{
			counter = 0;
			waitTimer = 0;
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