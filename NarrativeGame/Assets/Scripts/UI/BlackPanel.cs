using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPanel : MonoBehaviour
{
    private Animator animator;
    private Action animEndAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FadeIn(Action fadeEndAction)
    {
        animEndAction = fadeEndAction;
        animator.Play("FadeIn");
    }

    public void FadeOut(Action fadeEndAction)
    {
        animEndAction = fadeEndAction;
        animator.Play("FadeOut");
    }

    public void InvokeAnimEndAction()
    {
        if (animEndAction != null)
        {
            animEndAction.Invoke();
            animEndAction = null;
        }
    }

}
