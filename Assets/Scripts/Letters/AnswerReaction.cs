using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerReaction
{
    private string Type;
    private int Count;

    public AnswerReaction() { }

    /// <summary>
    /// Money, ImperatorRep, WorkersRep, RichRep, PoorRep, Vacina, Volunteer
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public AnswerReaction(string type, int count) 
    {
        Type = type;
        Count = count;
    }

    public void ApplyReaction()
    {
        switch (Type)
        {
            case "Money":
                MainData.Money += Count;
                break;
            case "ImperatorRep":
                MainData.ImperatorReputation += Count;
                break;
            case "WorkersRep":
                MainData.WorkersReputation += Count;
                break;
            case "RichRep":
                MainData.RichReputation += Count;
                break;
            case "PoorRep":
                MainData.PoorReputation += Count;
                break;
            case "Vacina":
                MainData.Vacina += Count;
                break;
            case "InfectIn":
                MainData.InHouseDopCoef += Count;
                break;
            case "InfectOut":
                MainData.AnotherDistrictDopCoef += Count;
                break;
            case "Volunteer":
                MainData.MaxVolunteers += Count;
                MainData.Volunteers += Count;
                MainScript.Instance.UpdateUI();
                break;
        }
    }
}