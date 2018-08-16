using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour {

    public int dmg = 10;
    private Player player;
	public AudioSource hit;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		hit = GetComponent<AudioSource> ();
    }
        void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
            col.SendMessageUpwards("Damage", dmg);
                if(player.transform.localScale.x == -1)
                { 
                StartCoroutine(player.Knockback(0.01f, 2, player.transform.position));
                }
                if (player.transform.localScale.x == 1)
                {
                    StartCoroutine(player.Knockback2(0.01f, 2, player.transform.position));
                }
            }
        }
	}
}