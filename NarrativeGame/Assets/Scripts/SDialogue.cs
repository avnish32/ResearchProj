using System.Collections.Generic;
using System;

[System.Serializable]
public struct Dialogue
{
    public EDialogueID dialogueId;
    public List<RelationshipLevel> relationshipLevels;
    public string dialogueText;
    public Dictionary<RelationshipLevel, string> rshipToResponseMap;
    public List<EDialogueID> nextDialogueIds;
    public Action dialogueAction;
};

