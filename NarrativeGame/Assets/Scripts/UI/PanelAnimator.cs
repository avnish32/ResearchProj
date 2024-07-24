using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PanelAnimator : MonoBehaviour
{
    private Animator myAnimator;
    protected bool hidDueToPause = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Hide(bool isJuicy, bool hidDueToPause = false)
    {
        this.hidDueToPause = hidDueToPause;

        if (!isJuicy)
        {
            OnHideAnimEnd();
            return;
        }

        myAnimator.Play("Hide");
    }

    //Called at the end of "Hide" animation
    //Can be overridden in child classes
    virtual public void OnHideAnimEnd()
    {
        this.gameObject.SetActive(false);
    }

    virtual public void OnSpawnAnimEnd()
    {
        //do nothing by default
    }

}
