using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDialogueOuterPanel, playerDialogueInnerPanelPrefab, dialogueButtonPrefab;

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
            newDialogueButton.GetComponent<DialogueButton>().Init(dialogue, this);
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
}
