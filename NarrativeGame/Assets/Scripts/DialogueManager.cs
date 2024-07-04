using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    protected List<Dialogue> dialogueList;
    private RelationshipLevel rshipWLead;
    private Dictionary<EDialogueID, Dialogue> dialogueIdsToDialogueMap;
    private Dictionary<RelationshipLevel, List<EDialogueID>> rshipLevelToDialogueIdsMap;

    // Start is called before the first frame update
    protected void Start()
    {
        rshipWLead = RelationshipLevel.NEUTRAL;
        dialogueIdsToDialogueMap = new Dictionary<EDialogueID, Dialogue>();
        rshipLevelToDialogueIdsMap = new Dictionary<RelationshipLevel, List<EDialogueID>>();

        foreach (var dialogue in dialogueList)
        {
            dialogueIdsToDialogueMap[dialogue.dialogueId] = dialogue;

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

    public List<Dialogue> GetDialogueList()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        List<EDialogueID> dialogueIds = rshipLevelToDialogueIdsMap[rshipWLead];

        foreach (var dialogueId in dialogueIds)
        {
            dialogueList.Add(dialogueIdsToDialogueMap[dialogueId]);
        }

        return dialogueList;
    }
}
