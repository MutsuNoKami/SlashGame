using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth1 : MonoBehaviour
{
	public Slider HealthBar;
	private BossPhases bossPhases;


	// Use this for initialization
	void Start()
	{
		bossPhases = FindObjectOfType<BossPhases>();
	}

	// Update is called once per frame
	void Update()
	{

		HealthBar.value = (bossPhases.currentHealth);
	}
}
