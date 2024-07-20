using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDialogueOuterPanel, playerDialogueInnerPanelPrefab, dialogueButtonPrefab;

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
    private GameObject instantiatedPlayerDialogueInnerPanel;
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

        playerDialogueOuterPanel.SetActive(false);
        npcDialoguePanel.gameObject.SetActive(false);
    }

    public void DisplayPlayerDialoguePanel(List<SDialogue> dialogues)
    {
        instantiatedPlayerDialogueInnerPanel = Instantiate(playerDialogueInnerPanelPrefab);
        instantiatedPlayerDialogueInnerPanel.transform.SetParent(playerDialogueOuterPanel.transform, false);
        foreach (SDialogue dialogue in dialogues)
        {
            GameObject newDialogueButton = Instantiate(dialogueButtonPrefab);
            newDialogueButton.GetComponent<PlayerDialogueButton>().Init(dialogue, this);
            newDialogueButton.transform.SetParent(instantiatedPlayerDialogueInnerPanel.transform, false);
        }

        gameController.DisablePlayerMovement();
        playerDialogueOuterPanel.SetActive(true);
    }

    public void HidePlayerDialoguePanel()
    {
        Destroy(instantiatedPlayerDialogueInnerPanel);
        instantiatedPlayerDialogueInnerPanel = null;
        playerDialogueOuterPanel.SetActive(false);
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
        
        npcDialoguePanel.gameObject.SetActive(true);
    }

    private void HideNPCDialoguePanel()
    {
        npcDialoguePanel.gameObject.SetActive(false);
    }

    public bool IsAnyDialogueGoingOn()
    {
        return npcDialoguePanel.gameObject.activeInHierarchy || playerDialogueOuterPanel.activeInHierarchy;
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
}
