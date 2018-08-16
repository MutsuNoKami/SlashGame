using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth2 : MonoBehaviour {

	public Slider HealthBar;
	private BossA boss;


	// Use this for initialization
	void Start()
	{
		boss = FindObjectOfType<BossA>();
	}

	// Update is called once per frame
	void Update()
	{
		HealthBar.value = (boss.currentHealth);
	}
}
