using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeCodeHistoryItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] inputDigits;

    private int numOfCorrectDigits = 0;

    private bool ArrContainsDigit(int[] arr, int digit)
    {
        foreach (int inputDigit in arr)
        {
            if (digit == inputDigit)
            {
                return true;
            }
        }
        return false;
    }

    public void Init(int[] codeEntered, int[] actualCode)
    {
        for (int i = 0; i < actualCode.Length; i++)
        {
            inputDigits[i].text = codeEntered[i].ToString();

            if (codeEntered[i] == actualCode[i])
            {
                inputDigits[i].color = Color.green;
                numOfCorrectDigits++;
                continue;
            }

            if (ArrContainsDigit(actualCode, codeEntered[i]))
            {
                inputDigits[i].color = new Color32(255, 138, 101, 255);
                continue;
            }

            inputDigits[i].color = Color.red;            
        }
    }

    public bool IsEnteredCodeCorrect()
    {
        return numOfCorrectDigits == inputDigits.Length;
    } 
}


