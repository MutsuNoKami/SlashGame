using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBoss2 : MonoBehaviour {

	private gameMaster gm;
	public BossA boss;


	// Use this for initialization
	void Start () {
		boss = FindObjectOfType<BossA>();

		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

	}

	// Update is called once per frame
	void Update () {
		if (boss.currentHealth > (boss.maxHealth * 0.67)) {
			gm.inputText.text = ("Aim for the head. That should bring him offline");
		}
		if (boss.currentHealth < (boss.maxHealth * 0.67) && boss.currentHealth > (boss.maxHealth * 0.33)) {
			gm.inputText.text = ("He might charge at you, Try and get the timing just right with this");
		}
		if (boss.currentHealth < (boss.maxHealth * 0.2)) {
			gm.inputText.text = ("Oh no, get down!");
		}
		if(boss.down)
		{
			gm.inputText.text = ("Go get him before he wakes up!");
		}
	}
}
