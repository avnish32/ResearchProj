using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool canPlayerMoveOrInteract = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisablePlayerMovement()
    {
        canPlayerMoveOrInteract = false;
    }

    public void EnablePlayerMovement()
    {
        canPlayerMoveOrInteract = true;
    }

    public bool CanPlayerMoveOrInteract()
    {
        return canPlayerMoveOrInteract;
    }

    public void OnGameEnd()
    {
        //credits
        //load main menu
        Debug.Log("End of game.");
    }
}
