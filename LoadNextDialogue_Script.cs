using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadNextDialogue_Script : MonoBehaviour {

   [SerializeField]private GameObject[] dialogueObjectList;
    private int currentDialogueObject = -1;


    private void Start()
    {
       //dialogueObjectList = GameObject.FindGameObjectsWithTag("DialogueObject").OrderBy(function(g){g.SetActive(false); return g.name;}).ToArray();
    }

    void NextDialogue()
    {
       dialogueObjectList[(currentDialogueObject++ % dialogueObjectList.Length + dialogueObjectList.Length) % dialogueObjectList.Length].SetActive(false);
        dialogueObjectList[currentDialogueObject].SetActive(true);
    }
}


/* Friday March 3, 2017 Goal:
 * 
 *   - Research Linq and Data Structures in C# for manipulating arrays of strings
 *   - Research Algorithms for doing this.
 *   - Have an idea ready for tomorrow.
 * 
 * 
 * 
 * 
 * Saturday March 4, 2017 Goal:
 * 
 *   - Implement way to:
 *              - disable array when it reaches the end (Grandpa text)
 *              - replace it with a new array (Billy text)
 *              
 *              
 */