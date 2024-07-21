using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private PlayerDialoguesOuterPanel playerDialogueOuterPanel;

    [SerializeField]
    private NPCDialoguePanel npcDialoguePanel;

    [SerializeField]
    private BlackPanel blackPanel;

    [SerializeField]
    SSPeakerInfo[] speakersInfo;

    [SerializeField]
    private AudioClip clickSfx;

    private AudioController audioController;
    private GameController gameController;
    private bool npcDialogueAdvanced = false;
    private Dictionary<ECharacters, SSPeakerInfo> speakerToInfoMap;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speakerToInfoMap = new Dictionary<ECharacters, SSPeakerInfo>();
        foreach (var speakerInfo in speakersInfo)
        {
            speakerToInfoMap[speakerInfo.speaker] = speakerInfo;
        }

        playerDialogueOuterPanel.Disable();
        npcDialoguePanel.Disable();
    }

    public void DisplayPlayerDialoguePanel(List<SDialogue> dialogues)
    {
        gameController.DisablePlayerMovement();
        playerDialogueOuterPanel.Spawn(dialogues, this);
    }

    public void HidePlayerDialoguePanel()
    {
        playerDialogueOuterPanel.Hide();
    }

    public void OnPlayerDialoguePanelClosed()
    {
        audioController.PlaySound(clickSfx);
        HidePlayerDialoguePanel();
        gameController.EnablePlayerMovement();
    }

    private void DisplayNPCDialoguePanel(ECharacters speaker)
    {
        if (speaker == ECharacters.NARRATOR)
        {
            npcDialoguePanel.HideNPCSpeakerDetails();
        } else
        {
            SSPeakerInfo speakerInfo = speakerToInfoMap[speaker];
            npcDialoguePanel.SetNPCSpeakerDetails(speakerInfo.speakerName, speakerInfo.speakerImg);
            npcDialoguePanel.ShowNPCSpeakerDetails();
        }

        npcDialoguePanel.Spawn();
    }

    private void HideNPCDialoguePanel()
    {
        npcDialoguePanel.Disable();
    }

    public bool IsAnyDialogueGoingOn()
    {
        return npcDialoguePanel.gameObject.activeInHierarchy || playerDialogueOuterPanel.gameObject.activeInHierarchy;
    }

    private IEnumerator RunThroughNPCDialogues(string[] npcDialogues, Action dialogueEndAction)
    {
        for (int i = 0; i < npcDialogues.Length; i++)
        {
            //Debug.Log("i= " + i+" Frame: "+Time.frameCount);
            npcDialoguePanel.SetNPCDialogueText(npcDialogues[i]);
            npcDialogueAdvanced = false;

            yield return new WaitUntil(() =>
            {
                return npcDialogueAdvanced;
            });
        }
        HideNPCDialoguePanel();
        if (dialogueEndAction != null)
        {
            dialogueEndAction.Invoke();
        }
    }

    public void AdvanceNPCDialogue()
    {
        audioController.PlaySound(clickSfx);
        npcDialogueAdvanced = true;
    }

    /*
     Displays panel only for first dialogue.
    Thereafter only changes the text.
     */
    public void StartDialogues(string[] npcDialogues, ECharacters speaker, Action dialogueEndAction)
    {
        gameController.DisablePlayerMovement();
        DisplayNPCDialoguePanel(speaker);
        StartCoroutine(RunThroughNPCDialogues(npcDialogues, dialogueEndAction));
    }

    public void FadeToBlack(Action fadeEndAction)
    {
        blackPanel.FadeIn(fadeEndAction);
    }

    public void FadeFromBlack(Action fadeEndAction)
    {
        blackPanel.FadeOut(fadeEndAction);
    }

    public void OnGamePaused()
    {
        Debug.Log("UI controller paused.");
    }

    public void OnGameResumed()
    {
        Debug.Log("UI controller resumed.");
    }
}
