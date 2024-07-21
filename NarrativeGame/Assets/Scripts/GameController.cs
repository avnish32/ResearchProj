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

    public void OnPauseButtonClicked()
    {
        if (isGamePaused)
        {
            return;
        }

        Time.timeScale = 0f;
        isGamePaused = true;
        canPlayerMoveOrInteract=false;

        foreach (var pauseBroadcaster in PauseBroadcaster.GetInstances())
        {
            //Debug.Log("PB ongamepaused called.");
            pauseBroadcaster.BroadcastPause();
        }
    }

    public void OnResumeButtonClicked()
    {
        canPlayerMoveOrInteract = true;
        isGamePaused=false;
        Time.timeScale = 1f;

        foreach (var pauseBroadcaster in PauseBroadcaster.GetInstances())
        {
            pauseBroadcaster.roadcastResume();
        }
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
