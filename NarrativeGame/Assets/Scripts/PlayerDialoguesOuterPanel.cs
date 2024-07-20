using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialoguesOuterPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDialogueInnerPanelPrefab, dialogueButtonPrefab;

    private Animator myAnimator;
    private GameObject instantiatedPlayerDialogueInnerPanel;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

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

    public void Hide()
    {
        myAnimator.Play("Hide");
    }

    //Called at the end of "Hide" animation
    public void Disable()
    {
        Destroy(instantiatedPlayerDialogueInnerPanel);
        instantiatedPlayerDialogueInnerPanel = null;
        this.gameObject.SetActive(false);
    }
}
