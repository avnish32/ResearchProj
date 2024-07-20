using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private UIController uiController;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrintDialogues(List<SDialogue> dialogues)
    {
        string dialogueString = "";
        foreach (SDialogue dialogue in dialogues)
        {
            dialogueString += dialogue.dialogueText + "\n";
        }
        Debug.Log(dialogueString);
    }

    public void Interact()
    {
        if (uiController.IsAnyDialogueGoingOn())
        {
            return;
        }

        DialogueManager myDialogueMgr = GetComponent<DialogueManager>();
        if (myDialogueMgr == null)
        {
            return;
        }

        NPCMovement myNpcMovement = GetComponent<NPCMovement>();
        if (myNpcMovement != null)
        {
            myNpcMovement.StopWalking();
        }
        
        List<SDialogue> dialogues = myDialogueMgr.GetDialogueListBasedOnRship();
        //PrintDialogues(dialogues);

        uiController.DisplayPlayerDialoguePanel(dialogues);

    }
}