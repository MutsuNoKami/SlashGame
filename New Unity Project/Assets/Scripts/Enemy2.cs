using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    public Transform[] patrolpoints;
    public int currentPoint;
    public float speed = 0.05f;
	public float timeStill;
	public float currentHealth;
	public float maxHealth;
    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
		currentHealth = maxHealth;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (currentHealth <= 0) {
			Destroy (gameObject);
		}
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (patrolpoints [currentPoint].position.x, transform.position.y), speed);

		if (transform.position.x < patrolpoints [currentPoint].position.x)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (transform.position.x > patrolpoints [currentPoint].position.x)
			transform.localScale = new Vector3 (1, 1, 1);
		if (transform.position.x == patrolpoints [currentPoint].position.x) {
			
			timeStill += Time.deltaTime;
			if (timeStill >= 2) {
				currentPoint++;
				timeStill = 0;
			}
		}
			if (currentPoint >= patrolpoints.Length) {
				currentPoint = 0;
			}
		}



    
    public void Damage(int damage)
    {
        currentHealth -= damage;
		anim.SetTrigger("Hurt");

    }
	public void Damage2(int damage)
	{

		anim.SetTrigger("Hurt");
		currentHealth -= damage;
	}
}
