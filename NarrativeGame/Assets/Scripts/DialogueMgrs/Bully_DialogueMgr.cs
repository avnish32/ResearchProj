using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bully_DialogueMgr : DialogueManager
{
    [SerializeField]
    GameObject wabMinigamePrefab;

    // ### voices
    [SerializeField]
    AudioClip whatVoice, hahVoice, yeahYeahVoice;
    // ### voices end

    [SerializeField]
    AudioClip winSfx, loseSfx;

    private const ECharacters BULLY_CHAR = ECharacters.BULLY;

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

    private void BullyHeyNeutralAction()
    {
        string[] dialogueList =
        {
            "Hey, little guy. Think you're in the wrong building.",
            "Kindergarten is that way."
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void BullyHeyMentionedAction()
    {
        string[] dialogueList =
        {
            "Hey, kiddo. Are you lost? Can't find your parents?"
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.BULLYWHATSWITHSIS, EDialogueID.BULLYPLSTELLIFKNOW });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void BullyHeyFoughtAction()
    {
        string[] dialogueList =
        {
            "Wassup tiny fellow, back for more?"
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.BULLYBRINGITON, EDialogueID.BULLYPASSINGBY });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void BullyHadEnufAction()
    {
        string[] dialogueList =
        {
            "Yeah yeah...I got you loud and clear.",
            "I won’t bother her again."
        };

        audioController.PlaySound(yeahYeahVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void BullyWhatsWithSisAction()
    {
        string[] dialogueList =
        {
            "Huh? What do you mean?"
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.BULLYSISUPSETWIDU, EDialogueID.BULLYWHYBULLYING });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void BullyPlsTellIfKnowAction()
    {
        string[] dialogueList =
        {
            "Why? You got a crush on her or something?",
            "I ain’t telling you anything!"
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void BullyWhyBullyingAction()
    {
        string[] dialogueList =
        {
            "I do what I please. Who are you, my dad?"
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.BULLYSTOPBOTHERINGHER, EDialogueID.BULLYGOODIFUSTOP });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void BullySisUpsetWidUAction()
    {
        string[] dialogueList =
        {
            "Do I look like I care?",
            "Go away before you get crushed under my feet."
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void BullyStopBotheringHerAction()
    {
        string[] dialogueList =
        {
            "Oh, you think you’re her knight in shining armour, huh?",
            "*shoves you aside forcefully, almost knocking you over*"
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.BULLYUASKEDFORIT, EDialogueID.BULLYSORRYIMLEAVING });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void BullyGoodIfUStopAction()
    {
        string[] dialogueList =
        {
            "Ahhh....I seeeee...you know what would be even better?",
            "If you take your sorry face away before I smash it to a pulp."
        };

        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void StartWABMinigame()
    {
        stateWPlayer = PlayerStates.BULLYFOUGHT;
        GameObject wabMinigame = Instantiate(wabMinigamePrefab);
        wabMinigame.GetComponent<WAB>().Init(audioController, gameController);
    }

    private void BullySorryImLeavingAction()
    {
        gameController.EnablePlayerMovement();
    }

    private void BullyPassingByAction()
    {
        gameController.EnablePlayerMovement();
    }

    public void OnBullyDefeated()
    {
        stateWPlayer = PlayerStates.BULLYDEFEATED;
        string[] dialogueList =
       {
            "Ow! Okay, okay! I get it, man!",
            "I won't even go near her, I swear.",
            "Leave me alone, for god's sake."
        };

        audioController.PlaySound(winSfx);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    public void OnBullyWon()
    {
        string[] dialogueList =
       {
            "You see what happens when you mess with me?",
            "Be thankful you still got a face to take back home.",
            "Run along now, and stay away, you hear?"
        };

        audioController.PlaySound(loseSfx);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, gameController.EnablePlayerMovement);
    }

    private void PopulateDialogueList()
    {
        //######## LEAF DIALOGUES ############
        {
            SDialogue bullyHey = new SDialogue();
            bullyHey.dialogueText = "Hey!";
            bullyHey.dialogueId = EDialogueID.BULLYHEY;
            bullyHey.playerStates = new List<PlayerStates>
            {PlayerStates.NEUTRAL, PlayerStates.WILLTALK2BULLY, PlayerStates.BULLYFOUGHT};
            Dictionary<PlayerStates, Action> bullyHeyRespMap = new Dictionary<PlayerStates, Action>();
            bullyHeyRespMap[PlayerStates.NEUTRAL] = BullyHeyNeutralAction;
            bullyHeyRespMap[PlayerStates.WILLTALK2BULLY] = BullyHeyMentionedAction;
            bullyHeyRespMap[PlayerStates.BULLYFOUGHT] = BullyHeyFoughtAction;
            bullyHey.rshipToResponseMap = bullyHeyRespMap;
            dialogueList.Add(bullyHey);
        }

        {
            SDialogue bullyHadEnuf = new SDialogue();
            bullyHadEnuf.dialogueText = "Had enough?";
            bullyHadEnuf.dialogueId = EDialogueID.BULLYHADENUF;
            bullyHadEnuf.playerStates = new List<PlayerStates>{PlayerStates.BULLYDEFEATED};
            Dictionary<PlayerStates, Action> bullyHadEnufRespMap = new Dictionary<PlayerStates, Action>();
            bullyHadEnufRespMap[PlayerStates.BULLYDEFEATED] = BullyHadEnufAction;
            bullyHadEnuf.rshipToResponseMap = bullyHadEnufRespMap;
            dialogueList.Add(bullyHadEnuf);
        }

        //######## LEAF DIALOGUES END############

        {
            SDialogue bullyWhyBothering = new SDialogue();
            bullyWhyBothering.dialogueText = "What's with you and Anya's sister?";
            bullyWhyBothering.dialogueId = EDialogueID.BULLYWHATSWITHSIS;
            Dictionary<PlayerStates, Action> bullyWhyBotheringRespMap = new Dictionary<PlayerStates, Action>();
            bullyWhyBotheringRespMap[PlayerStates.WILLTALK2BULLY] = BullyWhatsWithSisAction;
            bullyWhyBothering.rshipToResponseMap = bullyWhyBotheringRespMap;
            dialogueList.Add(bullyWhyBothering);
        }

        {
            SDialogue bullyPlsTellIfKnow = new SDialogue();
            bullyPlsTellIfKnow.dialogueText = "Could you please tell me if you know Anya’s sister?";
            bullyPlsTellIfKnow.dialogueId = EDialogueID.BULLYPLSTELLIFKNOW;
            Dictionary<PlayerStates, Action> bullyPlsTellIfKnowRespMap = new Dictionary<PlayerStates, Action>();
            bullyPlsTellIfKnowRespMap[PlayerStates.WILLTALK2BULLY] = BullyPlsTellIfKnowAction;
            bullyPlsTellIfKnow.rshipToResponseMap = bullyPlsTellIfKnowRespMap;
            dialogueList.Add(bullyPlsTellIfKnow);
        }

        {
            SDialogue bullyWhyBullying = new SDialogue();
            bullyWhyBullying.dialogueText = "Why are you bullying her?";
            bullyWhyBullying.dialogueId = EDialogueID.BULLYWHYBULLYING;
            Dictionary<PlayerStates, Action> bullyWhyBullyingRespMap = new Dictionary<PlayerStates, Action>();
            bullyWhyBullyingRespMap[PlayerStates.WILLTALK2BULLY] = BullyWhyBullyingAction;
            bullyWhyBullying.rshipToResponseMap = bullyWhyBullyingRespMap;
            dialogueList.Add(bullyWhyBullying);
        }

        {
            SDialogue bullySisUpsetWidU = new SDialogue();
            bullySisUpsetWidU.dialogueText = "You know, Anya’s sis...she’s just...a bit upset with you, that’s all.";
            bullySisUpsetWidU.dialogueId = EDialogueID.BULLYSISUPSETWIDU;
            Dictionary<PlayerStates, Action> bullySisUpsetWidURespMap = new Dictionary<PlayerStates, Action>();
            bullySisUpsetWidURespMap[PlayerStates.WILLTALK2BULLY] = BullySisUpsetWidUAction;
            bullySisUpsetWidU.rshipToResponseMap = bullySisUpsetWidURespMap;
            dialogueList.Add(bullySisUpsetWidU);
        }

        {
            SDialogue bullyStopBotheringHer = new SDialogue();
            bullyStopBotheringHer.dialogueText = "Whatever, dude. Just stop bothering her, will you?";
            bullyStopBotheringHer.dialogueId = EDialogueID.BULLYSTOPBOTHERINGHER;
            Dictionary<PlayerStates, Action> bullyStopBotheringHerRespMap = new Dictionary<PlayerStates, Action>();
            bullyStopBotheringHerRespMap[PlayerStates.WILLTALK2BULLY] = BullyStopBotheringHerAction;
            bullyStopBotheringHer.rshipToResponseMap = bullyStopBotheringHerRespMap;
            dialogueList.Add(bullyStopBotheringHer);
        }

        {
            SDialogue bullyGoodIfUStop = new SDialogue();
            bullyGoodIfUStop.dialogueText = "Uh, no...just...it would be good if you could stop messing with her, you know.";
            bullyGoodIfUStop.dialogueId = EDialogueID.BULLYGOODIFUSTOP;
            Dictionary<PlayerStates, Action> bullyGoodIfUStopRespMap = new Dictionary<PlayerStates, Action>();
            bullyGoodIfUStopRespMap[PlayerStates.WILLTALK2BULLY] = BullyGoodIfUStopAction;
            bullyGoodIfUStop.rshipToResponseMap = bullyGoodIfUStopRespMap;
            dialogueList.Add(bullyGoodIfUStop);
        }

        {
            SDialogue bullyUAskedForIt = new SDialogue();
            bullyUAskedForIt.dialogueText = "Okay, big man, you asked for it. *gets ready to fight*";
            bullyUAskedForIt.dialogueId = EDialogueID.BULLYUASKEDFORIT;
            Dictionary<PlayerStates, Action> bullyUAskedForItRespMap = new Dictionary<PlayerStates, Action>();
            bullyUAskedForItRespMap[PlayerStates.WILLTALK2BULLY] = StartWABMinigame;
            bullyUAskedForIt.rshipToResponseMap = bullyUAskedForItRespMap;
            dialogueList.Add(bullyUAskedForIt);
        }

        {
            SDialogue bullySorryImLeaving = new SDialogue();
            bullySorryImLeaving.dialogueText = "Woah, sorry mate. Jeez, no need for all that, I’m leaving.";
            bullySorryImLeaving.dialogueId = EDialogueID.BULLYSORRYIMLEAVING;
            Dictionary<PlayerStates, Action> bullySorryImLeavingRespMap = new Dictionary<PlayerStates, Action>();
            bullySorryImLeavingRespMap[PlayerStates.WILLTALK2BULLY] = BullySorryImLeavingAction;
            bullySorryImLeaving.rshipToResponseMap = bullySorryImLeavingRespMap;
            dialogueList.Add(bullySorryImLeaving);
        }

        {
            SDialogue bullyBringItOn = new SDialogue();
            bullyBringItOn.dialogueText = "Yes. Bring it on!";
            bullyBringItOn.dialogueId = EDialogueID.BULLYBRINGITON;
            Dictionary<PlayerStates, Action> bullyBringItOnRespMap = new Dictionary<PlayerStates, Action>();
            bullyBringItOnRespMap[PlayerStates.BULLYFOUGHT] = StartWABMinigame;
            bullyBringItOn.rshipToResponseMap = bullyBringItOnRespMap;
            dialogueList.Add(bullyBringItOn);
        }

        {
            SDialogue bullyPassingBy = new SDialogue();
            bullyPassingBy.dialogueText = "No, I was just passing by. Sorry!";
            bullyPassingBy.dialogueId = EDialogueID.BULLYPASSINGBY;
            Dictionary<PlayerStates, Action> bullyPassingByRespMap = new Dictionary<PlayerStates, Action>();
            bullyPassingByRespMap[PlayerStates.BULLYFOUGHT] = BullyPassingByAction;
            bullyPassingBy.rshipToResponseMap = bullyPassingByRespMap;
            dialogueList.Add(bullyPassingBy);
        }
    }
}
