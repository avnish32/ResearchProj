using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpeechBubble : PanelAnimator
{
    [SerializeField]
    TextMeshProUGUI eToInteractText;

    public void OnSpawnAnimStart()
    {
        eToInteractText.enabled = false;
    }

    public override void OnHideAnimEnd()
    {
        eToInteractText.enabled=true;
        base.OnHideAnimEnd();
    }
}
