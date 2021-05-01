using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    public event Action Chosen;
    public string Text { get; private set; }
    public bool IsConditionsDone { get { return CheckCondition(); } }
    public AnswerCondition[] Conditions;
    public AnswerReaction[] Reactions;
    public string ReactionText;

    public void AnswerChosen()
    {
        Chosen?.Invoke();

        if (Conditions != null)
            LettersManager.Instance.AddConditions(Conditions);

        if (Reactions != null)
        {
            LettersManager.Instance.AddReactions(Reactions);
        }

        if (ReactionText != "" && ReactionText != null)
        {
            LettersManager.Instance.itogiText += ReactionText + "\n\n";
            Debug.Log($"{ReactionText}");
        }
    }

    public virtual bool CheckCondition()
    {
        if (Conditions == null)
            return true;

        foreach (var c in Conditions)
            if (!c.IsConditionDone)
                return false;

        return true;
    }

    public Answer() { Text = ""; }

    public Answer(string text)
    {
        Text = text;
    }
}