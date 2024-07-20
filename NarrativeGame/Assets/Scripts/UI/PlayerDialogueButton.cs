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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(SDialogue dialogue, UIController uiController)
    {
        this.dialogue = dialogue;
        this.uiController = uiController;
        buttonText.text = dialogue.dialogueText;
    }

    public void OnButtonClicked()
    {
        FindObjectOfType<AudioController>().PlaySound(clickSfx);
        this.dialogue.dialogueAction.Invoke();
        uiController.HidePlayerDialoguePanel();
    }
}
