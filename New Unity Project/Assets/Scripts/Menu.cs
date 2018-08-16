using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	public Canvas MainCanvas;
	public Canvas OptionsCanvas;

	void Awake()//Activated when the program starts
	{
		OptionsCanvas.enabled = false;
	}

	public void OptionsOn()//Turning on the help screen
	{
		OptionsCanvas.enabled = true;
		MainCanvas.enabled = false;	
	}
	public void ReturnOn()//Turning off the help screen
	{
		OptionsCanvas.enabled = false;
		MainCanvas.enabled = true;
	}

	public void LoadOn()//Start game
	{
		Application.LoadLevel (1);
	}
}