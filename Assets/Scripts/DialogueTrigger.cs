using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueString> dialogueStrings = new();


    private void Start()
    {
        NPC.Instance.OnStartDialogue += Selected_OnStartDialogue;
    }

    private void Selected_OnStartDialogue(object sender, EventArgs e)
    {
        GameObject playerObject = GameObject.Find("Player");
        if(playerObject != null)
        {
            playerObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings);

        }
    }
}

[System.Serializable]

public class DialogueString
{
    public string text;
    public bool isEnd;

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;

}