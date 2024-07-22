using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsPanel, controlsPanel, buttonsPanel;

    [SerializeField]
    private AudioClip menuBgm, clickSfx;

    [SerializeField]
    private AudioController audioController;

    private GameObject currentlyActivePanel;

    // Start is called before the first frame update
    void Start()
    {
        audioController.PlayMusic(menuBgm);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        UpdateCurrentActivePanel(buttonsPanel);
    }

    private void UpdateCurrentActivePanel(GameObject panelToActivate)
    {
        if (currentlyActivePanel != null)
        {
            currentlyActivePanel.SetActive(false);
        }
        
        currentlyActivePanel = panelToActivate;
        currentlyActivePanel.SetActive(true);
    }

    public void OnControlsButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        UpdateCurrentActivePanel(controlsPanel);
    }

    public void OnCreditsButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        UpdateCurrentActivePanel(creditsPanel);
    }

    public void OnCloseButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        UpdateCurrentActivePanel(buttonsPanel);
    }

    public void OnExitButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        Application.Quit();
    }

    public void OnPlayButtonClicked()
    {
        audioController.PlaySound(clickSfx);
        SceneManager.LoadScene("L1_School");
    }
}
