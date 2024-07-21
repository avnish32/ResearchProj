using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialoguesOuterPanel : PanelAnimator
{
    [SerializeField]
    private GameObject playerDialogueInnerPanelPrefab, dialogueButtonPrefab;

    private GameObject instantiatedPlayerDialogueInnerPanel;

    public void Spawn(List<SDialogue> dialogues, UIController uIController)
    {
        instantiatedPlayerDialogueInnerPanel = Instantiate(playerDialogueInnerPanelPrefab, transform, false);
        //instantiatedPlayerDialogueInnerPanel.transform.SetParent(transform, false);
        foreach (SDialogue dialogue in dialogues)
        {
            GameObject newDialogueButton = Instantiate(dialogueButtonPrefab);
            newDialogueButton.GetComponent<PlayerDialogueButton>().Init(dialogue, uIController);
            newDialogueButton.transform.SetParent(instantiatedPlayerDialogueInnerPanel.transform, false);
        }

        this.gameObject.SetActive(true);
        //spawn animation handled by controller
    }

    //Called at the end of "Hide" animation
    public new void OnHideAnimEnd()
    {
        if (!hidDueToPause)
        {
            Destroy(instantiatedPlayerDialogueInnerPanel);
            instantiatedPlayerDialogueInnerPanel = null;
        }
        
        this.gameObject.SetActive(false);
    }
}
