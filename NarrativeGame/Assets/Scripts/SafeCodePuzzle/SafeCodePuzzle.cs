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
    private SafeCodeHistoryItem historyItemPrefab;

    [SerializeField]
    private InputActionReference codeEnterInputAction;
    
    private int guessesRemaining;
    private int[] code = new int[3];
    private int[] codeEntered = new int[3];
    private List<int> numsAvailableForCode = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private bool isCodeCracked = false;
    private string codeString = "";
    private Safe instantiatingSafe;

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

        codeEnterInputAction.action.performed += (ctx) =>
        {
            OnCodeEntered();
        };

        inputDigits[0].ActivateInputField();
        guessesRemaining = 5;
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

    private void OnDestroy()
    {
        instantiatingSafe.OnSafeMinigameClosed();
    }

    public void Init(Safe instantiatingSafe)
    {
        this.instantiatingSafe = instantiatingSafe;
    }

    public void OnCodeEntered()
    {
        Debug.Log("Inside on code entered.");
        if (isCodeCracked || !IsEnteredCodeValid())
        {
            inputDigits[0].ActivateInputField();
            return;
        }

        guessesRemaining--;

        SafeCodeHistoryItem instantiatedHistoryItem = Instantiate(historyItemPrefab, historyPanel, false);
        instantiatedHistoryItem.Init(codeEntered, code);

        for (int i = 0; i < inputDigits.Length; i++)
        {
            ResetInputField(i);
        }
        inputDigits[0].ActivateInputField();

        if (instantiatedHistoryItem.IsEnteredCodeCorrect())
        {
            Debug.Log("Successfully cracked the code. Codestring: "+codeString);
            isCodeCracked = true;
            instantiatingSafe.OnSafeCracked(codeString);
            return;
        }

        if (guessesRemaining <= 0)
        {
            Debug.Log("Couldn't guess code. Code was: "+codeString);
            Destroy(gameObject, 2f);
        }
    }

    public void OnCloseButtonPressed()
    {
        //puzzleCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SelectNextInputDigit(int currentlySelectedInput)
    {
        inputDigits[((currentlySelectedInput + 1) % inputDigits.Length)].Select();
        //Debug.Log("Input field " + (currentlySelectedInput + 1) % inputDigits.Length + " is selcted.");
    }

}
