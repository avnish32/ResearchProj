using System.Collections.Generic;
using System;

[System.Serializable]
public struct SDialogue
{
    public EDialogueID dialogueId;
    public List<PlayerStates> playerStates;
    public string dialogueText;
    public Dictionary<PlayerStates, Action> rshipToResponseMap;
    public List<EDialogueID> nextDialogueIds;
    public Action dialogueAction;
};

