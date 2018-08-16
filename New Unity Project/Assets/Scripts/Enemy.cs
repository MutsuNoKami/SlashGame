using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public float distance;
    public float speed = 10;
    public float maxSpeed = 1;
    public bool attacking;
    public Transform Target;
    private Player player;
    private Rigidbody2D rb2d;
    private Animator anim;
	public PlayerAttack attack;
    public float duration = 0.02f;
    public float power = 3;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		attack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack> ();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Distance", distance);
        distance = Vector3.Distance(transform.position, Target.transform.position);

        if (currentHealth <= 0)
        {

            Die();

        }

        if (Target.transform.position.x < transform.position.x && distance < 8)
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb2d.AddForce(Vector2.left * speed);
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }
        }
        else if (Target.transform.position.x > transform.position.x && distance < 8)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb2d.AddForce(Vector2.right * speed);
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
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
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
    void Die()
    {
        anim.SetTrigger("Dead");
		StartCoroutine ("Dead");
    }




}
