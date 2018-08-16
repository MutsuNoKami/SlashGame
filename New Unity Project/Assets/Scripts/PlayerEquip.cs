using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour {

    public bool shoot;

    public bool gun;
    public bool shield;
    public static int ammo;
	public static int shieldHealth;
	//Floats
	public float bulletSpeed = 10;
    private float attackTimer = 0;
	//References
	private Animator anim;
    public PlayerAttack playerAttack;
	public GameObject Bullet;
	public Transform shootPoint;
    public static bool gunAvailable;
	private gameMaster gm;
    public float bulletTimer;
    public float shootInterval;
	private AudioSource gunshot;
    // Use this for initialization
    void Start () {
   
        anim = gameObject.GetComponent<Animator>();
		playerAttack = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerAttack>();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		gunshot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Shoot", shoot);
        anim.SetBool("Gun", gun);
		anim.SetBool("Shield", shield);
        if (Input.GetButtonDown("Weapon"))
        {
			if (!gun) {
				gun = true;
			} else
				gun = false;
			}



        if (gun)
        {
            playerAttack.enabled = false;
        }
        else
        {
            playerAttack.enabled = true;
        }

		if ((Input.GetAxis("Fire") < 0 && gun && ammo > 0) || Input.GetMouseButton(0) && gun && ammo > 0)
        {
            shoot = true;
            
            bulletTimer += Time.deltaTime;
            if (bulletTimer >= shootInterval)
            {

                if (transform.localScale.x == 1)
                {
                    ammo -= 1;
                    GameObject bulletClone;
                    bulletClone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                    bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
                    bulletTimer = 0;
					gunshot.Play ();

                }
                if (transform.localScale.x == -1 && bulletTimer >= shootInterval)
                {
                    ammo -= 1;
                    GameObject bulletClone;
                    bulletClone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                    bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
                    bulletTimer = 0;
					gunshot.Play ();
                }


            }
        }
        else
        {
            shoot = false;
        }
        if (ammo <= 0)
        {
            gun = false;
            
        }

    }
        

	void OnTriggerEnter2D(Collider2D col)
	{ 
		if (col.CompareTag ("Ammo")) {
			Destroy (col.gameObject);
			ammo += 14;
		}
        if (col.CompareTag("Gun"))
        {
            Destroy(col.gameObject);
            gunAvailable = true;
            gun = true;
            ammo += 28;
        }
		if (col.CompareTag("Shield"))
		{
			Destroy(col.gameObject);
			shield = true;
			shieldHealth += 10;
		}
    }
}
