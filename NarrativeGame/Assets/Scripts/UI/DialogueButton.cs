using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    //private DialogueManager dialogueManager;
    private Dialogue dialogue;
    private UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Dialogue dialogue, UIController uiController)
    {
        this.dialogue = dialogue;
        this.uiController = uiController;
        buttonText.text = dialogue.dialogueText;
    }

    public void OnButtonClicked()
    {
        this.dialogue.dialogueAction.Invoke();
        uiController.HidePlayerDialoguePanel();
    }
}
