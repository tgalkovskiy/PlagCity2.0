using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    public event Action Chosen;
    public string Text { get; private set; }
    public bool IsConditionDone { get { return CheckCondition(); } }


    public void AnswerChosen()
    {
        Chosen?.Invoke();
    }

    public virtual bool CheckCondition()
    {
        return true;
    }

    public Answer() { Text = ""; }

    public Answer(string text)
    {
        Text = text;
    }
}