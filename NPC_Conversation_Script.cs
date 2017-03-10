using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Conversation_Script : MonoBehaviour
{

    public GameObject npc_dialogueBox;
    private bool isShowing;

    private void Start()
    {
        npc_dialogueBox.SetActive(true);
        isShowing = true;
    }

    private void Update()
    {
        if (isShowing == true)
        {
            if (Input.GetKeyDown("e"))
            {
                isShowing = false;
                npc_dialogueBox.SetActive(false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            npc_dialogueBox.SetActive(false);

        }
    }
}

