using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth3 : MonoBehaviour {


	public Slider HealthBar;
	private MainBoss boss;


	// Use this for initialization
	void Start()
	{
		boss = FindObjectOfType<MainBoss>();
	}

	// Update is called once per frame
	void Update()
	{
		HealthBar.value = (boss.currentHealth);
	}
}
