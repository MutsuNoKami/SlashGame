using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {

	private BossA boss;

	void Start ()
	{
		boss = GameObject.FindObjectOfType<BossA> ();
	}
			
	void OnTriggerEnter2D(Collider2D col)
	{
			if (col.CompareTag ("Trigger")) 
			{
			boss.down = true;
			}
	}
}