using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{    
    protected List<SDialogue> dialogueList;
    public PlayerStates stateWPlayer;
    private Dictionary<EDialogueID, SDialogue> dialogueIdsToDialogueMap;
    private Dictionary<PlayerStates, List<EDialogueID>> stateToDialogueIdsMap;

    protected GameController gameController;
    protected UIController uiController;
    protected AudioController audioController;
    protected NPCMovement npcMovement;

    protected void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        audioController = FindObjectOfType<AudioController>();
        npcMovement = GetComponent<NPCMovement>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        stateWPlayer = PlayerStates.NEUTRAL;
        dialogueIdsToDialogueMap = new Dictionary<EDialogueID, SDialogue>();
        stateToDialogueIdsMap = new Dictionary<PlayerStates, List<EDialogueID>>();

        foreach (var dialogue in dialogueList)
        {
            dialogueIdsToDialogueMap[dialogue.dialogueId] = dialogue;
            if (dialogue.playerStates == null)
            {
                continue;
            }

            foreach (var rshipLevel in dialogue.playerStates)
            {
                if (!stateToDialogueIdsMap.ContainsKey(rshipLevel))
                {
                    stateToDialogueIdsMap[rshipLevel] = new List<EDialogueID>();
                }
                stateToDialogueIdsMap[rshipLevel].Add(dialogue.dialogueId);
            }
        }
    }

    protected void InvokePlayerDialogueAction(Dictionary<PlayerStates, Action> rshipToActionMap)
    {
        if (!rshipToActionMap.ContainsKey(stateWPlayer))
        {
            Debug.LogError("ERROR: No action defined for this dialogue at " + stateWPlayer + " state.");
            return;
        }
        rshipToActionMap[stateWPlayer].Invoke();
    }

    protected List<SDialogue> GetDialogueListFromId(List<EDialogueID> dialogueIds)
    {
        List<SDialogue> dialogueList = new List<SDialogue>();
        foreach (var dialogueId in dialogueIds)
        {
            dialogueList.Add(dialogueIdsToDialogueMap[dialogueId]);
        }
        return dialogueList;
    }

    public List<SDialogue> GetDialogueListBasedOnState()
    {
        List<SDialogue> dialogueList = new List<SDialogue>();
        List<EDialogueID> dialogueIds = stateToDialogueIdsMap[stateWPlayer];

        foreach (var dialogueId in dialogueIds)
        {
            dialogueList.Add(dialogueIdsToDialogueMap[dialogueId]);
        }

        return dialogueList;
    }

    public void SetStateWPlayer(PlayerStates state)
    {
        this.stateWPlayer = state;
    }

    public PlayerStates GetStateWPlayer()
    {
        return this.stateWPlayer;
    }

}
