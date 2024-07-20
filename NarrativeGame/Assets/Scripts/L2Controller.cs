using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class L2Controller : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private AudioClip levelBGM, climaxBGM;

    [SerializeField]
    private Janitor_DialogueMgr janitor;

    [SerializeField]
    private Mgr_DialogueMgr manager;

    [SerializeField]
    private CinemachineVirtualCamera mainCam, opCutsceneCam;

    [SerializeField]
    private GameObject mgrAndJanitor;

    private const ECharacters JANITOR_CHAR = ECharacters.JANITOR;
    private const ECharacters MANAGER_CHAR = ECharacters.MANAGER;
    private const int MAIN_CAM_PRIORITY = 10, SECONDARY_CAM_PRIORITY = 5;

    // Start is called before the first frame update
    void Start()
    {
        audioController.PlayMusic(levelBGM);
        opCutsceneCam.Priority = SECONDARY_CAM_PRIORITY;
        mainCam.Priority = MAIN_CAM_PRIORITY;

        string[] l2IntroDialogue =
        {
            "Thanks to Anya, I managed to secure a part-time gig at AB&C Lettings.",
            "My first few weeks were spent getting familiar with the office and the employees.",
            "Being \"the new guy\", my inquisitive questions were passed off good-naturedly as the " +
            "natural curiosity of a learner.",
            "I used it to my advantage and managed to learn that the janitor knew the manager, Mr. Bridges, too well. " +
            "Unusually well, I would say.",
            "I suspected they knew something that could help me in my investigation.",
            "Sure enough, I overheard a conversation earlier today that confirmed my theory."
        };

        uiController.StartDialogues(l2IntroDialogue, ECharacters.NARRATOR, StartJanitorLetMeGoDialogue);
    }

    private void StartJanitorLetMeGoDialogue()
    {
        opCutsceneCam.Priority = MAIN_CAM_PRIORITY;
        mainCam.Priority = SECONDARY_CAM_PRIORITY;

        string[] janitorLetMeGoDialogue =
        {
            "...That’s just absurd, I can’t keep on working here forever. You gotta let me go!"
        };

        uiController.StartDialogues(janitorLetMeGoDialogue, JANITOR_CHAR, StartMgrRiskyDialogue);
    }

    private void StartMgrRiskyDialogue()
    {
        string[] mgrTooRiskyDialogue =
        {   
            "I can’t. It’s too risky for me."
        };

        uiController.StartDialogues(mgrTooRiskyDialogue, MANAGER_CHAR, StartJanitorWontTellDialogue);
    }

    private void StartJanitorWontTellDialogue()
    {
        string[] janitorWontTellAny1Dialogue =
        {
            "I promise not to tell nobody nothing."
        };
        uiController.StartDialogues(janitorWontTellAny1Dialogue, JANITOR_CHAR, StartMgrUCantTellDialogue);
    }

    private void StartMgrUCantTellDialogue()
    {
        string[] mgrUCantTellDialogue =
        {
            "You couldn’t even if you wanted to, son."
        };
        uiController.StartDialogues(mgrUCantTellDialogue, MANAGER_CHAR, StartJanitorAYrAgoDialogue);
    }

    private void StartJanitorAYrAgoDialogue()
    {
        string[] janitorItWasAYrAgoDialogue =
        {
            "That’s what I’m sayin’!",
            "You got my file and everything, Bridges.",
            "Why can’t you let me leave and make a new life for myself?",
            "It’s been more than a year since it happened, anyway. Nobody even remembers it, I’m telling you!"
        };
        uiController.StartDialogues(janitorItWasAYrAgoDialogue, JANITOR_CHAR, StartMgrKeepWorkingDialogue);
    }

    private void StartMgrKeepWorkingDialogue()
    {
        string[] mgrKeepWorkingDialogue =
        {
            "Doesn’t matter. You keep working where I can see you and keep your mouth shut.",
            "That’s that."
        };
        uiController.StartDialogues(mgrKeepWorkingDialogue, MANAGER_CHAR, StartNarratorIntroEndDialogue);
    }

    private void StartNarratorIntroEndDialogue() 
    {
        opCutsceneCam.Priority = SECONDARY_CAM_PRIORITY;
        mainCam.Priority = MAIN_CAM_PRIORITY;

        string[] narratorIntroEndDialogue =
        {
            "They were talking about something that happened one year ago.",
            "It has to be that incident with dad.",
            "The janitor clearly knows something about it. I should talk to him tactfully and try to get info out of him."
        };
        uiController.StartDialogues(narratorIntroEndDialogue, ECharacters.NARRATOR, () => {

            Destroy(mgrAndJanitor);
            gameController.EnablePlayerMovement();
        });
    }

    private void StartEpilogueDialogues()
    {
        string[] epilogueDialogue =
        {
            "Mr. Bridges spilled it all the moment he heard Katherine’s name.",
            "I was prepared for this; I recorded it all using the voice recorder in my phone and " +
            "wasted no time in contacting the police.",
            "Further investigation inevitably led to his arrest and for the first time since dad disappeared, " +
            "I felt a sense of closure.",
            "I couldn’t have done it without the janitor’s help, of course.",
            "I should go and thank him."
        };

        audioController.PlayMusic(climaxBGM);
        janitor.SetStateWPlayer(PlayerStates.MGRARRESTED);

        uiController.StartDialogues(epilogueDialogue, ECharacters.NARRATOR, () => {
            uiController.FadeFromBlack(null);
            gameController.EnablePlayerMovement();

        });
    }


    public void OnConfessionObtained()
    {
        //TODO fade to black
        uiController.FadeToBlack(() =>
        {
            Destroy(manager.gameObject);
        });
        //TODO remove manager from level

        StartEpilogueDialogues();
    }
}
