using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

	public float maxSpeed;
	public float speed;
	public GameObject currentCheckpoint;
	public bool checkpointReached;
	private Player player;
	public bool crouch = false;
	public bool block = false;
    private Rigidbody2D rb2d;
    private Animator anim;
	private gameMaster gm;


	// Use this for initialization
	void Start()
	{

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);

		if (player.transform.localScale.x == -1)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (player.transform.localScale.x == 1)
		{
			transform.localScale = new Vector3(1, 1, 1);
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

}
