using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public enum Type { letter, human}

    public bool IsActual;
    public int LifeTimeInDays;

    public string SenderName;

    public Sprite ActualSprite;
    public Sprite UnactualSprite;
    public Sprite AnswerActualSprite;
    public Sprite AnswerUnactualSprite;
    public Sprite ActionSprite;

    public string mainText;

    public Answer Answer_1;
    public Answer Answer_2;

    public event Action<Letter> LetterSelected;
    public AnswerReaction[] IgnorReactions;
    public string IgnorText;
    
    public void SelectLetter()
    {
        LetterSelected?.Invoke(this);
    }

    public void Ignored()
    {
        IsActual = false;

        if (IgnorReactions != null)
        {
            LettersManager.Instance.AddReactions(IgnorReactions);
            LettersManager.Instance.itogiText += IgnorText + "\n";
        }
    }

    public void OnDelete()
    {
        //if (IsActual)
        //    Ignored();
    }
}
