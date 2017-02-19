using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager_Script : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;

    //public PlayerController player;

    private void Start()
    {
        //player = FindObjectOfType<PlayerController>();
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0) //go to the final line, then stop
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextbox();
        }
        else
        {
            DisableTextbox();
        }
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine += 1;
        }

        if(currentLine > endAtLine)
        {
            DisableTextbox();
        }
    }

    public void EnableTextbox()
    {
        textBox.SetActive(true);
    }

    public void DisableTextbox()
    {
        textBox.SetActive(false);
    }
}
