using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public MovingPlatform platform;

	// Use this for initialization
	void Start () {
        platform = gameObject.GetComponentInParent<MovingPlatform>();
	}

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            if(Input.GetButtonDown("Submit"))
            {
                platform.switchOn = true;
            }

    }

}

