using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyFaceButton : MonoBehaviour
{
    private float lifetimeInSecs;
    private WAB wabMgr;
    private RectTransform slotParentBehind, slotParentFront, slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifetimeInSecs);
        GetComponent<Animator>().Play("Destroy");
    }

    public void Init(float lifetime, WAB wabMgr, RectTransform slotParentBehind, RectTransform slotParentFront,
        RectTransform slot)
    {
        this.lifetimeInSecs = lifetime;
        this.wabMgr = wabMgr;
        this.slotParentBehind = slotParentBehind;
        this.slotParentFront = slotParentFront;
        this.slot = slot;
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
        slot.SetParent(slotParentBehind);
    }

    public void BringSlotsToFront()
    {
        slot.SetParent(slotParentFront);
    }
}
