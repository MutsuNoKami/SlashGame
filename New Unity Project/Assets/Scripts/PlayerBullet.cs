using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

	public int dmg;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true){

			if (col.CompareTag("Enemy")){
		
			col.SendMessageUpwards("Damage2", dmg);
		}
        Destroy(gameObject);
		}
    }
}
