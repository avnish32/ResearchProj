using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Friend_DialogueMgr : DialogueManager
{
    [SerializeField]
    Bully_DialogueMgr bully;

    // ### voices
    [SerializeField]
    private AudioClip uhHiVoice, huhVoice, hiFriendlyVoice, 
        yepVoice, tyVoice, wowVoice, hmmVoice, doIKnowUVoice,
        howDoUKnowVoice, almostVoice, reallyVoice, okVoice,
        whyVoice, evenAQuestionVoice, omgVoice, excuseMeVoice,
        ikrVoice, alrightVoice, luckyYouVoice, whatVoice, ohVoice,
        weirdVoice, theresAProblemVoice, howRudeVoice, didUJustSayVoice,
        suitUrselfVoice, urLyingVoice;
    // #### voices end

    private const ECharacters FRIEND_CHAR = ECharacters.FRIEND;

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

    private void LoadOfficeScene()
    {
        SceneManager.LoadScene("L2_Office");
    }

    private void FrndHeyNeutralAction()
    {
        string[] dialogueList = {
            "Uh...hi?"
        };

        audioController.PlaySound(uhHiVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR , () =>
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

        audioController.PlaySound(doIKnowUVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndDontUWorkAtLettingsNeutralAction()
    {
        string[] dialogueList = {
            "How do you know that? Have you been stalking me?"
        };

        audioController.PlaySound(howDoUKnowVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndDontUWorkAtLettingsJobRvldAction()
    {
        string[] dialogueList = {
            "Yeah. How about you, are you working anywhere?"
        };

        audioController.PlaySound(yepVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRAPPLIEDMYSELF, EDialogueID.FRIDONTWORK });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndHeyAcqAction()
    {
        //This is when they are acquaintances i.e. bully has not been mentioned by Frnd.
        string[] dialogueList = {
            "Hi Jared, nice to run into you again."
        };

        audioController.PlaySound(hiFriendlyVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRCANTWAITTONAP, EDialogueID.FRNOWTHATWEKNOW });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndCantBelieveUFellAction()
    {
        string[] dialogueList = {
            "*Almost* fell off my chair.",
            "You know what, I’m not telling you anything else if you can't stop making fun of it."
        };

        audioController.PlaySound(almostVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndWantMe2Talk2RogerAction()
    {
        string[] dialogueList = {
            "Yep, think you can manage?"
        };

        audioController.PlaySound(yepVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRWILLTALK2BULLY, EDialogueID.FRWONTTALK2BULLY });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndIHandledITBullyMentionedAction()
    {
        if (bully.GetStateWPlayer() == PlayerStates.BULLYDEFEATED)
        {
            string[] dialogueList = { 
                "Oh, thank you so much! I can rest easy now.",
                "Don’t worry about your application, I’ll make sure to talk to the manager. See ya!"
            };

            audioController.PlaySound(tyVoice);
            uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
            {
                if (audioController.IsGameJuicy())
                {
                    uiController.FadeToBlack(LoadOfficeScene);
                } else
                {
                    LoadOfficeScene();
                }
                
            });
        } else
        {
            string[] dialogueList = { "Really?"};

            audioController.PlaySound(reallyVoice);
            uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
            {
                var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRLYINGABTBULLY, EDialogueID.FRYESREALLYHANDLEDBULLY });
                uiController.DisplayPlayerDialoguePanel(playerDialogueList);
            });
        }
    }

    private void FrndStupidBullyMentionedAction()
    {

        if (bully.GetStateWPlayer() == PlayerStates.BULLYDEFEATED)
        {
            string[] dialogueList = {
                "Wow, I didn’t know you were so full of yourself.",
                "I don’t think I’ll be recommending someone like you to my manager.",
                "Good luck with your application, Jared."
            };

            audioController.PlaySound(wowVoice);
            uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
        }
        else
        {
            string[] dialogueList = { "Really?" };

            audioController.PlaySound(reallyVoice);
            uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
            {
                var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.FRLYINGABTBULLY, EDialogueID.FRYESREALLYHANDLEDBULLY });
                uiController.DisplayPlayerDialoguePanel(playerDialogueList);
            });
        }
    }

    private void FrndTrynaFigureOutAction()
    {
        string[] dialogueList = { 
            "I’m sure you’ll figure something out.",
            "Exit the classroom and go to the other end of the corridor.",
            "You'll see the playground. That's where he hangs out mostly.",
            "Just remember to be confident and assertive with him. There's no use being polite."
        };
        audioController.PlaySound(hmmVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndNeedUrHelpNeutralAction()
    {
        string[] dialogueList = {
            "O...kay?"
        };

        audioController.PlaySound(okVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> 
            { EDialogueID.FRCANUREFERME, EDialogueID.FRYRUHOSTILE});
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

        audioController.PlaySound(whyVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndYRUHostileNeutralAction()
    {
        string[] dialogueList = {
            "*scoffs* Is that even a question?",
            "I barely know you, and you’re asking me for help as if we’ve been friends for years.",
            "Get a grip on yourself."
        };

        audioController.PlaySound(evenAQuestionVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndBoring2DayAction()
    {
        string[] dialogueList = {
            "*rolls eyes* Ohmygod, tell me about it. I almost fell off my chair snoozing in the class."
        };

        audioController.PlaySound(omgVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> 
            { EDialogueID.FRCANUNDERSTAND, EDialogueID.FRBITMUCH});
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndBitMuchAction()
    {
        string[] dialogueList = {
            "*narrows eyes* Excuse me, who are you to judge?"
        };

        audioController.PlaySound(excuseMeVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndCanUnderstandAction()
    {
        string[] dialogueList = {
            "I know, right?"
        };

        audioController.PlaySound(ikrVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> 
            { EDialogueID.FRIMJARED, EDialogueID.FRWHAT2SAYNEXT});
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndImJaredAction()
    {
        string[] dialogueList = {
            "Hi Jared, I’m Anya. Nice to meet you."
        };

        audioController.PlaySound(hiFriendlyVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            stateWPlayer = PlayerStates.ACQUAINTANCE;
            //TODO state change effects
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> 
            { EDialogueID.FRCANTWAITTONAP, EDialogueID.FRNOWTHATWEKNOW });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);

        });
    }

    private void FrndWhat2SayNextAction()
    {
        string[] dialogueList = {
            "It’s all right, don’t sweat it."
        };

        audioController.PlaySound(alrightVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndCantWaitToNapAction()
    {
        string[] dialogueList = {
            "Lucky you. I couldn’t even if I wanted. I’ve got work after this."
        };

        audioController.PlaySound(luckyYouVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRATLETTINGSRIGHT, EDialogueID.FRWHEREDOUWORK });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndNowThatWeKnowAction()
    {
        string[] dialogueList = {
            "What? Is that why you were talking to me, just to get a favour?",
            "How selfish can you be?"
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndWhereDoUWorkAction()
    {
        string[] dialogueList = {
            "AB&C Lettings. Have you heard of them?"
        };

        stateWPlayer = PlayerStates.FRNDJOBREVEALED;

        audioController.PlaySound(hmmVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRTHEYBEHINDMYDAD, EDialogueID.FRAPPLIEDMYSELF });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndAtLettingsRightAction()
    {
        string[] dialogueList = {
            "Wha-how did you know? Are you some kind of stalker or something?",
            "Jeez, what a creep."
        };

        audioController.PlaySound(howDoUKnowVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndAppliedMyselfAction()
    {
        string[] dialogueList = {
            "Really? Have you heard back?"
        };

        audioController.PlaySound(reallyVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRITSBEENAWEEK, EDialogueID.FRWUDITALK2U });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndIDontWorkAction()
    {
        string[] dialogueList = {
            "Oh, okay...that was quite honest."
        };

        audioController.PlaySound(okVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndTheyBehindMyDadAction()
    {
        string[] dialogueList = {
            "Oh...okay, um...I’m sorry.",
            "I guess I should get going now. *turns away thinking you’re weird*"
        };

        audioController.PlaySound(ohVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndItsBeenAWeekAction()
    {
        string[] dialogueList = {
            "That’s weird. They usually don’t take so long.",
            "Unless they have multiple candidates and are having a hard time deciding."
        };

        audioController.PlaySound(weirdVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRUKNOWALOT, EDialogueID.FRAPPHIGHLIGHT });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndWudITalk2UAction()
    {
        string[] dialogueList = {
            "Woah, easy. I was just trying to be friendly, jeez.",
            "Guess I hit a nerve there."
        };

        audioController.PlaySound(wowVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndIfOnlyAppHighlightedAction()
    {
        string[] dialogueList = {
            "Oh, hmm...I think I can help with that.",
            "I can put in a good word for you with the manager."
        };

        audioController.PlaySound(hmmVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRWUDBHELP, EDialogueID.FRTOOKULONGENUF });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndUKnowALotAction()
    {
        string[] dialogueList = {
            "*shrugs* If you’re so cynical then I don’t know what to say to you."
        };

        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndWudBHelpAction()
    {
        string[] dialogueList = {
            "There’s a problem though.",
            "There’s something bothering me, and I might forget to speak with the manager because of that."
        };

        audioController.PlaySound(theresAProblemVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRONETRACKMIND, EDialogueID.FRMAYBEICANHELP });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndTookULongEnufAction()
    {
        string[] dialogueList = {
            "How rude! So this was what you were hoping I would say all along?",
            "Tryna steer the conversation, weren’t you?",
            "Forget about it, Jared, I’m not talking to the manager for you."
        };

        audioController.PlaySound(howRudeVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndMaybeICanHelpAction()
    {
        string[] dialogueList = {
            "There’s this big bully, Roger, who’s always picking on my sister.",
            "We’ve complained to the teachers about him but it’s no use.",
            "Do you think you could get him to stop harassing my sis?"
        };
        stateWPlayer = PlayerStates.FRNDBULLYMENTIONED;

        uiController.StartDialogues(dialogueList, FRIEND_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.FRWILLTALK2BULLY, EDialogueID.FRWONTTALK2BULLY });

            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void FrndOneTrackMindAction()
    {
        string[] dialogueList = {
            "*gapes in surprise* Did you just say that?",
            "If you’re such a hotshot multi-tasker, why not just go and brag about it at AB&C yourself, huh?"
        };

        audioController.PlaySound(didUJustSayVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndWillTalk2BullyAction()
    {
        stateWPlayer = PlayerStates.WILLTALK2BULLY;
        bully.SetStateWPlayer(PlayerStates.WILLTALK2BULLY);
        OnDialogueEnd();
    }

    private void FrndWontTalk2BullyAction()
    {
        string[] dialogueList = {
            "Suit yourself."
        };

        audioController.PlaySound(suitUrselfVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void FrndLyingAbtBullyAction()
    {
        OnDialogueEnd();
    }

    private void FrndYesReallyHandledBullyAction()
    {
        string[] dialogueList = {
            "I know you’re lying; I can tell what you're trying to do.",
            "I cannot recommend a fraud like you to my manager."
        };

        audioController.PlaySound(urLyingVoice);
        uiController.StartDialogues(dialogueList, FRIEND_CHAR, OnDialogueEnd);
    }

    private void OnDialogueEnd()
    {
        gameController.EnablePlayerMovement();
        /*if (npcMovement != null)
        {
            npcMovement.StartWalking();
        }*/
        //Above commented; NPC will resume walking only when player exits collider.
    }

    private void PopulateDialogueList()
    {
        //######## LEAF DIALOGUES ############
        {
            SDialogue frndHey = new SDialogue();
            frndHey.dialogueText = "Hey";
            frndHey.dialogueId = EDialogueID.FRHEY;
            frndHey.playerStates = new List<PlayerStates>
            {PlayerStates.NEUTRAL, PlayerStates.ACQUAINTANCE};
            Dictionary<PlayerStates, Action> frndHeyRespMap = new Dictionary<PlayerStates, Action>();
            frndHeyRespMap[PlayerStates.NEUTRAL] = FrndHeyNeutralAction;
            frndHeyRespMap[PlayerStates.ACQUAINTANCE] = FrndHeyAcqAction;
            frndHey.stateToResponseMap = frndHeyRespMap;
            dialogueList.Add(frndHey);
        }

        {
            SDialogue frndHiNeedUrHelp = new SDialogue();
            frndHiNeedUrHelp.dialogueText = "Hi, I need your help.";
            frndHiNeedUrHelp.dialogueId = EDialogueID.FRHINEEDHELP;
            frndHiNeedUrHelp.playerStates = new List<PlayerStates>{PlayerStates.NEUTRAL};
            Dictionary<PlayerStates, Action> frndHiNeedUrHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndHiNeedUrHelpRespMap[PlayerStates.NEUTRAL] = FrndHiNeedHelpNeutralAction;
            frndHiNeedUrHelp.stateToResponseMap = frndHiNeedUrHelpRespMap;
            dialogueList.Add(frndHiNeedUrHelp);
        }

        {
            SDialogue frndDontUWorkAtLettings = new SDialogue();
            frndDontUWorkAtLettings.dialogueText = "You work at the AB&C Lettings, don’t you?";
            frndDontUWorkAtLettings.dialogueId = EDialogueID.FRDONTUWORKATLETTINGS;
            frndDontUWorkAtLettings.playerStates = new List<PlayerStates> { PlayerStates.NEUTRAL, PlayerStates.FRNDJOBREVEALED };
            Dictionary<PlayerStates, Action> frndDontUWorkAtLettingsRespMap = new Dictionary<PlayerStates, Action>();
            frndDontUWorkAtLettingsRespMap[PlayerStates.NEUTRAL] = FrndDontUWorkAtLettingsNeutralAction;
            frndDontUWorkAtLettingsRespMap[PlayerStates.FRNDJOBREVEALED] = FrndDontUWorkAtLettingsJobRvldAction;
            frndDontUWorkAtLettings.stateToResponseMap = frndDontUWorkAtLettingsRespMap;
            dialogueList.Add(frndDontUWorkAtLettings);
        }

        {
            SDialogue frndCantBelieveUFell = new SDialogue();
            frndCantBelieveUFell.dialogueText = "Can’t believe you fell off your chair!";
            frndCantBelieveUFell.dialogueId = EDialogueID.FRCANTBELIEVEUFELL;
            frndCantBelieveUFell.playerStates = new List<PlayerStates> 
            { PlayerStates.ACQUAINTANCE, PlayerStates.FRNDJOBREVEALED, PlayerStates.FRNDBULLYMENTIONED };
            Dictionary<PlayerStates, Action> frndCantBelieveUFellRespMap = new Dictionary<PlayerStates, Action>();
            frndCantBelieveUFellRespMap[PlayerStates.ACQUAINTANCE] = FrndCantBelieveUFellAction;
            frndCantBelieveUFellRespMap[PlayerStates.FRNDJOBREVEALED] = FrndCantBelieveUFellAction;
            frndCantBelieveUFellRespMap[PlayerStates.FRNDBULLYMENTIONED] = FrndCantBelieveUFellAction;
            frndCantBelieveUFell.stateToResponseMap = frndCantBelieveUFellRespMap;
            dialogueList.Add(frndCantBelieveUFell);
        }

        {
            SDialogue frndStillTalk2Bully = new SDialogue();
            frndStillTalk2Bully.dialogueText = "Still want me to talk to Roger?";
            frndStillTalk2Bully.dialogueId = EDialogueID.FRSTILLTALK2ROGER;
            frndStillTalk2Bully.playerStates = new List<PlayerStates> { PlayerStates.FRNDBULLYMENTIONED };
            Dictionary<PlayerStates, Action> frndStillTalk2BullyRespMap = new Dictionary<PlayerStates, Action>();
            frndStillTalk2BullyRespMap[PlayerStates.FRNDBULLYMENTIONED] = FrndWantMe2Talk2RogerAction;
            frndStillTalk2Bully.stateToResponseMap = frndStillTalk2BullyRespMap;
            dialogueList.Add(frndStillTalk2Bully);
        }

        {
            SDialogue frndIHandledIt = new SDialogue();
            frndIHandledIt.dialogueText = "I handled it; he won’t bother your sister anymore.";
            frndIHandledIt.dialogueId = EDialogueID.FRIHANDLEDIT;
            frndIHandledIt.playerStates = new List<PlayerStates> { PlayerStates.WILLTALK2BULLY };
            Dictionary<PlayerStates, Action> frndIHandledItRespMap = new Dictionary<PlayerStates, Action>();
            frndIHandledItRespMap[PlayerStates.WILLTALK2BULLY] = FrndIHandledITBullyMentionedAction;
            frndIHandledIt.stateToResponseMap = frndIHandledItRespMap;
            dialogueList.Add(frndIHandledIt);
        }

        {
            SDialogue frndStupidBully = new SDialogue();
            frndStupidBully.dialogueText = "That stupid bully was no match for me, ha! I made mincemeat out of him!";
            frndStupidBully.dialogueId = EDialogueID.FRSTUPIDBULLY;
            frndStupidBully.playerStates = new List<PlayerStates> { PlayerStates.WILLTALK2BULLY };
            Dictionary<PlayerStates, Action> frndIHandledItRespMap = new Dictionary<PlayerStates, Action>();
            frndIHandledItRespMap[PlayerStates.WILLTALK2BULLY] = FrndStupidBullyMentionedAction;
            frndStupidBully.stateToResponseMap = frndIHandledItRespMap;
            dialogueList.Add(frndStupidBully);
        }

        {
            SDialogue frndTrynaFigureOut = new SDialogue();
            frndTrynaFigureOut.dialogueText = "Trying to figure out how to deal with this Roger guy.";
            frndTrynaFigureOut.dialogueId = EDialogueID.FRTRYNAFIGUREOUT;
            frndTrynaFigureOut.playerStates = new List<PlayerStates> { PlayerStates.WILLTALK2BULLY };
            Dictionary<PlayerStates, Action> frndTrynaFigureOutRespMap = new Dictionary<PlayerStates, Action>();
            frndTrynaFigureOutRespMap[PlayerStates.WILLTALK2BULLY] = FrndTrynaFigureOutAction;
            frndTrynaFigureOut.stateToResponseMap = frndTrynaFigureOutRespMap;
            dialogueList.Add(frndTrynaFigureOut);
        }

        //######## LEAF DIALOGUES END ############

        {
            SDialogue frndNeedUrHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndNeedUrHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndNeedUrHelp.dialogueText = "I need your help.";
            frndNeedUrHelp.dialogueId = EDialogueID.FRNEEDURHELP;
            frndNeedUrHelpRespMap[PlayerStates.NEUTRAL] = FrndNeedUrHelpNeutralAction;
            frndNeedUrHelp.stateToResponseMap = frndNeedUrHelpRespMap;
            dialogueList.Add(frndNeedUrHelp);
        }

        {
            SDialogue frndCanUReferMe = new SDialogue();
            Dictionary<PlayerStates, Action> frndCanUReferMeRespMap = new Dictionary<PlayerStates, Action>();
            frndCanUReferMe.dialogueText = "Can you put in a reference for me where you work?";
            frndCanUReferMe.dialogueId = EDialogueID.FRCANUREFERME;
            frndCanUReferMeRespMap[PlayerStates.NEUTRAL] = FrndCanUReferMeNeutralAction;
            frndCanUReferMe.stateToResponseMap = frndCanUReferMeRespMap;
            dialogueList.Add(frndCanUReferMe);
        }

        {
            SDialogue frndYRUHostile = new SDialogue();
            Dictionary<PlayerStates, Action> frndYRUHostileRespMap = new Dictionary<PlayerStates, Action>();
            frndYRUHostile.dialogueText = "Why are you so hostile to me?";
            frndYRUHostile.dialogueId = EDialogueID.FRYRUHOSTILE;
            frndYRUHostileRespMap[PlayerStates.NEUTRAL] = FrndYRUHostileNeutralAction;
            frndYRUHostile.stateToResponseMap = frndYRUHostileRespMap;
            dialogueList.Add(frndYRUHostile);
        }

        {
            SDialogue frndBoring2Day = new SDialogue();
            Dictionary<PlayerStates, Action> frndBoring2DayRespMap = new Dictionary<PlayerStates, Action>();
            frndBoring2Day.dialogueText = "Bit boring today, isn’t it?";
            frndBoring2Day.dialogueId = EDialogueID.FRBORING2DAY;
            frndBoring2DayRespMap[PlayerStates.NEUTRAL] = FrndBoring2DayAction;
            frndBoring2Day.stateToResponseMap = frndBoring2DayRespMap;
            dialogueList.Add(frndBoring2Day);
        }

        {
            SDialogue frndBitMuch = new SDialogue();
            Dictionary<PlayerStates, Action> frndBitMuchRespMap = new Dictionary<PlayerStates, Action>();
            frndBitMuch.dialogueText = "That’s a bit much, don’t you think?";
            frndBitMuch.dialogueId = EDialogueID.FRBITMUCH;
            frndBitMuchRespMap[PlayerStates.NEUTRAL] = FrndBitMuchAction;
            frndBitMuch.stateToResponseMap = frndBitMuchRespMap;
            dialogueList.Add(frndBitMuch);
        }

        {
            SDialogue frndCanUnderstand = new SDialogue();
            Dictionary<PlayerStates, Action> frndCanUnderstandRespMap = new Dictionary<PlayerStates, Action>();
            frndCanUnderstand.dialogueText = "Hahaha, I can understand. These back-to-back lectures are always tiring.";
            frndCanUnderstand.dialogueId = EDialogueID.FRCANUNDERSTAND;
            frndCanUnderstandRespMap[PlayerStates.NEUTRAL] = FrndCanUnderstandAction;
            frndCanUnderstand.stateToResponseMap = frndCanUnderstandRespMap;
            dialogueList.Add(frndCanUnderstand);
        }

        {
            SDialogue frndImJared = new SDialogue();
            Dictionary<PlayerStates, Action> frndImJaredRespMap = new Dictionary<PlayerStates, Action>();
            frndImJared.dialogueText = "I’m Jared.";
            frndImJared.dialogueId = EDialogueID.FRIMJARED;
            frndImJaredRespMap[PlayerStates.NEUTRAL] = FrndImJaredAction;
            frndImJared.stateToResponseMap = frndImJaredRespMap;
            dialogueList.Add(frndImJared);
        }

        {
            SDialogue frndWhat2SayNext = new SDialogue();
            Dictionary<PlayerStates, Action> frndWhat2SayNextRespMap = new Dictionary<PlayerStates, Action>();
            frndWhat2SayNext.dialogueText = "Hmm...don’t know what to say next, hehe.";
            frndWhat2SayNext.dialogueId = EDialogueID.FRWHAT2SAYNEXT;
            frndWhat2SayNextRespMap[PlayerStates.NEUTRAL] = FrndWhat2SayNextAction;
            frndWhat2SayNext.stateToResponseMap = frndWhat2SayNextRespMap;
            dialogueList.Add(frndWhat2SayNext);
        }

        {
            SDialogue frndCantWaitToNap = new SDialogue();
            Dictionary<PlayerStates, Action> frndCantWaitToNapRespMap = new Dictionary<PlayerStates, Action>();
            frndCantWaitToNap.dialogueText = "And you, Anya. I bet you can’t wait to take a looong nap. I know I will.";
            frndCantWaitToNap.dialogueId = EDialogueID.FRCANTWAITTONAP;
            frndCantWaitToNapRespMap[PlayerStates.ACQUAINTANCE] = FrndCantWaitToNapAction;
            frndCantWaitToNap.stateToResponseMap = frndCantWaitToNapRespMap;
            dialogueList.Add(frndCantWaitToNap);
        }

        {
            SDialogue frndNowThatWeKnow = new SDialogue();
            Dictionary<PlayerStates, Action> frndNowThatWeKnowRespMap = new Dictionary<PlayerStates, Action>();
            frndNowThatWeKnow.dialogueText = "Anya. Cool. Now that you know me, can I ask you a favour?";
            frndNowThatWeKnow.dialogueId = EDialogueID.FRNOWTHATWEKNOW;
            frndNowThatWeKnowRespMap[PlayerStates.ACQUAINTANCE] = FrndNowThatWeKnowAction;
            frndNowThatWeKnow.stateToResponseMap = frndNowThatWeKnowRespMap;
            dialogueList.Add(frndNowThatWeKnow);
        }

        {
            SDialogue frndWhereDoUWork = new SDialogue();
            Dictionary<PlayerStates, Action> frndWhereDoUWorkRespMap = new Dictionary<PlayerStates, Action>();
            frndWhereDoUWork.dialogueText = "Ah, you work part-time? Where exactly?";
            frndWhereDoUWork.dialogueId = EDialogueID.FRWHEREDOUWORK;
            frndWhereDoUWorkRespMap[PlayerStates.ACQUAINTANCE] = FrndWhereDoUWorkAction;
            frndWhereDoUWork.stateToResponseMap = frndWhereDoUWorkRespMap;
            dialogueList.Add(frndWhereDoUWork);
        }

        {
            SDialogue frndAtLettingsRight = new SDialogue();
            Dictionary<PlayerStates, Action> frndAtLettingsRightRespMap = new Dictionary<PlayerStates, Action>();
            frndAtLettingsRight.dialogueText = "At AB&C Lettings, right?";
            frndAtLettingsRight.dialogueId = EDialogueID.FRATLETTINGSRIGHT;
            frndAtLettingsRightRespMap[PlayerStates.ACQUAINTANCE] = FrndAtLettingsRightAction;
            frndAtLettingsRight.stateToResponseMap = frndAtLettingsRightRespMap;
            dialogueList.Add(frndAtLettingsRight);
        }

        {
            SDialogue frndAppliedMyself = new SDialogue();
            Dictionary<PlayerStates, Action> frndAppliedMyselfRespMap = new Dictionary<PlayerStates, Action>();
            frndAppliedMyself.dialogueText = "Funny you should ask. I’ve actually applied for one of their part-time roles myself.";
            frndAppliedMyself.dialogueId = EDialogueID.FRAPPLIEDMYSELF;
            frndAppliedMyselfRespMap[PlayerStates.FRNDJOBREVEALED] = FrndAppliedMyselfAction;
            frndAppliedMyself.stateToResponseMap = frndAppliedMyselfRespMap;
            dialogueList.Add(frndAppliedMyself);
        }

        {
            SDialogue frndIDontWork = new SDialogue();
            Dictionary<PlayerStates, Action> frndIDontWorkRespMap = new Dictionary<PlayerStates, Action>();
            frndIDontWork.dialogueText = "No, I don't work.";
            frndIDontWork.dialogueId = EDialogueID.FRIDONTWORK;
            frndIDontWorkRespMap[PlayerStates.FRNDJOBREVEALED] = FrndIDontWorkAction;
            frndIDontWork.stateToResponseMap = frndIDontWorkRespMap;
            dialogueList.Add(frndIDontWork);
        }

        {
            SDialogue frndTheyBehindMyDad = new SDialogue();
            Dictionary<PlayerStates, Action> frndTheyBehindMyDadRespMap = new Dictionary<PlayerStates, Action>();
            frndTheyBehindMyDad.dialogueText = "Heard of them? They’re the ones responsible for my father’s disappearance! ";
            frndTheyBehindMyDad.dialogueId = EDialogueID.FRTHEYBEHINDMYDAD;
            frndTheyBehindMyDadRespMap[PlayerStates.FRNDJOBREVEALED] = FrndTheyBehindMyDadAction;
            frndTheyBehindMyDad.stateToResponseMap = frndTheyBehindMyDadRespMap;
            dialogueList.Add(frndTheyBehindMyDad);
        }

        {
            SDialogue frndItsBeenAWeek = new SDialogue();
            Dictionary<PlayerStates, Action> frndItsBeenAWeekRespMap = new Dictionary<PlayerStates, Action>();
            frndItsBeenAWeek.dialogueText = "Nah. It’s been, like, a week.";
            frndItsBeenAWeek.dialogueId = EDialogueID.FRITSBEENAWEEK;
            frndItsBeenAWeekRespMap[PlayerStates.FRNDJOBREVEALED] = FrndItsBeenAWeekAction;
            frndItsBeenAWeek.stateToResponseMap = frndItsBeenAWeekRespMap;
            dialogueList.Add(frndItsBeenAWeek);
        }

        {
            SDialogue frndWudITalk2U = new SDialogue();
            Dictionary<PlayerStates, Action> frndWudITalk2URespMap = new Dictionary<PlayerStates, Action>();
            frndWudITalk2U.dialogueText = "Would I be talking to you right now if I had?";
            frndWudITalk2U.dialogueId = EDialogueID.FRWUDITALK2U;
            frndWudITalk2URespMap[PlayerStates.FRNDJOBREVEALED] = FrndWudITalk2UAction;
            frndWudITalk2U.stateToResponseMap = frndWudITalk2URespMap;
            dialogueList.Add(frndWudITalk2U);
        }

        {
            SDialogue frndIfAppHighlighted = new SDialogue();
            Dictionary<PlayerStates, Action> frndIfAppHighlightedRespMap = new Dictionary<PlayerStates, Action>();
            frndIfAppHighlighted.dialogueText = "Yeah, that makes sense. If only there was some way to highlight my application...";
            frndIfAppHighlighted.dialogueId = EDialogueID.FRAPPHIGHLIGHT;
            frndIfAppHighlightedRespMap[PlayerStates.FRNDJOBREVEALED] = FrndIfOnlyAppHighlightedAction;
            frndIfAppHighlighted.stateToResponseMap = frndIfAppHighlightedRespMap;
            dialogueList.Add(frndIfAppHighlighted);
        }

        {
            SDialogue frndUKnowALot = new SDialogue();
            Dictionary<PlayerStates, Action> frndUKnowALotRespMap = new Dictionary<PlayerStates, Action>();
            frndUKnowALot.dialogueText = "You seem to know an awful lot about their hiring process for someone who just works part-time.";
            frndUKnowALot.dialogueId = EDialogueID.FRUKNOWALOT;
            frndUKnowALotRespMap[PlayerStates.FRNDJOBREVEALED] = FrndUKnowALotAction;
            frndUKnowALot.stateToResponseMap = frndUKnowALotRespMap;
            dialogueList.Add(frndUKnowALot);
        }

        {
            SDialogue frndWudBHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndWudBHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndWudBHelp.dialogueText = "Thanks, that would be a huge help!";
            frndWudBHelp.dialogueId = EDialogueID.FRWUDBHELP;
            frndWudBHelpRespMap[PlayerStates.FRNDJOBREVEALED] = FrndWudBHelpAction;
            frndWudBHelp.stateToResponseMap = frndWudBHelpRespMap;
            dialogueList.Add(frndWudBHelp);
        }

        {
            SDialogue frndTookULongEnuf = new SDialogue();
            Dictionary<PlayerStates, Action> frndTookULongEnufRespMap = new Dictionary<PlayerStates, Action>();
            frndTookULongEnuf.dialogueText = "*sighs in relief* Now we’re talking. Ah, finally...took you long enough.";
            frndTookULongEnuf.dialogueId = EDialogueID.FRTOOKULONGENUF;
            frndTookULongEnufRespMap[PlayerStates.FRNDJOBREVEALED] = FrndTookULongEnufAction;
            frndTookULongEnuf.stateToResponseMap = frndTookULongEnufRespMap;
            dialogueList.Add(frndTookULongEnuf);
        }

        {
            SDialogue frndMaybICanHelp = new SDialogue();
            Dictionary<PlayerStates, Action> frndMaybICanHelpRespMap = new Dictionary<PlayerStates, Action>();
            frndMaybICanHelp.dialogueText = "Oh. What is it? Maybe I can help.";
            frndMaybICanHelp.dialogueId = EDialogueID.FRMAYBEICANHELP;
            frndMaybICanHelpRespMap[PlayerStates.FRNDJOBREVEALED] = FrndMaybeICanHelpAction;
            frndMaybICanHelp.stateToResponseMap = frndMaybICanHelpRespMap;
            dialogueList.Add(frndMaybICanHelp);
        }

        {
            SDialogue frndOneTrackMind = new SDialogue();
            Dictionary<PlayerStates, Action> frndOneTrackMindRespMap = new Dictionary<PlayerStates, Action>();
            frndOneTrackMind.dialogueText = "Seriously? How one-track is your mind?";
            frndOneTrackMind.dialogueId = EDialogueID.FRONETRACKMIND;
            frndOneTrackMindRespMap[PlayerStates.FRNDJOBREVEALED] = FrndOneTrackMindAction;
            frndOneTrackMind.stateToResponseMap = frndOneTrackMindRespMap;
            dialogueList.Add(frndOneTrackMind);
        }

        {
            SDialogue frndWillTalkToBully = new SDialogue();
            Dictionary<PlayerStates, Action> frndWillTalkToBullyRespMap = new Dictionary<PlayerStates, Action>();
            frndWillTalkToBully.dialogueText = "Yeah, yeah, definitely. Just wait for me here, I’ll go talk to him.";
            frndWillTalkToBully.dialogueId = EDialogueID.FRWILLTALK2BULLY;
            frndWillTalkToBullyRespMap[PlayerStates.FRNDBULLYMENTIONED] = FrndWillTalk2BullyAction;
            frndWillTalkToBully.stateToResponseMap = frndWillTalkToBullyRespMap;
            dialogueList.Add(frndWillTalkToBully);
        }

        {
            SDialogue frndWontTalkToBully = new SDialogue();
            Dictionary<PlayerStates, Action> frndWontTalkToBullyRespMap = new Dictionary<PlayerStates, Action>();
            frndWontTalkToBully.dialogueText = "Oh, uh...no, I think I’ll prioritise my physical well-being and stay away from such risky engagements.";
            frndWontTalkToBully.dialogueId = EDialogueID.FRWONTTALK2BULLY;
            frndWontTalkToBullyRespMap[PlayerStates.FRNDBULLYMENTIONED] = FrndWontTalk2BullyAction;
            frndWontTalkToBully.stateToResponseMap = frndWontTalkToBullyRespMap;
            dialogueList.Add(frndWontTalkToBully);
        }

        {
            SDialogue frndLyingAbtBully = new SDialogue();
            Dictionary<PlayerStates, Action> frndLyingAbtBullyRespMap = new Dictionary<PlayerStates, Action>();
            frndLyingAbtBully.dialogueText = "No, I’m lying, I’m sorry. I’ll go talk to him right away.";
            frndLyingAbtBully.dialogueId = EDialogueID.FRLYINGABTBULLY;
            frndLyingAbtBullyRespMap[PlayerStates.WILLTALK2BULLY] = FrndLyingAbtBullyAction;
            frndLyingAbtBully.stateToResponseMap = frndLyingAbtBullyRespMap;
            dialogueList.Add(frndLyingAbtBully);
        }

        {
            SDialogue frndYesReallyHandledBully = new SDialogue();
            Dictionary<PlayerStates, Action> frndYesReallyHandledBullyRespMap = new Dictionary<PlayerStates, Action>();
            frndYesReallyHandledBully.dialogueText = "Yes.";
            frndYesReallyHandledBully.dialogueId = EDialogueID.FRYESREALLYHANDLEDBULLY;
            frndYesReallyHandledBullyRespMap[PlayerStates.WILLTALK2BULLY] = FrndYesReallyHandledBullyAction;
            frndYesReallyHandledBully.stateToResponseMap = frndYesReallyHandledBullyRespMap;
            dialogueList.Add(frndYesReallyHandledBully);
        }


    }

}
