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
    SSPeakerInfo[] speakersInfo;

    private GameController gameController;
    private GameObject instantiatedPlayerDialogueInnerPanel;
    private bool npcDialogueAdvanced = false;
    private Dictionary<ECharacters, SSPeakerInfo> speakerToInfoMap;

    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        
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
        HidePlayerDialoguePanel();
        gameController.EnablePlayerMovement();
    }

    private void DisplayNPCDialoguePanel(ECharacters speaker)
    {
        if (speaker == ECharacters.NARRATOR)
        {
            npcDialoguePanel.HideNPCSpeakerNamePanel();
        } else
        {
            npcDialoguePanel.SetNPCSpeakerName(speakerToInfoMap[speaker].speakerName);
            npcDialoguePanel.ShowNPCSpeakerNamePanel();
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
            Debug.Log("i= " + i+" Frame: "+Time.frameCount);
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
}
