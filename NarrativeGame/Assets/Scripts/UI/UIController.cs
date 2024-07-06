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
    InputActionReference npcDialogueAdvanceAction;

    private GameController gameController;
    private GameObject instantiatedPlayerDialogueInnerPanel;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerDialogueOuterPanel.SetActive(false);
        npcDialoguePanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPlayerDialoguePanel(List<Dialogue> dialogues)
    {
        instantiatedPlayerDialogueInnerPanel = Instantiate(playerDialogueInnerPanelPrefab);
        instantiatedPlayerDialogueInnerPanel.transform.SetParent(playerDialogueOuterPanel.transform, false);
        foreach (Dialogue dialogue in dialogues)
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
        gameController.EnablePlayerMovement();
    }

    private void DisplayNPCDialoguePanel()
    {
        npcDialoguePanel.gameObject.SetActive(true);
    }

    private void HideNPCDialoguePanel()
    {
        npcDialoguePanel.gameObject.SetActive(false);
    }

    private IEnumerator WaitForActionPerformed(InputAction action)
    {
        while (!action.WasReleasedThisFrame())
        {
            yield return null;
        }
    }

    private IEnumerator RunThroughNPCDialogues(string[] npcDialogues, Action dialogueEndAction)
    {
        for (int i = 0; i < npcDialogues.Length; i++)
        {
            npcDialoguePanel.SetNPCDialogueText(npcDialogues[i]);
            yield return StartCoroutine(WaitForActionPerformed(npcDialogueAdvanceAction.action));
        }
        HideNPCDialoguePanel();
        if (dialogueEndAction != null)
        {
            dialogueEndAction.Invoke();
        }
    }

    /*
     Displays panel only for first dialogue.
    Thereafter only changes the text.
     */
    public void StartNPCDialogues(string[] npcDialogues, Action dialogueEndAction)
    {
        gameController.DisablePlayerMovement();
        DisplayNPCDialoguePanel();
        StartCoroutine(RunThroughNPCDialogues(npcDialogues, dialogueEndAction));
    }
}
