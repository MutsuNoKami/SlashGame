using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour
{

    public TextAsset text;

    public int startLine;
    public int endLine;

    public TextBoxManager textBox;
    public Player player;
    public bool destroyWhenActivated;
    // Use this for initialization
    void Start()
    {
        textBox = FindObjectOfType<TextBoxManager>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            textBox.ReloadScript(text);
            textBox.stopPlayerMovement = true;
            textBox.currentLine = startLine;
            textBox.endAtLine = endLine;
            textBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
}
