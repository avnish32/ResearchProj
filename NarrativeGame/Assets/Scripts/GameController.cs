using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool canPlayerMoveOrInteract = true, isGamePaused = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
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
        SceneManager.LoadScene("MainMenu");
    }
}
