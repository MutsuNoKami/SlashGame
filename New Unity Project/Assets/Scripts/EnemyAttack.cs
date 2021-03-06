using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public Collider2D attackTrigger;

    private bool attacking;
	public float distance;
    public Transform Target;
    public float attackTimer;
    public float attackCd = 0.01f;

	private Animator anim;



	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
		attackTrigger.enabled = false;

    }

	// Update is called once per frame
	void Update ()
	{
        anim.SetFloat("Distance", distance);
        distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance < 4 ) {
            Wait();
            attackTimer = attackCd;
            attackTimer -= Time.deltaTime;
            attackTrigger.enabled = true;
            
		} 
		else if (distance >= 4)
		{

            attackTrigger.enabled = false;
        }
        

    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(6);
        Update();
    }
}