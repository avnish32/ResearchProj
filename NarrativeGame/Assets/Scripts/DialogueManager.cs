using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    protected List<SDialogue> dialogueList;
    private RelationshipLevel rshipWLead;
    private Dictionary<EDialogueID, SDialogue> dialogueIdsToDialogueMap;
    private Dictionary<RelationshipLevel, List<EDialogueID>> rshipLevelToDialogueIdsMap;

    protected GameController gameController;
    protected UIController uiController;

    protected void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        rshipWLead = RelationshipLevel.NEUTRAL;
        dialogueIdsToDialogueMap = new Dictionary<EDialogueID, SDialogue>();
        rshipLevelToDialogueIdsMap = new Dictionary<RelationshipLevel, List<EDialogueID>>();

        foreach (var dialogue in dialogueList)
        {
            dialogueIdsToDialogueMap[dialogue.dialogueId] = dialogue;
            if (dialogue.relationshipLevels == null)
            {
                continue;
            }

            foreach (var rshipLevel in dialogue.relationshipLevels)
            {
                if (!rshipLevelToDialogueIdsMap.ContainsKey(rshipLevel))
                {
                    rshipLevelToDialogueIdsMap[rshipLevel] = new List<EDialogueID>();
                }
                rshipLevelToDialogueIdsMap[rshipLevel].Add(dialogue.dialogueId);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void InvokePlayerDialogueAction(Dictionary<RelationshipLevel, Action> rshipToActionMap)
    {
        if (!rshipToActionMap.ContainsKey(rshipWLead))
        {
            Debug.LogError("ERROR: No action defined for this dialogue at " + rshipWLead + " rship.");
            return;
        }
        rshipToActionMap[rshipWLead].Invoke();
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

    public List<SDialogue> GetDialogueListBasedOnRship()
    {
        List<SDialogue> dialogueList = new List<SDialogue>();
        List<EDialogueID> dialogueIds = rshipLevelToDialogueIdsMap[rshipWLead];

        foreach (var dialogueId in dialogueIds)
        {
            dialogueList.Add(dialogueIdsToDialogueMap[dialogueId]);
        }

        return dialogueList;
    }
}
