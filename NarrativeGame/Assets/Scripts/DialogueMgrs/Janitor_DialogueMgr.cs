using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering.Universal;

public class Janitor_DialogueMgr : DialogueManager
{
    private string safeCode;
    //private bool isSafeCracked = false;
    private const ECharacters JANITOR_CHAR = ECharacters.JANITOR;

    // ### voices
    [SerializeField]
    AudioClip whatVoice, noneBusinessVoice, yeahVoice, reallyVoice, goodLuckVoice, dontWorryVoice;
    // ### voices end

    [SerializeField]
    AudioClip pencilWriteSfx;

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

    private void JanitorIHeardAction()
    {
        string[] dialogueList = {
            "What? What are you talking about?"
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID> 
            { EDialogueID.JANITORDONTPRETEND, EDialogueID.JANITORIWANTEDTOKNW });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorDoUKnwMyDadAction()
    {
        string[] dialogueList = {
            "I don’t even know *you*.",
            "Stop wasting my time with your stupid social pranks.",
            "Next thing you know, you’ll ask me to wave my hand at some hidden camera."
        };

        audioController.PlaySound(whatVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorRUCloseWidMgrAction()
    {
        string[] dialogueList = {
            "None of your business, kid."
        };

        audioController.PlaySound(noneBusinessVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorStillWantMe2CrackAction()
    {
        string[] dialogueList = {
            "Yuh-uh! Finally decided to give it a shot?"
        };

        audioController.PlaySound(yeahVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORITHINKSO, EDialogueID.JANITORDANGEROUSMISSION });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorSortWoutCrackingAction()
    {
        string[] dialogueList = {
            "Listen, kid, you scratch my back, I scratch yours. Simple as that."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorIGotCodeAction()
    {
        string[] dialogueList = {
            "By jove, for real?",
            "Well, spit it out then."
        };

        audioController.PlaySound(reallyVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            List<SDialogue> playerDialogueList;
            if (stateWPlayer.Equals(PlayerStates.SAFECRACKED))
            {
                playerDialogueList = GetDialogueListFromId(new List<EDialogueID>{EDialogueID.JANITORTHECODEISCORRECT});
                SDialogue theCodeIsCorrectDialogue = playerDialogueList[0];
                theCodeIsCorrectDialogue.dialogueText = string.Format
                ("It’s {0}. There was a lot of stuff inside, I couldn’t tell which file was yours.", safeCode);
                playerDialogueList.Clear();

                playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.JANITORTHECODEISWRONG});
                SDialogue theCodeIsWrongDialogue = playerDialogueList[0];

                char[] jumbledCode = safeCode.ToCharArray();
                jumbledCode[0] = safeCode[safeCode.Length - 1];
                jumbledCode[safeCode.Length - 1] = safeCode[0];

                theCodeIsWrongDialogue.dialogueText = string.Format
                ("It’s {0}. There was a lot of stuff inside, I couldn’t tell which file was yours.", new String(jumbledCode));
                
                playerDialogueList.Clear();

                playerDialogueList.Add(theCodeIsWrongDialogue);
                playerDialogueList.Add(theCodeIsCorrectDialogue);
            } else
            {
                playerDialogueList = GetDialogueListFromId(new List<EDialogueID> { EDialogueID.JANITORJOGMEMORY });
            }
             
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorStillWorkingAction()
    {
        string[] dialogueList = {
            "Good luck. Just so you know, I think all the digits are different. Hope that helps."
        };

        audioController.PlaySound(goodLuckVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorRepeatSecretAction()
    {
        string[] dialogueList = {
            "Not the sharpest of minds, are you?",
            "Bridges had a client – lady by the name of Katherine.",
            "He had rented out a storage unit to her on Grove Street, I think.",
            "Apparently, there was something inside that unit that wasn’t supposed to be there.",
            "What it was, I dunno, but one day he just closed that unit, took it off the listings" +
            " and I’m assuming that whatever was in there, isn’t there anymore.",
            "I’m guessing someone found out about it, Bridges panicked and shut shop, " +
            "and Katherine went into hiding.",
            "Got all that?"
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorThanksAction()
    {
        string[] dialogueList = {
            "Don’t worry about it, kid. I’m glad Bridges was finally brought to justice."
        };

        audioController.PlaySound(dontWorryVoice);
        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORWHATWEREURDEEDS });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorDontPretendAction()
    {
        string[] dialogueList = {
            "So?",
            "What’s it to you?"
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORJUSTCURIOUS, EDialogueID.JANITORMGRBMAILINGU });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorIWantedtoKnwAction()
    {
        string[] dialogueList = {
            "Quit poking your nose around, kid."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorJustCuriousAction()
    {
        string[] dialogueList = {
            "Beat it, kid.",
            "I’ve got other things to do than satisfy your desire for knowledge."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorMgrBMailingUAction()
    {
        string[] dialogueList = {
            "*sighs* Ain’t nothing anyone can do about it."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORHOPELESSGUY, EDialogueID.JANITORIWANNATAKEHIMDOWN });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorIWannaTakeHimDownAction()
    {
        string[] dialogueList = {
            "Why?"
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORMGRINVOLVEDININCIDENT, EDialogueID.JANITORGOTMYREASONS });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorHopelessGuyAction()
    {
        string[] dialogueList = {
            "I heard that."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorMgrInvolvedAction()
    {
        string[] dialogueList = {
            "I wish I could help you, kid.",
            "I don’t know the whole story, just bits and pieces.",
            "And I would have gone to the police and snitched on him, too, had it not been for the dirt he has on me.",
            "But what’s your deal? Why do you want to meddle in all this?"
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORCANTTELLU, EDialogueID.JANITORTURNINURFAVOR });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorGotMyReasonsAction()
    {
        string[] dialogueList = {
            "Well, if you’re keeping your mouth shut, so am I."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorTurnInUrFavorAction()
    {
        string[] dialogueList = {
            "My favour? How?"
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORIFMGRJAILED, EDialogueID.JANITORTELLMEABTDIRT });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorCantTellUAction()
    {
        string[] dialogueList = {
            "Fine, then. You keep your secrets, I’ll keep mine."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorTellMeAbtDirtAction()
    {
        string[] dialogueList = {
            "*sighs* All right.",
            "I’m an immigrant and...when I came here, I did some things that..." +
            "let’s just say I’m not exactly proud of them.",
            "I was made to do those things, and it was none other than Bridges who was behind it all.",
            "He offered me this janitor job in exchange for doing his dirty work.",
            "He kept a record of everything I did, and when I discovered what he was involved in, " +
            "he threatened to hand my file over to the police if I blew the whistle.",
            "He keeps the file in his safe inside his office.",
            "If only I could get the passcode...think you can manage it for me?"
        };
        stateWPlayer = PlayerStates.SAFECRACKOFFERED;

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.JANITORITHINKSO, EDialogueID.JANITORDANGEROUSMISSION });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void JanitorIfMgrJailedAction()
    {
        string[] dialogueList = {
            "Hmm...I don’t think it’s that simple."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorIThinkSoAction()
    {
        string[] dialogueList = {
            "Wow, you’ve got guts, kid, I’ll give you that.",
            "From what I know of Bridges, it is likely that all digits of the passcode are different.",
            "Other than that, you’re on your own.",
            "I do hope you crack it."
        };
        stateWPlayer = PlayerStates.SAFECRACKACCEPTED;

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorDangerousMissionAction()
    {
        gameController.EnablePlayerMovement();
    }

    private void JanitorTheCodeIsWrongAction()
    {
        //isSafeCracked = false;
        stateWPlayer = PlayerStates.SAFECRACKACCEPTED;
        SendJanitorToCheckCode();
    }

    private void SendJanitorToCheckCode()
    {
        string[] dialogueList = {
            "That’s fine, kid. I can deal with that.",
            "No way you could have checked every single file.",
            "But with the code I will go fetch it by myself as soon as I get a chance.",
            "I'll tell you everything once I get my file. Wait for me."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, WhileJanitorChecksCode);
    }

    private void WhileJanitorChecksCode()
    {
        string[] dialogueList = {
            "Some time later..."
        };

        uiController.FadeToBlack(() =>
        {
            uiController.StartDialogues(dialogueList, ECharacters.NARRATOR, AfterJanitorChecksCode);
        });
    }

    private void AfterJanitorChecksCode()
    {
        uiController.FadeFromBlack(null);

        if (stateWPlayer.Equals(PlayerStates.SAFECRACKED))
        {
            string[] dialogueList = {
                "Thanks, kid. I got my file.",
                "Couldn't have done it without your help.",
                "Right then, as promised, here’s what I know about that whole dirty business.",
                "Bridges had a client – a shady lady by the name of Katherine.",
                "He had rented out a storage unit to her. On Grove Street, I think.",
                "Apparently, there was something inside that unit that wasn’t supposed to be there.",
                "What it was, I dunno, but one day he just closed that unit, took it off the listings " +
                "and I’m assuming that whatever was in there, isn’t there anymore.",
                "I’m guessing someone found out about it, Bridges panicked and shut shop, " +
                "and Katherine went into hiding.",
                "But that’s all I know. I hope that helps you.",
                "I could do with seeing that man’s back when they catch him."
            };

            stateWPlayer = PlayerStates.MGRSECRETFOUND;
            uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
        }
        else
        {
            // this will execute when player has cracked the safe but chose the wrong code
            string[] dialogueList = {
                "Do you take me for a fool or something?",
                "The code you gave me was wrong! The safe didn't open an inch.",
                "Go try again, and you better come back with the correct code this time."
            };
            uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
        }
    }

    private void JanitorJogMemoryAction()
    {
        string[] dialogueList = {
            "*eyes you suspiciously* Okay."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, gameController.EnablePlayerMovement);
    }

    private void JanitorWhatWereDeedsAction()
    {
        string[] dialogueList = {
            "Hmm...It was only this one job that I did for him.",
            "I know I told you that he made me do a lot of things. Maybe because I was too desperate for your help.",
            "But in any case, I guess I can tell you what I did.",
            "I owe you this; after all, you helped me get my file back from Bridges’ safe."
        };

        uiController.StartDialogues(dialogueList, JANITOR_CHAR, EpilogueEndAction);
    }

    private void EpilogueEndAction()
    {
        // play scribbling noise
        audioController.PlaySound(pencilWriteSfx);
        uiController.FadeToBlack(null);

        string[] dialogueList = {
            "He wrote something on a piece of paper, handed it to me and left.",
            "Maybe he suspected that I was recording him, too.",
            "I wasn’t, but when I read what he’d written, I wished I had.",
            "The paper said: \"I took care of the reporter that broke the Bridges-Katherine story.\"",
            "...",
            "It was him...that bloody janitor was the one who was actually behind dad’s disappearance...",
            "...and I don’t even know his name."
        };

        uiController.StartDialogues(dialogueList, ECharacters.NARRATOR, gameController.OnGameEnd);
    }

    private void PopulateDialogueList()
    {
        //######## LEAF DIALOGUES ############
        {
            SDialogue janitorIHeardUTalking = new SDialogue();
            janitorIHeardUTalking.dialogueText = "I heard what you were saying.";
            janitorIHeardUTalking.dialogueId = EDialogueID.JANITORIHEARD;
            janitorIHeardUTalking.playerStates = new List<PlayerStates>{PlayerStates.NEUTRAL};
            Dictionary<PlayerStates, Action> janitorIHeardUTalkingRespMap = new Dictionary<PlayerStates, Action>();
            janitorIHeardUTalkingRespMap[PlayerStates.NEUTRAL] = JanitorIHeardAction;
            janitorIHeardUTalking.rshipToResponseMap = janitorIHeardUTalkingRespMap;
            dialogueList.Add(janitorIHeardUTalking);
        }

        {
            SDialogue janitorDoUKnwMyDad = new SDialogue();
            janitorDoUKnwMyDad.dialogueText = "Do you know about my father?";
            janitorDoUKnwMyDad.dialogueId = EDialogueID.JANITORDOUKNWMYDAD;
            janitorDoUKnwMyDad.playerStates = new List<PlayerStates> { PlayerStates.NEUTRAL };
            Dictionary<PlayerStates, Action> janitorDoUKnwMyDadRespMap = new Dictionary<PlayerStates, Action>();
            janitorDoUKnwMyDadRespMap[PlayerStates.NEUTRAL] = JanitorDoUKnwMyDadAction;
            janitorDoUKnwMyDad.rshipToResponseMap = janitorDoUKnwMyDadRespMap;
            dialogueList.Add(janitorDoUKnwMyDad);
        }

        {
            SDialogue janitorAreUCloseWidMgr = new SDialogue();
            janitorAreUCloseWidMgr.dialogueText = "Are you close with Mr. Bridges?";
            janitorAreUCloseWidMgr.dialogueId = EDialogueID.JANITORRUCLOSEWIDMGR;
            janitorAreUCloseWidMgr.playerStates = new List<PlayerStates> { PlayerStates.NEUTRAL };
            Dictionary<PlayerStates, Action> janitorAreUCloseWidMgrRespMap = new Dictionary<PlayerStates, Action>();
            janitorAreUCloseWidMgrRespMap[PlayerStates.NEUTRAL] = JanitorRUCloseWidMgrAction;
            janitorAreUCloseWidMgr.rshipToResponseMap = janitorAreUCloseWidMgrRespMap;
            dialogueList.Add(janitorAreUCloseWidMgr);
        }

        {
            SDialogue janitorStillWantMe2Crack = new SDialogue();
            janitorStillWantMe2Crack.dialogueText = "Do you still want me to crack that safe?";
            janitorStillWantMe2Crack.dialogueId = EDialogueID.JANITORSTILLWANTME2CRACK;
            janitorStillWantMe2Crack.playerStates = new List<PlayerStates> { PlayerStates.SAFECRACKOFFERED };
            Dictionary<PlayerStates, Action> janitorStillWantMe2CrackRespMap = new Dictionary<PlayerStates, Action>();
            janitorStillWantMe2CrackRespMap[PlayerStates.SAFECRACKOFFERED] = JanitorStillWantMe2CrackAction;
            janitorStillWantMe2Crack.rshipToResponseMap = janitorStillWantMe2CrackRespMap;
            dialogueList.Add(janitorStillWantMe2Crack);
        }

        {
            SDialogue janitorSortWoutCracking = new SDialogue();
            janitorSortWoutCracking.dialogueText = "Any chance we can sort this out without cracking the safe?";
            janitorSortWoutCracking.dialogueId = EDialogueID.JANITORSORTWOUTCRACKING;
            janitorSortWoutCracking.playerStates = new List<PlayerStates> { PlayerStates.SAFECRACKOFFERED };
            Dictionary<PlayerStates, Action> janitorSortWoutCrackingRespMap = new Dictionary<PlayerStates, Action>();
            janitorSortWoutCrackingRespMap[PlayerStates.SAFECRACKOFFERED] = JanitorSortWoutCrackingAction;
            janitorSortWoutCracking.rshipToResponseMap = janitorSortWoutCrackingRespMap;
            dialogueList.Add(janitorSortWoutCracking);
        }

        {
            SDialogue janitorGotCode = new SDialogue();
            janitorGotCode.dialogueText = "I did it, I got the passcode.";
            janitorGotCode.dialogueId = EDialogueID.JANITORIGOTCODE;
            janitorGotCode.playerStates = new List<PlayerStates> { PlayerStates.SAFECRACKACCEPTED, PlayerStates.SAFECRACKED };
            Dictionary<PlayerStates, Action> janitorGotCodeRespMap = new Dictionary<PlayerStates, Action>();
            janitorGotCodeRespMap[PlayerStates.SAFECRACKACCEPTED] = JanitorIGotCodeAction;
            janitorGotCodeRespMap[PlayerStates.SAFECRACKED] = JanitorIGotCodeAction;
            janitorGotCode.rshipToResponseMap = janitorGotCodeRespMap;
            dialogueList.Add(janitorGotCode);
        }

        {
            SDialogue janitorStillWorking = new SDialogue();
            janitorStillWorking.dialogueText = "Still working on cracking the safe.";
            janitorStillWorking.dialogueId = EDialogueID.JANITORSTILLWORKING;
            janitorStillWorking.playerStates = new List<PlayerStates> { PlayerStates.SAFECRACKACCEPTED };
            Dictionary<PlayerStates, Action> janitorStillWorkingRespMap = new Dictionary<PlayerStates, Action>();
            janitorStillWorkingRespMap[PlayerStates.SAFECRACKACCEPTED] = JanitorStillWorkingAction;
            janitorStillWorking.rshipToResponseMap = janitorStillWorkingRespMap;
            dialogueList.Add(janitorStillWorking);
        }

        {
            SDialogue janitorRepeatSecret = new SDialogue();
            janitorRepeatSecret.dialogueText = "Could you tell me the whole Mr. Bridges story again?";
            janitorRepeatSecret.dialogueId = EDialogueID.JANITORREPEATSECRET;
            janitorRepeatSecret.playerStates = new List<PlayerStates> { PlayerStates.MGRSECRETFOUND };
            Dictionary<PlayerStates, Action> janitorRepeatSecretRespMap = new Dictionary<PlayerStates, Action>();
            janitorRepeatSecretRespMap[PlayerStates.MGRSECRETFOUND] = JanitorRepeatSecretAction;
            janitorRepeatSecret.rshipToResponseMap = janitorRepeatSecretRespMap;
            dialogueList.Add(janitorRepeatSecret);
        }

        {
            SDialogue janitorThanks = new SDialogue();
            janitorThanks.dialogueText = "Hey, we finally did it. Thanks for all your help.";
            janitorThanks.dialogueId = EDialogueID.JANITORTHANKS;
            janitorThanks.playerStates = new List<PlayerStates> { PlayerStates.MGRARRESTED };
            Dictionary<PlayerStates, Action> janitorThanksRespMap = new Dictionary<PlayerStates, Action>();
            janitorThanksRespMap[PlayerStates.MGRARRESTED] = JanitorThanksAction;
            janitorThanks.rshipToResponseMap = janitorThanksRespMap;
            dialogueList.Add(janitorThanks);
        }

        //######## LEAF DIALOGUES END ############

        {
            SDialogue janitorDontPretend = new SDialogue();
            janitorDontPretend.dialogueText = "Don’t pretend. You were talking about something that happened a year ago.";
            janitorDontPretend.dialogueId = EDialogueID.JANITORDONTPRETEND;
            Dictionary<PlayerStates, Action> janitorDontPretendRespMap = new Dictionary<PlayerStates, Action>();
            janitorDontPretendRespMap[PlayerStates.NEUTRAL] = JanitorDontPretendAction;
            janitorDontPretend.rshipToResponseMap = janitorDontPretendRespMap;
            dialogueList.Add(janitorDontPretend);
        }

        {
            SDialogue janitorIWantedToKnw = new SDialogue();
            janitorIWantedToKnw.dialogueText = "I heard you and Mr. Bridges talking and I just wanted to know about it.";
            janitorIWantedToKnw.dialogueId = EDialogueID.JANITORIWANTEDTOKNW;
            Dictionary<PlayerStates, Action> janitorIWantedToKnwRespMap = new Dictionary<PlayerStates, Action>();
            janitorIWantedToKnwRespMap[PlayerStates.NEUTRAL] = JanitorIWantedtoKnwAction;
            janitorIWantedToKnw.rshipToResponseMap = janitorIWantedToKnwRespMap;
            dialogueList.Add(janitorIWantedToKnw);
        }

        {
            SDialogue janitorJustCurious = new SDialogue();
            janitorJustCurious.dialogueText = "Nothing, I was just curious.";
            janitorJustCurious.dialogueId = EDialogueID.JANITORJUSTCURIOUS;
            Dictionary<PlayerStates, Action> janitorIWantedToKnwRespMap = new Dictionary<PlayerStates, Action>();
            janitorIWantedToKnwRespMap[PlayerStates.NEUTRAL] = JanitorJustCuriousAction;
            janitorJustCurious.rshipToResponseMap = janitorIWantedToKnwRespMap;
            dialogueList.Add(janitorJustCurious);
        }

        {
            SDialogue janitorMgrBMailingU = new SDialogue();
            janitorMgrBMailingU.dialogueText = "I can tell that Mr. Bridges is blackmailing you.";
            janitorMgrBMailingU.dialogueId = EDialogueID.JANITORMGRBMAILINGU;
            Dictionary<PlayerStates, Action> janitorMgrBMailingURespMap = new Dictionary<PlayerStates, Action>();
            janitorMgrBMailingURespMap[PlayerStates.NEUTRAL] = JanitorMgrBMailingUAction;
            janitorMgrBMailingU.rshipToResponseMap = janitorMgrBMailingURespMap;
            dialogueList.Add(janitorMgrBMailingU);
        }

        {
            SDialogue janitorIWannaTakeHimDown = new SDialogue();
            janitorIWannaTakeHimDown.dialogueText = "I want to take him down.";
            janitorIWannaTakeHimDown.dialogueId = EDialogueID.JANITORIWANNATAKEHIMDOWN;
            Dictionary<PlayerStates, Action> janitorIWannaTakeHimDownRespMap = new Dictionary<PlayerStates, Action>();
            janitorIWannaTakeHimDownRespMap[PlayerStates.NEUTRAL] = JanitorIWannaTakeHimDownAction;
            janitorIWannaTakeHimDown.rshipToResponseMap = janitorIWannaTakeHimDownRespMap;
            dialogueList.Add(janitorIWannaTakeHimDown);
        }

        {
            SDialogue janitorHopelessGuy = new SDialogue();
            janitorHopelessGuy.dialogueText = "What a hopeless guy.";
            janitorHopelessGuy.dialogueId = EDialogueID.JANITORHOPELESSGUY;
            Dictionary<PlayerStates, Action> janitorHopelessGuyRespMap = new Dictionary<PlayerStates, Action>();
            janitorHopelessGuyRespMap[PlayerStates.NEUTRAL] = JanitorHopelessGuyAction;
            janitorHopelessGuy.rshipToResponseMap = janitorHopelessGuyRespMap;
            dialogueList.Add(janitorHopelessGuy);
        }

        {
            SDialogue janitorMgrInvolvedInIncident = new SDialogue();
            janitorMgrInvolvedInIncident.dialogueText = "From what he said, he clearly had something to do with that incident. I want to expose him.";
            janitorMgrInvolvedInIncident.dialogueId = EDialogueID.JANITORMGRINVOLVEDININCIDENT;
            Dictionary<PlayerStates, Action> janitorMgrInvolvedInIncidentRespMap = new Dictionary<PlayerStates, Action>();
            janitorMgrInvolvedInIncidentRespMap[PlayerStates.NEUTRAL] = JanitorMgrInvolvedAction;
            janitorMgrInvolvedInIncident.rshipToResponseMap = janitorMgrInvolvedInIncidentRespMap;
            dialogueList.Add(janitorMgrInvolvedInIncident);
        }

        {
            SDialogue janitorGotMyReasons = new SDialogue();
            janitorGotMyReasons.dialogueText = "I’ve got my reasons.";
            janitorGotMyReasons.dialogueId = EDialogueID.JANITORGOTMYREASONS;
            Dictionary<PlayerStates, Action> janitorGotMyReasonsRespMap = new Dictionary<PlayerStates, Action>();
            janitorGotMyReasonsRespMap[PlayerStates.NEUTRAL] = JanitorGotMyReasonsAction;
            janitorGotMyReasons.rshipToResponseMap = janitorGotMyReasonsRespMap;
            dialogueList.Add(janitorGotMyReasons);
        }

        {
            SDialogue janitorTurnInUrFavor = new SDialogue();
            janitorTurnInUrFavor.dialogueText = "Instead of that, you should focus on how you can turn this situation in your favour.";
            janitorTurnInUrFavor.dialogueId = EDialogueID.JANITORTURNINURFAVOR;
            Dictionary<PlayerStates, Action> janitorTurnInUrFavorRespMap = new Dictionary<PlayerStates, Action>();
            janitorTurnInUrFavorRespMap[PlayerStates.NEUTRAL] = JanitorTurnInUrFavorAction;
            janitorTurnInUrFavor.rshipToResponseMap = janitorTurnInUrFavorRespMap;
            dialogueList.Add(janitorTurnInUrFavor);
        }

        {
            SDialogue janitorCantTellU = new SDialogue();
            janitorCantTellU.dialogueText = "I, uh...well, I can’t tell you.";
            janitorCantTellU.dialogueId = EDialogueID.JANITORCANTTELLU;
            Dictionary<PlayerStates, Action> janitorCantTellURespMap = new Dictionary<PlayerStates, Action>();
            janitorCantTellURespMap[PlayerStates.NEUTRAL] = JanitorCantTellUAction;
            janitorCantTellU.rshipToResponseMap = janitorCantTellURespMap;
            dialogueList.Add(janitorCantTellU);
        }

        {
            SDialogue janitorTellAbtDirt = new SDialogue();
            janitorTellAbtDirt.dialogueText = "Tell me about this \"dirt\" Bridges has on you, and maybe I can help you bury it.";
            janitorTellAbtDirt.dialogueId = EDialogueID.JANITORTELLMEABTDIRT;
            Dictionary<PlayerStates, Action> janitorTellAbtDirtRespMap = new Dictionary<PlayerStates, Action>();
            janitorTellAbtDirtRespMap[PlayerStates.NEUTRAL] = JanitorTellMeAbtDirtAction;
            janitorTellAbtDirt.rshipToResponseMap = janitorTellAbtDirtRespMap;
            dialogueList.Add(janitorTellAbtDirt);
        }

        {
            SDialogue janitorIfMgrJailed = new SDialogue();
            janitorIfMgrJailed.dialogueText = "If he goes behind bars, you are rid of him. You can start a new life.";
            janitorIfMgrJailed.dialogueId = EDialogueID.JANITORIFMGRJAILED;
            Dictionary<PlayerStates, Action> janitorIfMgrJailedRespMap = new Dictionary<PlayerStates, Action>();
            janitorIfMgrJailedRespMap[PlayerStates.NEUTRAL] = JanitorIfMgrJailedAction;
            janitorIfMgrJailed.rshipToResponseMap = janitorIfMgrJailedRespMap;
            dialogueList.Add(janitorIfMgrJailed);
        }

        {
            SDialogue janitorIThinkSo = new SDialogue();
            janitorIThinkSo.dialogueText = "I think so.";
            janitorIThinkSo.dialogueId = EDialogueID.JANITORITHINKSO;
            Dictionary<PlayerStates, Action> janitorIThinkSoRespMap = new Dictionary<PlayerStates, Action>();
            janitorIThinkSoRespMap[PlayerStates.SAFECRACKOFFERED] = JanitorIThinkSoAction;
            janitorIThinkSo.rshipToResponseMap = janitorIThinkSoRespMap;
            dialogueList.Add(janitorIThinkSo);
        }

        {
            SDialogue janitorDangerousMission = new SDialogue();
            janitorDangerousMission.dialogueText = "Sounds like a dangerous mission. Let me think it over.";
            janitorDangerousMission.dialogueId = EDialogueID.JANITORDANGEROUSMISSION;
            Dictionary<PlayerStates, Action> janitorDangerousMissionRespMap = new Dictionary<PlayerStates, Action>();
            janitorDangerousMissionRespMap[PlayerStates.SAFECRACKOFFERED] = JanitorDangerousMissionAction;
            janitorDangerousMission.rshipToResponseMap = janitorDangerousMissionRespMap;
            dialogueList.Add(janitorDangerousMission);
        }

        {
            SDialogue janitorTheCodeIsCorrectOption = new SDialogue();
            janitorTheCodeIsCorrectOption.dialogueText = "Here you go. There was a lot of stuff inside, I couldn’t tell which file was yours.";
            janitorTheCodeIsCorrectOption.dialogueId = EDialogueID.JANITORTHECODEISCORRECT;
            Dictionary<PlayerStates, Action> janitorTheCodeIsCorrectOptionRespMap = new Dictionary<PlayerStates, Action>();
            janitorTheCodeIsCorrectOptionRespMap[PlayerStates.SAFECRACKED] = SendJanitorToCheckCode;
            janitorTheCodeIsCorrectOption.rshipToResponseMap = janitorTheCodeIsCorrectOptionRespMap;
            dialogueList.Add(janitorTheCodeIsCorrectOption);
        }

        {
            SDialogue janitorTheCodeIsWrongOption = new SDialogue();
            janitorTheCodeIsWrongOption.dialogueText = "Here you go. There was a lot of stuff inside, I couldn’t tell which file was yours.";
            janitorTheCodeIsWrongOption.dialogueId = EDialogueID.JANITORTHECODEISWRONG;
            Dictionary<PlayerStates, Action> janitorTheCodeIsWrongOptionRespMap = new Dictionary<PlayerStates, Action>();
            janitorTheCodeIsWrongOptionRespMap[PlayerStates.SAFECRACKED] = JanitorTheCodeIsWrongAction;
            janitorTheCodeIsWrongOption.rshipToResponseMap = janitorTheCodeIsWrongOptionRespMap;
            dialogueList.Add(janitorTheCodeIsWrongOption);
        }

        {
            SDialogue janitorJogMyMemory = new SDialogue();
            janitorJogMyMemory.dialogueText = "It’s uh...oh no, I forgot, let me jog my memory for a bit.";
            janitorJogMyMemory.dialogueId = EDialogueID.JANITORJOGMEMORY;
            Dictionary<PlayerStates, Action> janitorJogMyMemoryRespMap = new Dictionary<PlayerStates, Action>();
            janitorJogMyMemoryRespMap[PlayerStates.SAFECRACKACCEPTED] = JanitorJogMemoryAction;
            janitorJogMyMemory.rshipToResponseMap = janitorJogMyMemoryRespMap;
            dialogueList.Add(janitorJogMyMemory);
        }

        {
            SDialogue janitorWhatWereUrDeeds = new SDialogue();
            janitorWhatWereUrDeeds.dialogueText = "Now that it’s all behind us, can I ask you what dirty deeds Mr. Bridges made you do?";
            janitorWhatWereUrDeeds.dialogueId = EDialogueID.JANITORWHATWEREURDEEDS;
            Dictionary<PlayerStates, Action> janitorWhatWereUrDeedsRespMap = new Dictionary<PlayerStates, Action>();
            janitorWhatWereUrDeedsRespMap[PlayerStates.MGRARRESTED] = JanitorWhatWereDeedsAction;
            janitorWhatWereUrDeeds.rshipToResponseMap = janitorWhatWereUrDeedsRespMap;
            dialogueList.Add(janitorWhatWereUrDeeds);
        }
    }

    public void OnSafeCracked(string safeCode)
    {
        //isSafeCracked = true;
        stateWPlayer = PlayerStates.SAFECRACKED;
        this.safeCode = safeCode;
    }
}
