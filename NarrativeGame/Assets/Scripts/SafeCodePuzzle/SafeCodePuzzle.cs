using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    
    private int guessesRemaining;
    private int[] code = new int[3];
    private int[] codeEntered = new int[3];
    private List<int> numsAvailableForCode = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private bool isCodeCracked = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<code.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, numsAvailableForCode.Count);
            code[i] = numsAvailableForCode[randomIndex];
            numsAvailableForCode.RemoveAt(randomIndex);

            inputDigits[i].text = string.Empty;
        }
        guessesRemaining = 5;

        //inputDigit2.Select();
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

    public void OnCodeEntered()
    {        
        if (isCodeCracked || !IsEnteredCodeValid())
        {
            return;
        }

        guessesRemaining--;

        SafeCodeHistoryItem instantiatedHistoryItem = Instantiate(historyItemPrefab, historyPanel, false);
        instantiatedHistoryItem.Init(codeEntered, code);

        foreach (var inputDigit in inputDigits)
        {
            inputDigit.text = string.Empty;
        }

        if (instantiatedHistoryItem.IsEnteredCodeCorrect())
        {
            Debug.Log("Successfully cracked the code.");
            isCodeCracked = true;
            return;
        }

        if (guessesRemaining <= 0)
        {
            Debug.Log("Couldn't guess code. Code was: "+code.ToString());
            Destroy(gameObject, 2f);
        }
    }

    public void OnCloseButtonPressed()
    {
        puzzleCanvas.gameObject.SetActive(false);
    }

    public void SelectNextInputDigit(int currentlySelectedInput)
    {
        inputDigits[(currentlySelectedInput + 1) % inputDigits.Length].Select();
    }

}
