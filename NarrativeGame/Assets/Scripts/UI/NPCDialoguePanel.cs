using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialoguePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI npcDialogueText;

    [SerializeField]
    private TextMeshProUGUI npcSpeakerNameText;

    [SerializeField]
    private GameObject npcSpeakerNamePanel;

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

    public void SetNPCSpeakerName(string speakerText)
    {
        npcSpeakerNameText.text = speakerText;
    }

    public void HideNPCSpeakerNamePanel()
    {
        npcSpeakerNamePanel.SetActive(false);
    }

    public void ShowNPCSpeakerNamePanel()
    {
        npcSpeakerNamePanel.SetActive(true);
    }
}
