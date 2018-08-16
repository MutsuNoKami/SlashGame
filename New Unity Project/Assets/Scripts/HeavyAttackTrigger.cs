using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackTrigger : MonoBehaviour {
		public int dmg = 40;
	public AudioSource hit;
		// Use this for initialization
	void Start(){
		hit = GetComponent<AudioSource> ();
	}	
	void OnTriggerEnter2D(Collider2D col)
		{
			if (col.isTrigger != true && col.CompareTag("Enemy"))
			{
				col.SendMessageUpwards("Damage", dmg);
			hit.Play();
			}
		}
	}