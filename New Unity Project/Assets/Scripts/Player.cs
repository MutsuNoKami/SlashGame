using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float maxSpeed;
    public float trueMaxSpeed = 10;
    public float speed = 150f;
    public float jumpSpeed = 450f;
   
    public bool grounded;
	public bool dead = false;
    public GameObject currentCheckpoint;
    public int currentHealth;
    public int maxHealth = 100;
    public bool checkpointReached;
    public bool canMove;
    public LevelManager levelManager;
	public bool crouch = false;
    public bool block = false;
    private PlayerAttack attack;
    private PlayerEquip equip;
    private Rigidbody2D rb2d;
    private Animator anim;
	private gameMaster gm;
	public Collider2D shieldBlock;

    // Use this for initialization
    void Start()
    {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
        currentHealth = maxHealth;
        levelManager = FindObjectOfType<LevelManager>();
        attack = gameObject.GetComponent<PlayerAttack>();
        equip = gameObject.GetComponent<PlayerEquip>();
        maxSpeed = trueMaxSpeed;

    }

    // Update is called once per frame
    void Update()
    {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		attack = gameObject.GetComponent<PlayerAttack>();
		equip = gameObject.GetComponent<PlayerEquip>();
        anim.SetBool("Grounded", grounded);
        anim.SetBool("CanMove", canMove);
        anim.SetBool("Crouch", crouch);
        anim.SetBool("Block", block);
		anim.SetBool("Dead", dead);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetFloat("JumpSpeed", Mathf.Abs(rb2d.velocity.y));


        if (!canMove)
        {
            attack.enabled = false;
            speed = 0;

            jumpSpeed = 0;
            equip.enabled = false;
            maxSpeed = 0;
            block = false;
            crouch = false;
        }
        else if (!block || !crouch)
        {
            speed = 150f;
            jumpSpeed = 450f;
            attack.enabled = true;
            equip.enabled = true;

        }
        if (crouch )
        {
            speed = 0;
            maxSpeed = 0;
        }
        if (block)
        {
            speed = 0;
            jumpSpeed = 0;
        }
       
        if (Input.GetAxis("Horizontal") < -0.1f && !crouch)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f && !crouch)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            if (crouch)
                rb2d.AddForce(Vector2.up * jumpSpeed * 1.5f);
            else
                rb2d.AddForce(Vector2.up * jumpSpeed);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (Input.GetButton("Crouch") && grounded && canMove)
        {
            crouch = true;

        }

        else
        { 
        crouch = false;
        }

        if (Input.GetButton("Block") && grounded && canMove)
        {
            block = true;
            attack.enabled = false;
            speed = 0;
            jumpSpeed = 0;
            
			equip.enabled = true;
            maxSpeed = 0;
			if (equip.shield) {
				shieldBlock.enabled = true;
			} 
					
        }
        else
        {
            block = false;
			shieldBlock.enabled = false;
        }
        if (currentHealth <= 0)
        {
			
			Die();

        }
    }


    void FixedUpdate()
    {

        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;


        float h = Input.GetAxis("Horizontal");
        if (!equip.gun)
        {
            maxSpeed = trueMaxSpeed;
        }
        //Fake friction
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        //Moving player


        rb2d.AddForce((Vector2.right * speed) * h);
        if (equip.gun)
        {

        }
        else
        {
            maxSpeed = trueMaxSpeed;
        }
        if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }
        

		}


    
    void Die()
    {
		
		dead = true;
		canMove = false;

    }

    public void Damage(int dmg)
    {
        
		if (block) {
			currentHealth -= (dmg / 2);
		}
		if (!block) {
			currentHealth -= dmg;
			anim.SetTrigger ("Hurt");
		}
    }

    public IEnumerator Knockback(float knockDuration, float knockPower, Vector3 knockDirection)
    {
        float timer = 0;
		rb2d.velocity = new Vector2 (0, 0);
        while(knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockDirection.x * 10, knockDirection.y * knockPower, transform.position.z));
            attack.enabled = false;
        }
        attack.enabled = true;
        yield return 0;

    }
    public IEnumerator Knockback2(float knockDuration, float knockPower, Vector3 knockDirection)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(0, 0);
        while (knockDuration > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockDirection.x * -10, knockDirection.y * knockPower  , transform.position.z));
            attack.enabled = false;
        }
        attack.enabled = true;
        yield return 0;

    }
	public IEnumerator KnockbackX(float knockDuration, float knockPower, Vector3 knockDirection)
	{
		float timer = 0;
		rb2d.velocity = new Vector2(0, 0);
		while (knockDuration > timer)
		{
			timer += Time.deltaTime;
			rb2d.AddForce(new Vector3(knockDirection.x , knockDirection.y * knockPower  , transform.position.z));
			attack.enabled = false;
		}
		attack.enabled = true;
		yield return 0;

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
        if (col.CompareTag("Checkpoint"))
        {
            checkpointReached = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
		if (col.transform.tag == "MovingPlatform") {
			transform.parent = col.transform;
		} else {
			transform.parent = null;
		}
    }
    void OnCollisonExit2D(Collision2D col)
    {
        if (col.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
    
}





