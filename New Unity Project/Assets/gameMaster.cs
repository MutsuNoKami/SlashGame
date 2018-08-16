using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

	public static int points;
	public PlayerEquip player;
	public Text pointsText;
	public Text ammoText;
	public Text inputText;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquip>();
	}
	// Update is called once per frame
	void Update () {

		pointsText.text = ("Points: " + points);
		ammoText.text = ("Ammo: " + PlayerEquip.ammo);

		
	}
}
