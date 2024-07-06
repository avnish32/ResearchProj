using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialoguePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI npcDialogueText;

    [SerializeField]
    Animation spawnAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (spawnAnim != null)
        {
            spawnAnim.Play();
        }
    }

    public void SetNPCDialogueText(string dialogueText)
    {
        this.npcDialogueText.text = dialogueText;
    }
}
