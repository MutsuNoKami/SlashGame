using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTest : MonoBehaviour {

	public Slider HealthBar;
	private Player player;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

	}

	// Update is called once per frame
	void Update () {

		HealthBar.value = (player.currentHealth);
	}
}
