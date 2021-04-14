using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCondition
{
    private string Type;
    private int Count;

    public bool IsConditionDone { get { return CheckCondition(); } }

    public AnswerCondition() { }
    public AnswerCondition(string type, int count)
    {
        Type = type;
        Count = count;
    }

    private bool CheckCondition()
    {
        switch(Type)
        {
            case "Money":
                if (DeathStat.Money >= Count)
                    return true;
                break;
            case "Doctor":
                if (DeathStat.Doctors >= Count)
                    return true;
                break;
            case "Policeman":
                if (DeathStat.Policemen >= Count)
                    return true;
                break;
            case "Volonteer":
                if (DeathStat.Volunteers >= Count)
                    return true;
                break;
            default:
                Debug.LogError($"Несуществующий тип условия: \"{Type}\".");
                break;
        }

        return false;
    }

    public void ApplyCondition()
    {
        switch (Type)
        {
            case "Money":
                DeathStat.Money -= Count;
                break;
            case "Doctor":
                DeathStat.Doctors -= Count;
                break;
            case "Policeman":
                DeathStat.Policemen -= Count;
                break;
            case "Volonteer":
                DeathStat.Volunteers -= Count;
                break;
        }
    }
}
