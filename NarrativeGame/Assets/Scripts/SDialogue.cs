using System.Collections.Generic;
using System;

[System.Serializable]
public struct SDialogue
{
    public EDialogueID dialogueId;
    public List<RelationshipLevel> relationshipLevels;
    public string dialogueText;
    public Dictionary<RelationshipLevel, Action> rshipToResponseMap;
    public List<EDialogueID> nextDialogueIds;
    public Action dialogueAction;
};

