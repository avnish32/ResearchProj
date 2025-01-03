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
    AudioClip yeahYeahVoice, heyKidVoice,
        wassupVoice, whatDoUMeanVoice, whyVoice, IDoWatIPleaseVoice,
        doICareVoice, ohVoice, ISeeVoice;
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
                InvokePlayerDialogueAction(currentDialogue.stateToResponseMap);
            };
            dialogueList[i] = currentDialogue;
        }

        base.Start();
    }

    private void OnDialogueEnd()
    {
        gameController.EnablePlayerMovement();
    }

    private void BullyHeyNeutralAction()
    {
        string[] dialogueList =
        {
            "Hey, little guy. Think you're in the wrong building.",
            "Kindergarten is that way."
        };

        audioController.PlaySound(heyKidVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    private void BullyHeyMentionedAction()
    {
        string[] dialogueList =
        {
            "Hey, kiddo. Are you lost? Can't find your parents?"
        };

        audioController.PlaySound(heyKidVoice);
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

        audioController.PlaySound(wassupVoice);
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
            "I won�t bother her again."
        };

        audioController.PlaySound(yeahYeahVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    private void BullyWhatsWithSisAction()
    {
        string[] dialogueList =
        {
            "Huh? What do you mean?"
        };

        audioController.PlaySound(whatDoUMeanVoice);
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
            "I ain�t telling you anything!"
        };

        audioController.PlaySound(whyVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    private void BullyWhyBullyingAction()
    {
        string[] dialogueList =
        {
            "I do what I please. Who are you, my dad?"
        };

        audioController.PlaySound(IDoWatIPleaseVoice);
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

        audioController.PlaySound(doICareVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    private void BullyStopBotheringHerAction()
    {
        string[] dialogueList =
        {
            "Oh, you think you�re her knight in shining armour, huh?",
            "*shoves you aside forcefully, almost knocking you over*"
        };

        audioController.PlaySound(ohVoice);
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

        audioController.PlaySound(ISeeVoice);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    private void StartWABMinigame()
    {
        stateWPlayer = PlayerStates.BULLYFOUGHT;
        GameObject wabMinigame = Instantiate(wabMinigamePrefab);
        wabMinigame.GetComponent<WAB>().Init(audioController, gameController);
    }

    private void BullySorryImLeavingAction()
    {
        OnDialogueEnd();
    }

    private void BullyPassingByAction()
    {
        OnDialogueEnd();
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

        //audioController.PlaySound(winSfx);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
    }

    public void OnBullyWon()
    {
        string[] dialogueList =
       {
            "You see what happens when you mess with me?",
            "Be thankful you still got a face to take back home.",
            "Run along now, and stay away, you hear?"
        };

        //audioController.PlaySound(loseSfx);
        uiController.StartDialogues(dialogueList, BULLY_CHAR, OnDialogueEnd);
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
            bullyHey.stateToResponseMap = bullyHeyRespMap;
            dialogueList.Add(bullyHey);
        }

        {
            SDialogue bullyHadEnuf = new SDialogue();
            bullyHadEnuf.dialogueText = "Had enough?";
            bullyHadEnuf.dialogueId = EDialogueID.BULLYHADENUF;
            bullyHadEnuf.playerStates = new List<PlayerStates>{PlayerStates.BULLYDEFEATED};
            Dictionary<PlayerStates, Action> bullyHadEnufRespMap = new Dictionary<PlayerStates, Action>();
            bullyHadEnufRespMap[PlayerStates.BULLYDEFEATED] = BullyHadEnufAction;
            bullyHadEnuf.stateToResponseMap = bullyHadEnufRespMap;
            dialogueList.Add(bullyHadEnuf);
        }

        //######## LEAF DIALOGUES END############

        {
            SDialogue bullyWhyBothering = new SDialogue();
            bullyWhyBothering.dialogueText = "What's with you and Anya's sister?";
            bullyWhyBothering.dialogueId = EDialogueID.BULLYWHATSWITHSIS;
            Dictionary<PlayerStates, Action> bullyWhyBotheringRespMap = new Dictionary<PlayerStates, Action>();
            bullyWhyBotheringRespMap[PlayerStates.WILLTALK2BULLY] = BullyWhatsWithSisAction;
            bullyWhyBothering.stateToResponseMap = bullyWhyBotheringRespMap;
            dialogueList.Add(bullyWhyBothering);
        }

        {
            SDialogue bullyPlsTellIfKnow = new SDialogue();
            bullyPlsTellIfKnow.dialogueText = "Could you please tell me if you know Anya�s sister?";
            bullyPlsTellIfKnow.dialogueId = EDialogueID.BULLYPLSTELLIFKNOW;
            Dictionary<PlayerStates, Action> bullyPlsTellIfKnowRespMap = new Dictionary<PlayerStates, Action>();
            bullyPlsTellIfKnowRespMap[PlayerStates.WILLTALK2BULLY] = BullyPlsTellIfKnowAction;
            bullyPlsTellIfKnow.stateToResponseMap = bullyPlsTellIfKnowRespMap;
            dialogueList.Add(bullyPlsTellIfKnow);
        }

        {
            SDialogue bullyWhyBullying = new SDialogue();
            bullyWhyBullying.dialogueText = "Why are you bullying her?";
            bullyWhyBullying.dialogueId = EDialogueID.BULLYWHYBULLYING;
            Dictionary<PlayerStates, Action> bullyWhyBullyingRespMap = new Dictionary<PlayerStates, Action>();
            bullyWhyBullyingRespMap[PlayerStates.WILLTALK2BULLY] = BullyWhyBullyingAction;
            bullyWhyBullying.stateToResponseMap = bullyWhyBullyingRespMap;
            dialogueList.Add(bullyWhyBullying);
        }

        {
            SDialogue bullySisUpsetWidU = new SDialogue();
            bullySisUpsetWidU.dialogueText = "You know, Anya�s sis...she�s just...a bit upset with you, that�s all.";
            bullySisUpsetWidU.dialogueId = EDialogueID.BULLYSISUPSETWIDU;
            Dictionary<PlayerStates, Action> bullySisUpsetWidURespMap = new Dictionary<PlayerStates, Action>();
            bullySisUpsetWidURespMap[PlayerStates.WILLTALK2BULLY] = BullySisUpsetWidUAction;
            bullySisUpsetWidU.stateToResponseMap = bullySisUpsetWidURespMap;
            dialogueList.Add(bullySisUpsetWidU);
        }

        {
            SDialogue bullyStopBotheringHer = new SDialogue();
            bullyStopBotheringHer.dialogueText = "Whatever, dude. Just stop bothering her, will you?";
            bullyStopBotheringHer.dialogueId = EDialogueID.BULLYSTOPBOTHERINGHER;
            Dictionary<PlayerStates, Action> bullyStopBotheringHerRespMap = new Dictionary<PlayerStates, Action>();
            bullyStopBotheringHerRespMap[PlayerStates.WILLTALK2BULLY] = BullyStopBotheringHerAction;
            bullyStopBotheringHer.stateToResponseMap = bullyStopBotheringHerRespMap;
            dialogueList.Add(bullyStopBotheringHer);
        }

        {
            SDialogue bullyGoodIfUStop = new SDialogue();
            bullyGoodIfUStop.dialogueText = "Uh, no...just...it would be good if you could stop messing with her, you know.";
            bullyGoodIfUStop.dialogueId = EDialogueID.BULLYGOODIFUSTOP;
            Dictionary<PlayerStates, Action> bullyGoodIfUStopRespMap = new Dictionary<PlayerStates, Action>();
            bullyGoodIfUStopRespMap[PlayerStates.WILLTALK2BULLY] = BullyGoodIfUStopAction;
            bullyGoodIfUStop.stateToResponseMap = bullyGoodIfUStopRespMap;
            dialogueList.Add(bullyGoodIfUStop);
        }

        {
            SDialogue bullyUAskedForIt = new SDialogue();
            bullyUAskedForIt.dialogueText = "Okay, big man, you asked for it. *gets ready to fight*";
            bullyUAskedForIt.dialogueId = EDialogueID.BULLYUASKEDFORIT;
            Dictionary<PlayerStates, Action> bullyUAskedForItRespMap = new Dictionary<PlayerStates, Action>();
            bullyUAskedForItRespMap[PlayerStates.WILLTALK2BULLY] = StartWABMinigame;
            bullyUAskedForIt.stateToResponseMap = bullyUAskedForItRespMap;
            dialogueList.Add(bullyUAskedForIt);
        }

        {
            SDialogue bullySorryImLeaving = new SDialogue();
            bullySorryImLeaving.dialogueText = "Woah, sorry mate. Jeez, no need for all that, I�m leaving.";
            bullySorryImLeaving.dialogueId = EDialogueID.BULLYSORRYIMLEAVING;
            Dictionary<PlayerStates, Action> bullySorryImLeavingRespMap = new Dictionary<PlayerStates, Action>();
            bullySorryImLeavingRespMap[PlayerStates.WILLTALK2BULLY] = BullySorryImLeavingAction;
            bullySorryImLeaving.stateToResponseMap = bullySorryImLeavingRespMap;
            dialogueList.Add(bullySorryImLeaving);
        }

        {
            SDialogue bullyBringItOn = new SDialogue();
            bullyBringItOn.dialogueText = "Yes. Bring it on!";
            bullyBringItOn.dialogueId = EDialogueID.BULLYBRINGITON;
            Dictionary<PlayerStates, Action> bullyBringItOnRespMap = new Dictionary<PlayerStates, Action>();
            bullyBringItOnRespMap[PlayerStates.BULLYFOUGHT] = StartWABMinigame;
            bullyBringItOn.stateToResponseMap = bullyBringItOnRespMap;
            dialogueList.Add(bullyBringItOn);
        }

        {
            SDialogue bullyPassingBy = new SDialogue();
            bullyPassingBy.dialogueText = "No, I was just passing by. Sorry!";
            bullyPassingBy.dialogueId = EDialogueID.BULLYPASSINGBY;
            Dictionary<PlayerStates, Action> bullyPassingByRespMap = new Dictionary<PlayerStates, Action>();
            bullyPassingByRespMap[PlayerStates.BULLYFOUGHT] = BullyPassingByAction;
            bullyPassingBy.stateToResponseMap = bullyPassingByRespMap;
            dialogueList.Add(bullyPassingBy);
        }
    }
}
