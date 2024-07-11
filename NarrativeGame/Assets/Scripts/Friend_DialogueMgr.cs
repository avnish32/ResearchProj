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
            "Um...hi?"
        };

        uiController.StartNPCDialogues(dialogueList, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRNEEDURHELP, EDialogueID.FRBORING2DAY });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndHiNeedHelpNeutralAction()
    {
        string[] dialogueList = {
            "Do I know you?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndDontUWorkAtLettingsNeutralAction()
    {
        string[] dialogueList = {
            "How do you know that? Have you been stalking me?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
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

    private void FrndNeedUrHelpNeutralAction()
    {
        string[] dialogueList = {
            "O...kay?"
        };

        uiController.StartNPCDialogues(dialogueList, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRCANUREFERME, EDialogueID.FRYRUHOSTILE});
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndCanUReferMeNeutralAction()
    {
        string[] dialogueList = {
            "Why do you need me to do that?",
            "Why would I do that?",
            "I barely even know you. How do you know where I work?",
            "Gosh, I have so many questions. You look like bad news, dude."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndYRUHostileNeutralAction()
    {
        string[] dialogueList = {
            "*scoffs* Is that even a question?",
            "I barely know you, and you’re asking me for help as if we’ve been friends for years.",
            "Get a grip on yourself."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndBoring2DayAction()
    {
        string[] dialogueList = {
            "*rolls eyes* Ohmygod, tell me about it. I almost fell off my chair snoozing in the class."
        };

        uiController.StartNPCDialogues(dialogueList, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRCANUNDERSTAND, EDialogueID.FRBITMUCH});
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndBitMuchAction()
    {
        string[] dialogueList = {
            "*narrows eyes* Excuse me, who are you to judge?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndCanUnderstandAction()
    {
        string[] dialogueList = {
            "I know, right?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndImJaredAction()
    {
        string[] dialogueList = {
            "Hi Jared, I’m Anya. Nice to meet you."
        };

        uiController.StartNPCDialogues(dialogueList, () =>
        {
            stateWPlayer = PlayerStates.ACQUAINTANCE;
            //TODO state change effects
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRCANUNDERSTAND, EDialogueID.FRBITMUCH });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);

        });
    }

    private void FrndWhat2SayNextAction()
    {
        string[] dialogueList = {
            "It’s all right, don’t sweat it. See you later!"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndCantWaitToNapAction()
    {
        string[] dialogueList = {
            "Lucky you. I couldn’t even if I wanted. I’ve got work after this."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndNowThatWeKnowAction()
    {
        string[] dialogueList = {
            "What? Is that why you were talking to me, just to get a favour?",
            "How selfish can you be?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndWhereDoUWorkAction()
    {
        string[] dialogueList = {
            "AB&C Lettings. Have you heard of them?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndAtLettingsRightAction()
    {
        string[] dialogueList = {
            "Wha-how did you know? Are you some kind of stalker or something?",
            "Jeez, what a creep."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndAppliedMyselfAction()
    {
        string[] dialogueList = {
            "Really? Have you heard back?"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndTheyBehindMyDadAction()
    {
        string[] dialogueList = {
            "Oh...okay, um...I’m sorry.",
            "I guess I should get going now. *turns away thinking you’re weird*"
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndItsBeenAWeekAction()
    {
        string[] dialogueList = {
            "That’s weird. They usually don’t take so long.",
            "Unless they have multiple candidates and are having a hard time deciding."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndIfOnlyAppHighlightedAction()
    {
        string[] dialogueList = {
            "Oh, hmm...I think I can help with that.",
            "I can put in a good word for you with the manager."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndUKnowALotAction()
    {
        string[] dialogueList = {
            "*shrugs* If you’re so cynical then I don’t know what to say to you."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndWudBHelpAction()
    {
        string[] dialogueList = {
            "There’s a problem though.",
            "There’s something bothering me, and I might forget to speak with the manager because of that."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndTookULongEnufAction()
    {
        string[] dialogueList = {
            "How rude! So this was what you were hoping I would say all along?",
            "Tryna steer the conversation, weren’t you?",
            "Forgect about it, Jared, I’m not talking to the manager for you."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndMaybeICanHelpAction()
    {
        string[] dialogueList = {
            "There’s this big bully who’s always picking on my sister.",
            "We’ve complained to the teachers about him but it’s no use.",
            "Do you think you could get him to stop harassing my sis? "
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void FrndWudITalk2UAction()
    {
        string[] dialogueList = {
            "Woah, easy. I was just trying to be friendly, jeez.",
            "Guess I hit a nerve there."
        };

        uiController.StartNPCDialogues(dialogueList, gameController.EnablePlayerMovement);
    }

    private void PopulateDialogueList()
    {
        {
            SDialogue frndHey = new SDialogue();
            frndHey.dialogueText = "Hey";
            frndHey.dialogueId = EDialogueID.FRHEY;
            frndHey.playerStates = new List<PlayerStates>
            {PlayerStates.NEUTRAL, PlayerStates.ACQUAINTANCE};
            Dictionary<PlayerStates, Action> frndHeyRespMap = new Dictionary<PlayerStates, Action>();
            frndHeyRespMap[PlayerStates.NEUTRAL] = FrndHeyNeutralAction;
            frndHeyRespMap[PlayerStates.ACQUAINTANCE] = FrndHeyAcqAction;
            frndHey.rshipToResponseMap = frndHeyRespMap;
            dialogueList.Add(frndHey);
        }

        {
            SDialogue frndHiNeedUrHelp = new SDialogue();
            frndHiNeedUrHelp.dialogueText = "Hi, I need your help.";
            frndHiNeedUrHelp.dialogueId = EDialogueID.FRHINEEDHELP;
            frndHiNeedUrHelp.playerStates = new List<PlayerStates>{PlayerStates.NEUTRAL};
            Dictionary<PlayerStates, Action> frndHiNeedUrHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndHiNeedUrHelpRespMap[PlayerStates.NEUTRAL] = FrndHiNeedHelpNeutralAction;
            frndHiNeedUrHelp.rshipToResponseMap = frndHiNeedUrHelpRespMap;
            dialogueList.Add(frndHiNeedUrHelp);
        }

        {
            SDialogue frndDontUWorkAtLettings = new SDialogue();
            frndDontUWorkAtLettings.dialogueText = "You work at the AB&C Lettings, don’t you?";
            frndDontUWorkAtLettings.dialogueId = EDialogueID.FRDONTUWORKATLETTINGS;
            frndDontUWorkAtLettings.playerStates = new List<PlayerStates> { PlayerStates.NEUTRAL };
            Dictionary<PlayerStates, Action> frndDontUWorkAtLettingsRespMap = new Dictionary<PlayerStates, Action>();
            frndDontUWorkAtLettingsRespMap[PlayerStates.NEUTRAL] = FrndDontUWorkAtLettingsNeutralAction;
            frndDontUWorkAtLettings.rshipToResponseMap = frndDontUWorkAtLettingsRespMap;
            dialogueList.Add(frndDontUWorkAtLettings);
        }

        {
            SDialogue frndNeedUrHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndNeedUrHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndNeedUrHelp.dialogueText = "I need your help.";
            frndNeedUrHelp.dialogueId = EDialogueID.FRNEEDURHELP;
            frndNeedUrHelpRespMap[PlayerStates.NEUTRAL] = FrndNeedUrHelpNeutralAction;
            frndNeedUrHelp.rshipToResponseMap = frndNeedUrHelpRespMap;
            dialogueList.Add(frndNeedUrHelp);
        }

        {
            SDialogue frndCanUReferMe = new SDialogue();
            Dictionary<PlayerStates, Action> frndCanUReferMeRespMap = new Dictionary<PlayerStates, Action>();
            frndCanUReferMe.dialogueText = "Can you put in a reference for me where you work?";
            frndCanUReferMe.dialogueId = EDialogueID.FRCANUREFERME;
            frndCanUReferMeRespMap[PlayerStates.NEUTRAL] = FrndCanUReferMeNeutralAction;
            frndCanUReferMe.rshipToResponseMap = frndCanUReferMeRespMap;
            dialogueList.Add(frndCanUReferMe);
        }

        {
            SDialogue frndYRUHostile = new SDialogue();
            Dictionary<PlayerStates, Action> frndYRUHostileRespMap = new Dictionary<PlayerStates, Action>();
            frndYRUHostile.dialogueText = "Why are you so hostile to me?";
            frndYRUHostile.dialogueId = EDialogueID.FRYRUHOSTILE;
            frndYRUHostileRespMap[PlayerStates.NEUTRAL] = FrndYRUHostileNeutralAction;
            frndYRUHostile.rshipToResponseMap = frndYRUHostileRespMap;
            dialogueList.Add(frndYRUHostile);
        }

        {
            SDialogue frndBoring2Day = new SDialogue();
            Dictionary<PlayerStates, Action> frndBoring2DayRespMap = new Dictionary<PlayerStates, Action>();
            frndBoring2Day.dialogueText = "Bit boring today, isn’t it?";
            frndBoring2Day.dialogueId = EDialogueID.FRBORING2DAY;
            frndBoring2DayRespMap[PlayerStates.NEUTRAL] = FrndBoring2DayAction;
            frndBoring2Day.rshipToResponseMap = frndBoring2DayRespMap;
            dialogueList.Add(frndBoring2Day);
        }

        {
            SDialogue frndBitMuch = new SDialogue();
            Dictionary<PlayerStates, Action> frndBitMuchRespMap = new Dictionary<PlayerStates, Action>();
            frndBitMuch.dialogueText = "That’s a bit much, don’t you think?";
            frndBitMuch.dialogueId = EDialogueID.FRBITMUCH;
            frndBitMuchRespMap[PlayerStates.NEUTRAL] = FrndBitMuchAction;
            frndBitMuch.rshipToResponseMap = frndBitMuchRespMap;
            dialogueList.Add(frndBitMuch);
        }

        {
            SDialogue frndCanUnderstand = new SDialogue();
            Dictionary<PlayerStates, Action> frndCanUnderstandRespMap = new Dictionary<PlayerStates, Action>();
            frndCanUnderstand.dialogueText = "Hahaha, I can understand. These back-to-back lectures are always tiring.";
            frndCanUnderstand.dialogueId = EDialogueID.FRCANUNDERSTAND;
            frndCanUnderstandRespMap[PlayerStates.NEUTRAL] = FrndCanUnderstandAction;
            frndCanUnderstand.rshipToResponseMap = frndCanUnderstandRespMap;
            dialogueList.Add(frndCanUnderstand);
        }

        {
            SDialogue frndImJared = new SDialogue();
            Dictionary<PlayerStates, Action> frndImJaredRespMap = new Dictionary<PlayerStates, Action>();
            frndImJared.dialogueText = "I’m Jared.";
            frndImJared.dialogueId = EDialogueID.FRIMJARED;
            frndImJaredRespMap[PlayerStates.NEUTRAL] = FrndImJaredAction;
            frndImJared.rshipToResponseMap = frndImJaredRespMap;
            dialogueList.Add(frndImJared);
        }

        {
            SDialogue frndWhat2SayNext = new SDialogue();
            Dictionary<PlayerStates, Action> frndWhat2SayNextRespMap = new Dictionary<PlayerStates, Action>();
            frndWhat2SayNext.dialogueText = "Hmm...don’t know what to say next, hehe.";
            frndWhat2SayNext.dialogueId = EDialogueID.FRWHAT2SAYNEXT;
            frndWhat2SayNextRespMap[PlayerStates.NEUTRAL] = FrndWhat2SayNextAction;
            frndWhat2SayNext.rshipToResponseMap = frndWhat2SayNextRespMap;
            dialogueList.Add(frndWhat2SayNext);
        }

        {
            SDialogue frndCantWaitToNap = new SDialogue();
            Dictionary<PlayerStates, Action> frndCantWaitToNapRespMap = new Dictionary<PlayerStates, Action>();
            frndCantWaitToNap.dialogueText = "And you, Anya. I bet you can’t wait to go home and take a looong nap. I know I will.";
            frndCantWaitToNap.dialogueId = EDialogueID.FRCANTWAITTONAP;
            frndCantWaitToNapRespMap[PlayerStates.ACQUAINTANCE] = FrndCantWaitToNapAction;
            frndCantWaitToNap.rshipToResponseMap = frndCantWaitToNapRespMap;
            dialogueList.Add(frndCantWaitToNap);
        }

        {
            SDialogue frndNowThatWeKnow = new SDialogue();
            Dictionary<PlayerStates, Action> frndNowThatWeKnowRespMap = new Dictionary<PlayerStates, Action>();
            frndNowThatWeKnow.dialogueText = "Anya. Cool. Now that you know me, can I ask you a favour?";
            frndNowThatWeKnow.dialogueId = EDialogueID.FRNOWTHATWEKNOW;
            frndNowThatWeKnowRespMap[PlayerStates.ACQUAINTANCE] = FrndNowThatWeKnowAction;
            frndNowThatWeKnow.rshipToResponseMap = frndNowThatWeKnowRespMap;
            dialogueList.Add(frndNowThatWeKnow);
        }

        {
            SDialogue frndWhereDoUWork = new SDialogue();
            Dictionary<PlayerStates, Action> frndWhereDoUWorkRespMap = new Dictionary<PlayerStates, Action>();
            frndWhereDoUWork.dialogueText = "Ah, you work part-time? Where exactly?";
            frndWhereDoUWork.dialogueId = EDialogueID.FRWHEREDOUWORK;
            frndWhereDoUWorkRespMap[PlayerStates.ACQUAINTANCE] = FrndWhereDoUWorkAction;
            frndWhereDoUWork.rshipToResponseMap = frndWhereDoUWorkRespMap;
            dialogueList.Add(frndWhereDoUWork);
        }

        {
            SDialogue frndAtLettingsRight = new SDialogue();
            Dictionary<PlayerStates, Action> frndAtLettingsRightRespMap = new Dictionary<PlayerStates, Action>();
            frndAtLettingsRight.dialogueText = "At AB&C Lettings, right?";
            frndAtLettingsRight.dialogueId = EDialogueID.FRATLETTINGSRIGHT;
            frndAtLettingsRightRespMap[PlayerStates.ACQUAINTANCE] = FrndAtLettingsRightAction;
            frndAtLettingsRight.rshipToResponseMap = frndAtLettingsRightRespMap;
            dialogueList.Add(frndAtLettingsRight);
        }

        {
            SDialogue frndAppliedMyself = new SDialogue();
            Dictionary<PlayerStates, Action> frndAppliedMyselfRespMap = new Dictionary<PlayerStates, Action>();
            frndAppliedMyself.dialogueText = "Funny you should ask. I’ve actually applied for one of their part-time roles myself.";
            frndAppliedMyself.dialogueId = EDialogueID.FRAPPLIEDMYSELF;
            frndAppliedMyselfRespMap[PlayerStates.ACQUAINTANCE] = FrndAppliedMyselfAction;
            frndAppliedMyself.rshipToResponseMap = frndAppliedMyselfRespMap;
            dialogueList.Add(frndAppliedMyself);
        }

        {
            SDialogue frndTheyBehindMyDad = new SDialogue();
            Dictionary<PlayerStates, Action> frndTheyBehindMyDadRespMap = new Dictionary<PlayerStates, Action>();
            frndTheyBehindMyDad.dialogueText = "Heard of them? They’re the ones responsible for my father’s disappearance! ";
            frndTheyBehindMyDad.dialogueId = EDialogueID.FRTHEYBEHINDMYDAD;
            frndTheyBehindMyDadRespMap[PlayerStates.ACQUAINTANCE] = FrndTheyBehindMyDadAction;
            frndTheyBehindMyDad.rshipToResponseMap = frndTheyBehindMyDadRespMap;
            dialogueList.Add(frndTheyBehindMyDad);
        }

        {
            SDialogue frndItsBeenAWeek = new SDialogue();
            Dictionary<PlayerStates, Action> frndItsBeenAWeekRespMap = new Dictionary<PlayerStates, Action>();
            frndItsBeenAWeek.dialogueText = "Nah. It’s been, like, a week.";
            frndItsBeenAWeek.dialogueId = EDialogueID.FRITSBEENAWEEK;
            frndItsBeenAWeekRespMap[PlayerStates.ACQUAINTANCE] = FrndItsBeenAWeekAction;
            frndItsBeenAWeek.rshipToResponseMap = frndItsBeenAWeekRespMap;
            dialogueList.Add(frndItsBeenAWeek);
        }

        {
            SDialogue frndWudITalk2U = new SDialogue();
            Dictionary<PlayerStates, Action> frndWudITalk2URespMap = new Dictionary<PlayerStates, Action>();
            frndWudITalk2U.dialogueText = "Would I be talking to you right now if I had?";
            frndWudITalk2U.dialogueId = EDialogueID.FRWUDITALK2U;
            frndWudITalk2URespMap[PlayerStates.ACQUAINTANCE] = FrndWudITalk2UAction;
            frndWudITalk2U.rshipToResponseMap = frndWudITalk2URespMap;
            dialogueList.Add(frndWudITalk2U);
        }

        {
            SDialogue frndIfAppHighlighted = new SDialogue();
            Dictionary<PlayerStates, Action> frndIfAppHighlightedRespMap = new Dictionary<PlayerStates, Action>();
            frndIfAppHighlighted.dialogueText = "Yeah, that makes sense. If only there was some way to highlight my application...";
            frndIfAppHighlighted.dialogueId = EDialogueID.FRAPPHIGHLIGHT;
            frndIfAppHighlightedRespMap[PlayerStates.ACQUAINTANCE] = FrndIfOnlyAppHighlightedAction;
            frndIfAppHighlighted.rshipToResponseMap = frndIfAppHighlightedRespMap;
            dialogueList.Add(frndIfAppHighlighted);
        }

        {
            SDialogue frndUKnowALot = new SDialogue();
            Dictionary<PlayerStates, Action> frndUKnowALotRespMap = new Dictionary<PlayerStates, Action>();
            frndUKnowALot.dialogueText = "You seem to know an awful lot about their hiring process for someone who just works part-time.";
            frndUKnowALot.dialogueId = EDialogueID.FRUKNOWALOT;
            frndUKnowALotRespMap[PlayerStates.ACQUAINTANCE] = FrndUKnowALotAction;
            frndUKnowALot.rshipToResponseMap = frndUKnowALotRespMap;
            dialogueList.Add(frndUKnowALot);
        }

        {
            SDialogue frndWudBHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndWudBHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndWudBHelp.dialogueText = "Thanks, that would be a huge help!";
            frndWudBHelp.dialogueId = EDialogueID.FRWUDBHELP;
            frndWudBHelpRespMap[PlayerStates.ACQUAINTANCE] = FrndWudBHelpAction;
            frndWudBHelp.rshipToResponseMap = frndWudBHelpRespMap;
            dialogueList.Add(frndWudBHelp);
        }

        {
            SDialogue frndTookULongEnuf = new SDialogue();
            Dictionary<PlayerStates, Action> frndTookULongEnufRespMap = new Dictionary<PlayerStates, Action>();
            frndTookULongEnuf.dialogueText = "*sighs in relief* Now we’re talking. Ah, finally...took you long enough.";
            frndTookULongEnuf.dialogueId = EDialogueID.FRTOOKULONGENUF;
            frndTookULongEnufRespMap[PlayerStates.ACQUAINTANCE] = FrndTookULongEnufAction;
            frndTookULongEnuf.rshipToResponseMap = frndTookULongEnufRespMap;
            dialogueList.Add(frndTookULongEnuf);
        }

        {
            SDialogue frndMaybICanHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndMaybICanHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndMaybICanHelp.dialogueText = "Oh. What is it? Maybe I can help.";
            frndMaybICanHelp.dialogueId = EDialogueID.FRMAYBEICANHELP;
            frndMaybICanHelpRespMap[PlayerStates.ACQUAINTANCE] = FrndMaybeICanHelpAction;
            frndMaybICanHelp.rshipToResponseMap = frndMaybICanHelpRespMap;
            dialogueList.Add(frndMaybICanHelp);
        }


    }

}
