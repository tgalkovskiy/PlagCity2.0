using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerReaction
{
    private string Type;
    private int Count;

    public AnswerReaction() { }
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
                DeathStat.Money += Count;
                break;
            case "ImperatorRep":
                DeathStat.ImperatorReputation += Count;
                break;
            case "RegionRep":
                DeathStat.RegionReputation += Count;
                break;
            case "Vacina":
                DeathStat.Vacina += Count;
                break;
            case "Volonteer":
                DeathStat.MaxVolunteers += Count;
                DeathStat.Volunteers += Count;
                MainScript.Instance.UpdateUI();
                break;
        }
    }
}