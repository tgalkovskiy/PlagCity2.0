using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public bool IsActual;

    public Sprite ActualSprite;
    public Sprite UnactualSprite;
    public Sprite AnswerActualSprite;
    public Sprite AnswerUnactualSprite;
    public Sprite ActionSprite;

    public string mainText;

    public Answer Answer_1;
    public Answer Answer_2;

    public event Action<Letter> LetterSelected;

    public void ThrowAwayLetter()
    {
        Destroy(gameObject);
    }

    public void OnChooseAnswer_1()
    {
        Answer_1.AnswerChosen();
    }

    public void OnChooseAnswer_2()
    {
        Answer_2.AnswerChosen();
    }
    
    public void SelectLetter()
    {
        LetterSelected?.Invoke(this);
    }

    public void LetterIsBroken()
    {
        //    LetterUnactual.sprite = LetterUnactualSprite;

        //    answer_1.GetComponent<Image>().sprite = LetterAnswerUnactualSprite;
        //    answer_2.GetComponent<Image>().sprite = LetterAnswerUnactualSprite;
    }
}
