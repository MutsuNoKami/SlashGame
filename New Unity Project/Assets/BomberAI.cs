using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : MonoBehaviour {

	public int currentHealth;
	public int maxHealth;
	public float distance;
	public float speed = 10;
	public float maxSpeed = 1;

	private Player player;
	private Bomber bomber;
	public Transform Target;
	private Rigidbody2D rb2d;
	private Animator anim;


	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		bomber = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Bomber> ();
	}

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (transform.position, Target.transform.position);

		if (currentHealth <= 0) {
			Destroy (gameObject);
		}

		if (Target.transform.position.x < transform.position.x && distance < 20) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.Translate(Vector2.MoveTowards(transform.position, Target.transform.position, distance) * speed * Time.deltaTime);

		} else if (Target.transform.position.x > transform.position.x && distance < 20) {
			transform.localScale = new Vector3 (-1, 1, 1);
			transform.Translate(Vector2.MoveTowards(transform.position, Target.transform.position, distance) * speed * Time.deltaTime);
			}
		}
	}
