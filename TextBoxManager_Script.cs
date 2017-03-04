using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager_Script : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset[] textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public bool isActive;
    public bool stopPlayerMovement;
    public bool canMove;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

    public AudioSource voiceSFX;

    private void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile[0].text.Split('\n'));
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
        LoadNextTextFile();
    }


    private IEnumerator TextScroll (string lineOfText)
    {
        //start with empty, then add one letter at a time to the textbox on screen
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        
        //makes the letters appear on screen  by looping until no longer true
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            voiceSFX.Play();
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextbox()
    {
        textBox.SetActive(true);
        isActive = true;


        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableTextbox()
    {
        textBox.SetActive(false);
        isActive = false;
        //player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1]; //take the array of text lines that already exists, replace it with a new text file. Reduces unused indeces.
            textLines = (theText.text.Split('\n'));
        }
    }

    public void LoadNextTextFile()
    {
        if (!canMove)
        {
            return;
        }

        if (!isActive)
        {
            return;
        }

        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    DisableTextbox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }

        if (currentLine > endAtLine)
        {
            DisableTextbox();
        }
    }
}
