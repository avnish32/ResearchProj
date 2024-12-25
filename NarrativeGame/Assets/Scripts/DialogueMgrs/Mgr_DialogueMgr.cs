using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mgr_DialogueMgr : DialogueManager
{
    [SerializeField]
    Janitor_DialogueMgr janitor;

    [SerializeField]
    L2Controller l2Controller;

    [SerializeField]
    private AudioClip makeItQuickVoice, moreSpecificVoice, leaveAloneVoice, whatUnitVoice,
        notAwareVoice, getToWorkVoice, sheVoice, nerveVoice, niceStoryVoice, lookVoice;

    private ECharacters MANAGER_CHAR = ECharacters.MANAGER;

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

    private void MgrNeed2TalkAction()
    {
        string[] dialogueList = {
            "Make it quick, I have things to do."
        };

        audioController.PlaySound(makeItQuickVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.MGRABTAYRAGO });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void MgrAbtAYrAgoAction()
    {
        string[] dialogueList = {
            "You’ll need to be more specific than that."
        };

        audioController.PlaySound(moreSpecificVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, () =>
        {
            List<SDialogue> playerDialogueList;

            if(janitor.GetStateWPlayer() == PlayerStates.MGRSECRETFOUND)
            {
                playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
                { EDialogueID.MGRABTSTORAGEUNIT });
            } else
            {
                playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
                { EDialogueID.MGRUKNOWWHATIMSAYIN });
            }
            
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void MgrUKnowWhatImSayinAction()
    {
        string[] dialogueList = {
            "Leave me alone, son.",
            "Play this game with someone else."
        };

        audioController.PlaySound(leaveAloneVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, gameController.EnablePlayerMovement);
    }

    private void MgrAbtStorageUnitAction()
    {
        string[] dialogueList = {
            "What storage unit?"
        };

        audioController.PlaySound(whatUnitVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.MGRONGROVEST, EDialogueID.MGRONTROVEST });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void MgrGroveStAction()
    {
        string[] dialogueList = {
            "I’m not aware of any storage unit there."
        };

        audioController.PlaySound(notAwareVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.MGRDONTPRETEND, EDialogueID.MGRDONTLIE });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void MgrTroveStAction()
    {
        string[] dialogueList = {
            "Stop spouting nonsense and get back to work!"
        };

        audioController.PlaySound(getToWorkVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, gameController.EnablePlayerMovement);
    }

    private void MgrDontPretendAction()
    {
        string[] dialogueList = {
            "She? Who is she?"
        };

        audioController.PlaySound(sheVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, () =>
        {
            var playerDialogueList = GetDialogueListFromId(new List<EDialogueID>
            { EDialogueID.MGRKATARINA, EDialogueID.MGRKATHERINE });
            uiController.DisplayPlayerDialoguePanel(playerDialogueList);
        });
    }

    private void MgrDontLieAction()
    {
        string[] dialogueList = {
            "You’ve got some nerve, spitting random allegations and some cock-and-bull story.",
            "Better do what I pay you for, young man, or I’ll show you out myself!"
        };

        audioController.PlaySound(nerveVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, gameController.EnablePlayerMovement);
    }

    private void MgrKatarinaAction()
    {
        string[] dialogueList = {
            "Hah, nice story, son.",
            "Why don’t you give up this job and write a book instead?"
        };

        audioController.PlaySound(niceStoryVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, gameController.EnablePlayerMovement);
    }

    private void MgrKatherineAction()
    {
        string[] dialogueList = {
            "Okay, look... *visibly sweating* *takes a deep breath*",
            "Look, I’d told her already. The guns inside that unit, I’ve sold them all.",
            "There’s nothing in there anymore.",
            "Maybe if that nosey reporter had not discovered it, we would have still been in business now.",
            "But he did. I never knew how.",
            "As much as I tried to make him talk, he wouldn’t speak a word, so I silenced him forever.",
            "That place was clearly not safe anymore.",
            "Katherine knows all this, I have no idea why she would send you to start it all over again.",
            "Tell her to lay low for a while; I thought she had better sense than this."
        };

        audioController.PlaySound(lookVoice);
        uiController.StartDialogues(dialogueList, MANAGER_CHAR, l2Controller.OnConfessionObtained);
    }

    private void PopulateDialogueList()
    {
        //######## LEAF DIALOGUES ############
        {
            SDialogue mgrNeed2Talk = new SDialogue();
            mgrNeed2Talk.dialogueText = "I need to talk to you.";
            mgrNeed2Talk.dialogueId = EDialogueID.MGRNEED2TALK;
            mgrNeed2Talk.playerStates = new List<PlayerStates> { PlayerStates.NEUTRAL };
            Dictionary<PlayerStates, Action> mgrNeed2TalkRespMap = new Dictionary<PlayerStates, Action>();
            mgrNeed2TalkRespMap[PlayerStates.NEUTRAL] = MgrNeed2TalkAction;
            mgrNeed2Talk.stateToResponseMap = mgrNeed2TalkRespMap;
            dialogueList.Add(mgrNeed2Talk);
        }

        //######## LEAF DIALOGUES END ############

        {
            SDialogue mgrAbtAYrAgo = new SDialogue();
            mgrAbtAYrAgo.dialogueText = "It’s about what happened a year ago.";
            mgrAbtAYrAgo.dialogueId = EDialogueID.MGRABTAYRAGO;
            Dictionary<PlayerStates, Action> mgrAbtAYrAgoRespMap = new Dictionary<PlayerStates, Action>();
            mgrAbtAYrAgoRespMap[PlayerStates.NEUTRAL] = MgrAbtAYrAgoAction;
            mgrAbtAYrAgo.stateToResponseMap = mgrAbtAYrAgoRespMap;
            dialogueList.Add(mgrAbtAYrAgo);
        }

        {
            SDialogue mgrUKnowWhatImSayin = new SDialogue();
            mgrUKnowWhatImSayin.dialogueText = "You know very well what I’m talking about.";
            mgrUKnowWhatImSayin.dialogueId = EDialogueID.MGRUKNOWWHATIMSAYIN;
            Dictionary<PlayerStates, Action> mgrUKnowWhatImSayinRespMap = new Dictionary<PlayerStates, Action>();
            mgrUKnowWhatImSayinRespMap[PlayerStates.NEUTRAL] = MgrUKnowWhatImSayinAction;
            mgrUKnowWhatImSayin.stateToResponseMap = mgrUKnowWhatImSayinRespMap;
            dialogueList.Add(mgrUKnowWhatImSayin);
        }

        {
            SDialogue mgrAbtStorageUnit = new SDialogue();
            mgrAbtStorageUnit.dialogueText = "It’s about a certain storage unit.";
            mgrAbtStorageUnit.dialogueId = EDialogueID.MGRABTSTORAGEUNIT;
            Dictionary<PlayerStates, Action> mgrAbtStorageUnitRespMap = new Dictionary<PlayerStates, Action>();
            mgrAbtStorageUnitRespMap[PlayerStates.NEUTRAL] = MgrAbtStorageUnitAction;
            mgrAbtStorageUnit.stateToResponseMap = mgrAbtStorageUnitRespMap;
            dialogueList.Add(mgrAbtStorageUnit);
        }

        {
            SDialogue mgrOnGroveSt = new SDialogue();
            mgrOnGroveSt.dialogueText = "The one on Grove Street.";
            mgrOnGroveSt.dialogueId = EDialogueID.MGRONGROVEST;
            Dictionary<PlayerStates, Action> mgrOnGroveStRespMap = new Dictionary<PlayerStates, Action>();
            mgrOnGroveStRespMap[PlayerStates.NEUTRAL] = MgrGroveStAction;
            mgrOnGroveSt.stateToResponseMap = mgrOnGroveStRespMap;
            dialogueList.Add(mgrOnGroveSt);
        }

        {
            SDialogue mgrOnTroveSt = new SDialogue();
            mgrOnTroveSt.dialogueText = "The one on Trove Street.";
            mgrOnTroveSt.dialogueId = EDialogueID.MGRONTROVEST;
            Dictionary<PlayerStates, Action> mgrOnGroveStRespMap = new Dictionary<PlayerStates, Action>();
            mgrOnGroveStRespMap[PlayerStates.NEUTRAL] = MgrTroveStAction;
            mgrOnTroveSt.stateToResponseMap = mgrOnGroveStRespMap;
            dialogueList.Add(mgrOnTroveSt);
        }

        {
            SDialogue mgrDontPretend = new SDialogue();
            mgrDontPretend.dialogueText = "You don’t need to pretend anymore, Mr. Bridges. " +
                "She’s interested in restarting business with you. Couldn’t come herself, " +
                "obviously, so she sent me instead.";
            mgrDontPretend.dialogueId = EDialogueID.MGRDONTPRETEND;
            Dictionary<PlayerStates, Action> mgrDontPretendRespMap = new Dictionary<PlayerStates, Action>();
            mgrDontPretendRespMap[PlayerStates.NEUTRAL] = MgrDontPretendAction;
            mgrDontPretend.stateToResponseMap = mgrDontPretendRespMap;
            dialogueList.Add(mgrDontPretend);
        }

        {
            SDialogue mgrDontLie = new SDialogue();
            mgrDontLie.dialogueText = "Don’t lie! I know about it, I know what you did and " +
                "how you covered it up when you were caught. There’s no use acting innocent now. ";
            mgrDontLie.dialogueId = EDialogueID.MGRDONTLIE;
            Dictionary<PlayerStates, Action> mgrDontLieRespMap = new Dictionary<PlayerStates, Action>();
            mgrDontLieRespMap[PlayerStates.NEUTRAL] = MgrDontLieAction;
            mgrDontLie.stateToResponseMap = mgrDontLieRespMap;
            dialogueList.Add(mgrDontLie);
        }

        {
            SDialogue mgrKatarina = new SDialogue();
            mgrKatarina.dialogueText = "Does 'Katarina' ring a bell?";
            mgrKatarina.dialogueId = EDialogueID.MGRKATARINA;
            Dictionary<PlayerStates, Action> mgrKatarinaRespMap = new Dictionary<PlayerStates, Action>();
            mgrKatarinaRespMap[PlayerStates.NEUTRAL] = MgrKatarinaAction;
            mgrKatarina.stateToResponseMap = mgrKatarinaRespMap;
            dialogueList.Add(mgrKatarina);
        }

        {
            SDialogue mgrKatherine = new SDialogue();
            mgrKatherine.dialogueText = "Does 'Katherine' ring a bell?";
            mgrKatherine.dialogueId = EDialogueID.MGRKATHERINE;
            Dictionary<PlayerStates, Action> mgrKatherineRespMap = new Dictionary<PlayerStates, Action>();
            mgrKatherineRespMap[PlayerStates.NEUTRAL] = MgrKatherineAction;
            mgrKatherine.stateToResponseMap = mgrKatherineRespMap;
            dialogueList.Add(mgrKatherine);
        }
    }
}
