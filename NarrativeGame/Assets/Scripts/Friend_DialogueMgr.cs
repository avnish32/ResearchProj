using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend_DialogueMgr : DialogueManager
{
    new void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    new void Start()
    {
        dialogueList = new List<Dialogue>();

        Dialogue frndHey = new Dialogue();
        Dictionary<RelationshipLevel, string> frndHeyRespMap = new Dictionary<RelationshipLevel, string>();
        frndHey.dialogueText = "Hey";
        frndHey.dialogueId = EDialogueID.FRHEY;
        frndHey.relationshipLevels = new List<RelationshipLevel>
        {RelationshipLevel.NEUTRAL, RelationshipLevel.ACQUAINTANCE, RelationshipLevel.FRIEND };
        frndHeyRespMap[RelationshipLevel.NEUTRAL] = "Do I know you?";
        frndHeyRespMap[RelationshipLevel.ACQUAINTANCE] = "Hi.";
        frndHeyRespMap[RelationshipLevel.FRIEND] = "Hi, how are you?";
        frndHey.rshipToResponseMap = frndHeyRespMap;
        frndHey.dialogueAction = FrndHeyAction;
        dialogueList.Add(frndHey);

        Dialogue frndHelp = new Dialogue();
        Dictionary<RelationshipLevel, string> frndHelpRespMap = new Dictionary<RelationshipLevel, string>();
        frndHelp.dialogueText = "Can you help me?";
        frndHelp.dialogueId = EDialogueID.FRHELP;
        frndHelp.relationshipLevels = new List<RelationshipLevel>
        {RelationshipLevel.NEUTRAL, RelationshipLevel.ACQUAINTANCE, RelationshipLevel.FRIEND };
        frndHelpRespMap[RelationshipLevel.NEUTRAL] = "Do I know you?";
        frndHelpRespMap[RelationshipLevel.ACQUAINTANCE] = "Sure, what do you need?";
        frndHelpRespMap[RelationshipLevel.FRIEND] = "Definitely, what is it?";
        frndHelp.rshipToResponseMap = frndHelpRespMap;
        frndHelp.dialogueAction = FrndHelpAction;
        dialogueList.Add(frndHelp);

        base.Start();
    }

    private void FrndHeyAction()
    {
        Debug.Log("Frnd hey action performed.");
        gameController.EnablePlayerMovement();
    }

    private void FrndHelpAction()
    {
        Debug.Log("Frnd help action performed.");
        gameController.EnablePlayerMovement();
    }

}
