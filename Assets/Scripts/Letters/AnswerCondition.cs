using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    Money,
    Doctor,
    Policeman,
    Volunteer
}

public class AnswerCondition
{
    private ConditionType Type;
    private int Count;

    public bool IsConditionDone { get { return CheckCondition(); } }

    public AnswerCondition() { }
    public AnswerCondition(ConditionType type, int count)
    {
        Type = type;
        Count = count;
    }

    private bool CheckCondition()
    {
        switch(Type)
        {
            case ConditionType.Money:
                if (MainData.Money >= Count)
                    return true;
                break;
            case ConditionType.Doctor:
                if (MainData.Doctors >= Count)
                    return true;
                break;
            case ConditionType.Policeman:
                if (MainData.Policemen >= Count)
                    return true;
                break;
            case ConditionType.Volunteer:
                if (MainData.Volunteers >= Count)
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
            case ConditionType.Money:
                MainData.Money -= Count;
                break;
            case ConditionType.Doctor:
                MainData.Doctors -= Count;
                break;
            case ConditionType.Policeman:
                MainData.Policemen -= Count;
                break;
            case ConditionType.Volunteer:
                MainData.Volunteers -= Count;
                break;
        }
    }
}
