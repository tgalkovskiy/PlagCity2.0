using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LetterSender : MonoBehaviour
{
    [SerializeField] private GameObject LetterPref;

    private Letter CreateNewLetter(bool isImmediately, string text)
    {
        Letter letter = Instantiate(LetterPref, LettersManager.Instance.LettersParent).GetComponent<Letter>();

        letter.mainText = text;
        if (isImmediately)
            LettersManager.Instance.AddLetterToShow(letter);
        else
            LettersManager.Instance.AddLetter(letter);

        return letter;
    }

    public Letter AddLetter(bool isImmediately, string text)
    {
        Letter letter = CreateNewLetter(isImmediately, text);

        letter.Answer_1 = new Answer();
        letter.Answer_2 = new Answer();

        return letter;
    }

    public Letter AddLetter(bool isImmediately, string text, Answer ans_1, Answer ans_2)
    {
        Letter letter = CreateNewLetter(isImmediately, text);

        letter.Answer_1 = ans_1;
        letter.Answer_2 = ans_2;

        return letter;
    }
}
