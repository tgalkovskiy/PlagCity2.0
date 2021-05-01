using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Visitor : MonoBehaviour
{
    public int LifeTimeInDays;

    public string Name;

    public Sprite VisitorSprite;
    public Sprite AnswerSprite;

    public string Text;

    public Answer Answer_1;
    public Answer Answer_2;
    public Answer Answer_3;

    public event Action<Visitor> VisitorSelected;
    public AnswerReaction[] IgnorReactions;
    public string IgnorText;

    public void SelectLetter()
    {
        VisitorSelected?.Invoke(this);
    }

    public void Ignored()
    {
        if (IgnorReactions != null)
        {
            VisitorsManager.Instance.AddReactions(IgnorReactions);
            LettersManager.Instance.itogiText += IgnorText + "\n\n";
        }
    }

    public void OnDelete()
    {
        //if (IsActual)
        //    Ignored();
    }

}
