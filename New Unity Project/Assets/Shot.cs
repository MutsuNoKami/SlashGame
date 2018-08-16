using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true && !col.CompareTag("Enemy"))
		{
			if (col.CompareTag("Player"))
			{
				col.GetComponent<Player>().Damage(1);
                

            }
			Destroy(gameObject);

		}

	}
}