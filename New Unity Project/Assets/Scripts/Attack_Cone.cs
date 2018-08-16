using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {

	public TurretAI turretAI;

	public bool isLeft = false;

	// Use this for initialization
	void Start ()
	{
		turretAI = gameObject.GetComponentInParent<TurretAI>();
	}

	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Player"))
		{
			if (isLeft) 
			{
				turretAI.Attack (false);
			} 
			else
			{
				turretAI.Attack (true);
			}
		}
	}
}
