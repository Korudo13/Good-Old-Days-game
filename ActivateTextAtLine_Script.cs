using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine_Script : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public bool requireButtonPress;
    private bool waitForPress;


    public TextBoxManager_Script theTextBoxManager;

    public bool destroyWhenActivated;


    private void Start()
    {
        theTextBoxManager = FindObjectOfType<TextBoxManager_Script>();
    }

    private void Update()
    {
        if(waitForPress && Input.GetKeyDown(KeyCode.J))
        {
            theTextBoxManager.ReloadScript(theText);
            theTextBoxManager.currentLine = startLine;
            theTextBoxManager.endAtLine = endLine;
            theTextBoxManager.EnableTextbox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
              if (requireButtonPress)
              {
                  waitForPress = true;
                  return;
              }
            theTextBoxManager.ReloadScript(theText);
            theTextBoxManager.currentLine = startLine;
            theTextBoxManager.endAtLine = endLine;
            theTextBoxManager.EnableTextbox();

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            waitForPress = false;

        }
    }
}
