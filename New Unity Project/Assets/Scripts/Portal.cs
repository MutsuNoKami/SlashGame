using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour {

	public int LevelToLoad;

	private gameMaster gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {
			gm.inputText.text = ("Press enter");

			if (Input.GetButtonDown ("Submit")) {
				Application.LoadLevel(LevelToLoad);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) {

			if (Input.GetButtonDown ("Submit")) {
				Application.LoadLevel(LevelToLoad);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("Player"))
		{
			gm.inputText.text = (" ");
		}
	}
}