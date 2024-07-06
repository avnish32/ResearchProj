using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
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
        List<SDialogue> dialogues = GetComponent<DialogueManager>().GetDialogueListBasedOnRship();
        //PrintDialogues(dialogues);

        uiController.DisplayPlayerDialoguePanel(dialogues);

    }
}
