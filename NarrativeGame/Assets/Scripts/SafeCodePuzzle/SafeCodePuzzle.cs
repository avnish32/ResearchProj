using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SafeCodePuzzle : MonoBehaviour
{
    [SerializeField]
    private Canvas puzzleCanvas;
    
    [SerializeField]
    private TMP_InputField[] inputDigits;

    [SerializeField]
    private RectTransform historyPanel;

    [SerializeField]
    private Animator[] historyItemLabelAnimators;

    [SerializeField]
    private SafeCodeHistoryItem historyItemPrefab;

    [SerializeField]
    private InputActionReference codeEnterInputAction;

    [SerializeField]
    private TextMeshProUGUI endMsgText;

    [SerializeField]
    private GameObject winPanel, losePanel, inputFieldPanel, enterButton;

    [SerializeField]
    private AudioClip clickSfx, onCrackedSfx, onNotCrackedSfx;
    
    private int guessesRemaining;
    private int[] code = new int[3];
    private int[] codeEntered = new int[3];
    private List<int> numsAvailableForCode = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private bool isCodeCracked = false, isGamePaused = false;
    private string codeString = "";
    private Safe instantiatingSafe;
    private AudioController audioController;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<code.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, numsAvailableForCode.Count);
            code[i] = numsAvailableForCode[randomIndex];
            numsAvailableForCode.RemoveAt(randomIndex);
            ResetInputField(i);

            codeString += code[i].ToString();
        }

        codeEnterInputAction.action.performed += OnCodeEnteredUsingInputAction;

        inputDigits[0].ActivateInputField();
        guessesRemaining = 5;
        endMsgText.text = string.Empty;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void ResetInputField(int i)
    {
        inputDigits[i].onValueChanged.RemoveAllListeners();
        inputDigits[i].text = string.Empty;
        inputDigits[i].onValueChanged.AddListener( delegate { SelectNextInputDigit(i); });
    }

    private bool IsEnteredCodeValid()
    {
        try
        {
            for (int i = 0; i<inputDigits.Length; i++)
            {
                codeEntered[i] = Convert.ToInt16(inputDigits[i].text);
            }
        } catch (Exception e)
        {
            return false;
        }
        return true;
    }

    private void OnCodeEnteredUsingInputAction(InputAction.CallbackContext ctx)
    {
        OnCodeEntered();
    }

    private void OnDestroy()
    {
        codeEnterInputAction.action.performed -= OnCodeEnteredUsingInputAction;
        instantiatingSafe.OnSafeMinigameClosed();
    }

    public void Init(Safe instantiatingSafe, AudioController audioController,
        GameController gameController)
    {
        this.instantiatingSafe = instantiatingSafe;
        this.audioController = audioController;
        this.gameController = gameController;
    }

    public void OnCodeEntered()
    {
        //Debug.Log("Inside on code entered.");
        if (isGamePaused)
        {
            return;
        }
        
        if (isCodeCracked || guessesRemaining <= 0)
        {
            inputDigits[0].ActivateInputField();
            return;
        }
        audioController.PlaySound(clickSfx);

        if (!IsEnteredCodeValid() )
        {
            inputDigits[0].ActivateInputField();
            return;
        }

        guessesRemaining--;

        SafeCodeHistoryItem instantiatedHistoryItem = Instantiate(historyItemPrefab, historyPanel, false);
        instantiatedHistoryItem.Init(codeEntered, code);
        if (audioController.IsGameJuicy())
        {
            historyItemLabelAnimators[historyItemLabelAnimators.Length - guessesRemaining - 1].Play("OnUpdate");
        }

        for (int i = 0; i < inputDigits.Length; i++)
        {
            ResetInputField(i);
        }
        inputDigits[0].ActivateInputField();

        if (instantiatedHistoryItem.IsEnteredCodeCorrect())
        {
            Debug.Log("Successfully cracked the code. Codestring: "+codeString);

            if (gameController.IsGameJuicy())
            {
                inputFieldPanel.SetActive(false);
                enterButton.SetActive(false);
                winPanel.SetActive(true);
            }

            endMsgText.text = "Success! Press 'Exit' to leave.";
            audioController.PlaySound(onCrackedSfx);
            
            isCodeCracked = true;
            instantiatingSafe.OnSafeCracked(codeString);
            return;
        }

        if (guessesRemaining <= 0)
        {
            Debug.Log("Couldn't guess code. Code was: "+codeString);
            
            if (gameController.IsGameJuicy())
            {
                
                inputFieldPanel.SetActive(false);
                enterButton.SetActive(false);
                losePanel.SetActive(true);
            }

            endMsgText.text = "Failed. Press 'Exit' to leave.";
            audioController.PlaySound(onNotCrackedSfx);
            //Destroy(gameObject, 2f);
        }
    }

    public void OnCloseButtonPressed()
    {
        //puzzleCanvas.gameObject.SetActive(false);
        if (isGamePaused)
        {
            return;
        }
        audioController.PlaySound(clickSfx);
        Destroy(gameObject);
    }

    public void SelectNextInputDigit(int currentlySelectedInput)
    {
        audioController.PlaySound(clickSfx);
        inputDigits[((currentlySelectedInput + 1) % inputDigits.Length)].Select();
        //Debug.Log("Input field " + (currentlySelectedInput + 1) % inputDigits.Length + " is selcted.");
    }

    public void OnPauseButtonClicked()
    {
        gameController.OnPauseButtonClicked();
    }

    public void OnGamePaused()
    {
        isGamePaused = true;
    }

    public void OnGameResumed()
    {
        isGamePaused = false;
    }
}
