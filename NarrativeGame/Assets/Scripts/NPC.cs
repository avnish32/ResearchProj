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

    private void PrintDialogues(List<Dialogue> dialogues)
    {
        string dialogueString = "";
        foreach (Dialogue dialogue in dialogues)
        {
            dialogueString += dialogue.dialogueText + "\n";
        }
        Debug.Log(dialogueString);
    }

    public void Interact()
    {
        List<Dialogue> dialogues = GetComponent<DialogueManager>().GetDialogueList();
        PrintDialogues(dialogues);

        uiController.DisplayPlayerDialoguePanel(dialogues);

    }
}
