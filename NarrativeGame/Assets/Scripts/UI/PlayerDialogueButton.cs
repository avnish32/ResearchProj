using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDialogueButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    [SerializeField]
    private AudioClip clickSfx;

    //private DialogueManager dialogueManager;
    private SDialogue dialogue;
    private UIController uiController;

    public void Init(SDialogue dialogue, UIController uiController)
    {
        this.dialogue = dialogue;
        this.uiController = uiController;
        buttonText.text = dialogue.dialogueText;

        if (!uiController.IsGameJuicy())
        {
            GetComponent<Animator>().enabled = false;
        }
    }

    public void OnButtonClicked()
    {
        FindObjectOfType<AudioController>().PlaySound(clickSfx);
        this.dialogue.dialogueAction.Invoke();
        uiController.HidePlayerDialoguePanel();
    }
}
