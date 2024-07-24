using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyFaceButton : MonoBehaviour
{
    private float lifetimeInSecs;
    private WAB wabMgr;
    private RectTransform slotParentBehind, slotParentFront, slot;
    private bool isGameJuicy;

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifetimeInSecs);
        GetComponent<Animator>().Play("Destroy");
    }

    public void Init(float lifetime, WAB wabMgr, RectTransform slotParentBehind, RectTransform slotParentFront,
        RectTransform slot, bool isGameJuicy)
    {
        this.lifetimeInSecs = lifetime;
        this.wabMgr = wabMgr;
        this.slotParentBehind = slotParentBehind;
        this.slotParentFront = slotParentFront;
        this.slot = slot;
        this.isGameJuicy = isGameJuicy;
        StartCoroutine(Countdown());
    }

    public void DestroyButtonOnLifetimeEnd()
    {
        wabMgr.IncrementMiss();
        //BringSlotsToFront();
        Destroy(gameObject);
    }

    public void OnButtonClicked()
    {
        wabMgr.IncrementScore();
        BringSlotsToFront();
        Destroy(gameObject);
    }

    public void PushSlotsBack()
    {
        if ( !isGameJuicy)
        {
            return;
        }
        slot.SetParent(slotParentBehind);
    }

    public void BringSlotsToFront()
    {
        if (!isGameJuicy)
        {
            return;
        }
        slot.SetParent(slotParentFront);
    }
}
