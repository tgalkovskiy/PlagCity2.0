using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LetterSender : MonoBehaviour
{
    //public string Name;

    [SerializeField] private GameObject LetterPref;

    /// <summary>
    /// Создание нового письма без вариантов ответа
    /// </summary>
    /// <param name="isImmediately">Активируется ли сразу как приходит</param>
    /// <param name="text">Текст письма</param>
    /// <returns></returns>
    public Letter AddLetter(bool isImmediately, int lifeTimeInDays, string senderName, string text, Sprite tutorial = null)
    {
        Letter letter = CreateNewLetter(isImmediately, lifeTimeInDays, senderName, text);

        letter.TutorialSprite = tutorial;
        letter.Answer_1 = new Answer();
        letter.Answer_2 = new Answer();

        return letter;
    }

    /// <summary>
    /// Создание нового письма с вариантами ответа
    /// </summary>
    /// <param name="isImmediately">Активируется ли сразу как приходит</param>
    /// <param name="text">Текст письма</param>
    /// <param name="ans_1">Первый вариант ответа</param>
    /// <param name="ans_2">Второй вариант ответа</param>
    /// <returns></returns>
    public Letter AddLetter(bool isImmediately, int lifeTimeInDays, string senderName, string text, Answer ans_1, Answer ans_2, Sprite tutorial = null)
    {
        Letter letter = CreateNewLetter(isImmediately, lifeTimeInDays, senderName, text);

        letter.TutorialSprite = tutorial;
        letter.Answer_1 = ans_1;
        letter.Answer_2 = ans_2;

        return letter;
    }


    private Letter CreateNewLetter(bool isImmediately, int lifeTimeInDays, string senderName, string text)
    {
        Letter letter = Instantiate(LetterPref, LettersManager.Instance.LettersParent).GetComponent<Letter>();

        letter.LifeTimeInDays = lifeTimeInDays;
        letter.SenderName = senderName;
        letter.mainText = text;
        if (isImmediately)
            LettersManager.Instance.AddLetterToShow(letter);
        else
            LettersManager.Instance.AddLetter(letter);

        return letter;
    }
}
