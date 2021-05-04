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
                if (MainData.Money >= Count)
                    return true;
                break;
            case "Doctor":
                if (MainData.Doctors >= Count)
                    return true;
                break;
            case "Policeman":
                if (MainData.Policemen >= Count)
                    return true;
                break;
            case "Volunteer":
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
            case "Money":
                MainData.Money -= Count;
                break;
            case "Doctor":
                MainData.Doctors -= Count;
                break;
            case "Policeman":
                MainData.Policemen -= Count;
                break;
            case "Volunteer":
                MainData.Volunteers -= Count;
                break;
        }
    }
}
