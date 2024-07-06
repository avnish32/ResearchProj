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
        frndHey.dialogueText = "Hey";
        frndHey.dialogueId = EDialogueID.FRHEY;
        frndHey.relationshipLevels = new List<RelationshipLevel>
        {RelationshipLevel.NEUTRAL, RelationshipLevel.ACQUAINTANCE, RelationshipLevel.FRIEND };
        Dictionary<RelationshipLevel, Action> frndHeyRespMap = new Dictionary<RelationshipLevel, Action>();
        frndHeyRespMap[RelationshipLevel.NEUTRAL] = FrndHeyNeutralAction;
        frndHeyRespMap[RelationshipLevel.ACQUAINTANCE] = FrndHeyAcqAction;
        frndHeyRespMap[RelationshipLevel.FRIEND] = FrndHeyFrndAction;
        frndHey.rshipToResponseMap = frndHeyRespMap;
        dialogueList.Add(frndHey);

        Dialogue frndHelp = new Dialogue();
        Dictionary<RelationshipLevel, Action> frndHelpRespMap = new Dictionary<RelationshipLevel, Action>();
        frndHelp.dialogueText = "Can you help me?";
        frndHelp.dialogueId = EDialogueID.FRHELP;
        frndHelp.relationshipLevels = new List<RelationshipLevel>
        {RelationshipLevel.NEUTRAL, RelationshipLevel.ACQUAINTANCE, RelationshipLevel.FRIEND };
        /*frndHelpRespMap[RelationshipLevel.NEUTRAL] = "Do I know you?";
        frndHelpRespMap[RelationshipLevel.ACQUAINTANCE] = "Sure, what do you need?";
        frndHelpRespMap[RelationshipLevel.FRIEND] = "Definitely, what is it?";*/
        frndHelp.rshipToResponseMap = frndHelpRespMap;
        dialogueList.Add(frndHelp);

        for (int i = 0; i<dialogueList.Count; i++)
        {
            Dialogue currentDialogue = dialogueList[i];
            currentDialogue.dialogueAction = () => { 
                InvokePlayerDialogueAction(currentDialogue.rshipToResponseMap); 
            };
            dialogueList[i] = currentDialogue;
        }

        base.Start();
    }

    private void FrndHeyMasterAction()
    {
        //Debug.Log("Frnd hey action performed.");

        gameController.EnablePlayerMovement();
    }

    private void FrndHeyNeutralAction()
    {
        string[] dialogueList = {
            "Do I know you?"
        };

        uiController.StartNPCDialogues(dialogueList, null);
    }

    private void FrndHeyAcqAction()
    {
        string[] dialogueList = {
            "Hi Jared, how are you?",
            "I hope things are good with you.",
            "How's your job hunt going?"
        };

        Action frndHeyAcqEndAction = () =>
        {
            Debug.Log("Action at the end of friend hey acquaintance NPC Dialogue.");
            gameController.EnablePlayerMovement();
        };

        uiController.StartNPCDialogues(dialogueList, frndHeyAcqEndAction);
    }

    private void FrndHeyFrndAction()
    {
        string[] dialogueList = {
            "Jared! So good to see you!"
        };

        uiController.StartNPCDialogues(dialogueList, null);
    }

    private void FrndHelpAction()
    {
        Debug.Log("Frnd help action performed.");
        gameController.EnablePlayerMovement();
    }

}
