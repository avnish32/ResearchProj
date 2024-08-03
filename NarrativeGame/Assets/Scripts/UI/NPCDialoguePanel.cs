using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialoguePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI npcDialogueText;

    [SerializeField]
    private TextMeshProUGUI npcSpeakerNameText;

    [SerializeField]
    private GameObject npcSpeakerNamePanel;

    [SerializeField]
    private GameObject npcSpeakerImgPanel;

    [SerializeField]
    private Image npcSpeakerImg;

    private Animator animator;

    private void Awake()
    {
        //Debug.Log("npc dialogue panel awake");
        animator = GetComponent<Animator>();
    }

    public void SetNPCDialogueText(string dialogueText)
    {
        this.npcDialogueText.text = dialogueText;
    }

    public void SetNPCSpeakerDetails(string speakerText, Sprite npcImg)
    {
        npcSpeakerNameText.text = speakerText;
        npcSpeakerImg.sprite = npcImg;
    }

    public void HideNPCSpeakerDetails()
    {
        npcSpeakerNamePanel.SetActive(false);
        npcSpeakerImgPanel.SetActive(false);
    }

    public void ShowNPCSpeakerDetails(bool isGameJuicy)
    {
        npcSpeakerNamePanel.SetActive(true);

        if (isGameJuicy)
        {
            npcSpeakerImgPanel.SetActive(true);
        } else
        {
            npcSpeakerImgPanel.SetActive(false);
        }
        
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        //animation handled by controller

        // animation disabled for npc dialogue panel
        // as it was being played too often e.g. between different
        // function calls for different characters (L2 intro convo b/w mgr and janitor)
    }

    public void Hide()
    {
        animator.Play("Hide");
    }

    //Called at the end of "hide" animation
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
