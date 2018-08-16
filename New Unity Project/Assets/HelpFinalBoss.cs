using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpFinalBoss : MonoBehaviour {
	private gameMaster gm;
	public MainBoss boss;


	// Use this for initialization
	void Start () {
		boss = FindObjectOfType<MainBoss>();

		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (boss.currentHealth > (boss.maxHealth * 0.6)) {
			gm.inputText.text = ("Keep your distance from him");
		}
		if (boss.currentHealth < (boss.maxHealth * 0.6) && boss.currentHealth > (boss.maxHealth * 0.2)) {
			gm.inputText.text = ("Get ready he's coming at you");
		}
		if (boss.currentHealth < (boss.maxHealth * 0.2)) {
			gm.inputText.text = ("This doesn't look good, hide!");
		}
	}
}
