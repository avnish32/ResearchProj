using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject npcMarker, interactText;

    private UIController uiController;
    private GameController gameController;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        interactText.SetActive(false);
        npcMarker.SetActive(true);
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
        if (!gameController.CanPlayerMoveOrInteract())
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

        gameController.DisablePlayerMovement();
        List<SDialogue> dialogues = myDialogueMgr.GetDialogueListBasedOnState();
        //PrintDialogues(dialogues);

        uiController.DisplayPlayerDialoguePanel(dialogues);
    }

    public void OnPlayerEnteredToInteract() {
        npcMarker.SetActive(false);
        interactText.SetActive(true);
    }

    public void OnPlayerExited() {
        npcMarker.SetActive(true);
        interactText.SetActive(false);
    }
}
