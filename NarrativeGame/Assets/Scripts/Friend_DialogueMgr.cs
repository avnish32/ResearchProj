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
        dialogueList = new List<SDialogue>();
        PopulateDialogueList();

        for (int i = 0; i < dialogueList.Count; i++)
        {
            SDialogue currentDialogue = dialogueList[i];
            currentDialogue.dialogueAction = () =>
            {
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

        uiController.StartNPCDialogues(dialogueList, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRRUDE, EDialogueID.FRCANKNOWME });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
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

    private void FrndCanKnowMeAction()
    {
        string[] dialogueList = {
            "And why would I wanna do that? With school and studies and everything, " +
            "I've got enough boring stuff in my life already."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndHelpAction()
    {
        Debug.Log("Frnd help action performed.");
        gameController.EnablePlayerMovement();
    }

    private void PopulateDialogueList()
    {
        {
            SDialogue frndHey = new SDialogue();
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
        }

        {
            SDialogue frndHelp = new SDialogue();
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
        }

        {
            SDialogue frndRude = new SDialogue();
            Dictionary<RelationshipLevel, Action> frndRudeRespMap = new Dictionary<RelationshipLevel, Action>();
            frndRude.dialogueText = "Okay. Rude. Bye.";
            frndRude.dialogueId = EDialogueID.FRRUDE;
            frndRudeRespMap[RelationshipLevel.NEUTRAL] = gameController.EnablePlayerMovement;
            frndRude.rshipToResponseMap = frndRudeRespMap;
            dialogueList.Add(frndRude);
        }

        {
            SDialogue frndCanKnowMe = new SDialogue();
            Dictionary<RelationshipLevel, Action> frndCanKnowMeRespMap = new Dictionary<RelationshipLevel, Action>();
            frndCanKnowMe.dialogueText = "No, but you can.";
            frndCanKnowMe.dialogueId = EDialogueID.FRCANKNOWME;
            frndCanKnowMeRespMap[RelationshipLevel.NEUTRAL] = FrndCanKnowMeAction;
            frndCanKnowMe.rshipToResponseMap = frndCanKnowMeRespMap;
            dialogueList.Add(frndCanKnowMe);
        }


    }

}
