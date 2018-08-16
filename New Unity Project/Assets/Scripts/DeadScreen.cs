using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreen : MonoBehaviour {

	public GameObject DeathUI;
	private float timer;
	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		DeathUI.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Pause"))
		{
			player.dead = !player.dead;
		}

		if (player.dead)
		{
			timer += Time.deltaTime;

			if (timer >= 1.2f) {
				DeathUI.SetActive (true);

			}
		}

		if (!player.dead)
		{
			DeathUI.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void Restart()
	{
		
		if (player.checkpointReached)
		{
			player.currentHealth = 100;
			player.dead = false;
			player.transform.position = player.currentCheckpoint.transform.position;
			player.canMove = true;
		}
		else
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	public void MainMenu()
	{
		Application.LoadLevel(0);
	}
	public void Quit()
	{
		Application.Quit();
	}
}
