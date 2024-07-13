using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject safeCodeMinigame;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private Janitor_DialogueMgr janitor;

    private GameObject instantiatedMinigame;
    private bool isSafeCracked = false;

    public void Interact()
    {
        if (janitor.GetStateWPlayer() != PlayerStates.SAFECRACKACCEPTED || isSafeCracked)
        {
            //Dont let player interact with safe until player accepts safe crack mission
            //or if player has already cracked the safe
            return;
        }

        if (instantiatedMinigame != null)
        {
            return;
        }
        instantiatedMinigame = Instantiate(safeCodeMinigame);
        instantiatedMinigame.GetComponent<SafeCodePuzzle>().Init(this);
        gameController.DisablePlayerMovement();
    }

    public void OnSafeMinigameClosed()
    {
        gameController.EnablePlayerMovement();
        instantiatedMinigame = null;
    }

    public void OnSafeCracked(string codeString)
    {
        isSafeCracked =true;
        janitor.OnSafeCracked(codeString);
    }
}
