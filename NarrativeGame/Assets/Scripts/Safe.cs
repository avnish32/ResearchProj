using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject safeCodeMinigame;

    public void Interact()
    {
        Instantiate(safeCodeMinigame);
    }
}
