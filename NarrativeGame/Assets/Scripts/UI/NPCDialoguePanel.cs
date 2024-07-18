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

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("npc dialogue panel start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //Debug.Log("npc dialogue panel enabled");
        //juice
        //animator.Play("Spawn");
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

    public void ShowNPCSpeakerDetails()
    {
        npcSpeakerNamePanel.SetActive(true);
        npcSpeakerImgPanel.SetActive(true);
    }
}
