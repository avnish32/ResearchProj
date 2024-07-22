using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    AudioClip clickSfx;
    
    private bool canPlayerMoveOrInteract = true, isGamePaused = false;
    private AudioController audioController;

    private void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }

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

        audioController.PlaySound(clickSfx);
        Time.timeScale = 0f;
        isGamePaused = true;

        foreach (var pauseBroadcaster in PauseBroadcaster.GetInstances())
        {
            //Debug.Log("PB ongamepaused called.");
            pauseBroadcaster.BroadcastPause();
        }
    }

    public void OnResumeButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        isGamePaused =false;
        Time.timeScale = 1f;

        foreach (var pauseBroadcaster in PauseBroadcaster.GetInstances())
        {
            pauseBroadcaster.BroadcastResume();
        }
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
