using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager_Script : MonoBehaviour {

    public GameObject textBox;

    public Text theText;
    public TextAsset[] textFile;

    public AudioSource[] voiceSFX;
    private int currentSFX;

    public string[] textLines;

    public int currentTextFile;
    public int currentLine;
    public int endAtLine;

    private int currentPersonTalking;

    public bool isActive;
    public bool stopPlayerMovement;
    public bool canMove;

    private bool isTyping = false;
    private bool cancelTyping = false;
    public Color textColor1;
    public Color textColor2;

    public float typeSpeed;

    private void Awake()
    {
        currentTextFile = 0;
    }


    private void Start()
    {
        MonologueTalking();
        theText.color = textColor1;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        LoadNextTextFile();
    }


    private IEnumerator TextScroll(string lineOfText)
    {
        //start with empty, then add one letter at a time to the textbox on screen
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        //makes the letters appear on screen  by looping until no longer true
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            voiceSFX[currentSFX].Play();
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
    }

    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    //DisableTextbox();
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
            currentTextFile = 1;
            theText.color = textColor2;
            currentSFX = 1;
            currentLine = 0;
            MonologueTalking();
        }
    }

    void MonologueTalking()
    {
        if (textFile != null)
        {
            textLines = (textFile[currentTextFile].text.Split('\n'));
        }

        if (endAtLine == 0) //go to the final line, then stop
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
}