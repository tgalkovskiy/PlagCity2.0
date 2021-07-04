using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactionType
{
    Money,
    ImperatorRep,
    WorkersRep,
    RichRep,
    PoorRep,
    Vacina,
    CoefInfectInDistrict,
    CoefInfectOutDistrict,
    Volunteer
}

public class AnswerReaction
{
    private ReactionType Type;
    private int Count;

    public AnswerReaction() { }

    /// <summary>
    /// Money, ImperatorRep, WorkersRep, RichRep, PoorRep, Vacina, Volunteer
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public AnswerReaction(ReactionType type, int count) 
    {
        Type = type;
        Count = count;
    }

    public void ApplyReaction()
    {
        switch (Type)
        {
            case ReactionType.Money:
                MainData.Money += Count;
                break;
            case ReactionType.ImperatorRep:
                MainData.ImperatorReputation += Count;
                MainData.ImperatorReputation = MainData.CheckClamp(MainData.ImperatorReputation);
                break;
            case ReactionType.WorkersRep:
                MainData.WorkersReputation += Count;
                MainData.WorkersReputation = MainData.CheckClamp(MainData.WorkersReputation);
                break;
            case ReactionType.RichRep:
                MainData.RichReputation += Count;
                MainData.RichReputation = MainData.CheckClamp(MainData.RichReputation);
                break;
            case ReactionType.PoorRep:
                MainData.PoorReputation += Count;
                MainData.PoorReputation = MainData.CheckClamp(MainData.PoorReputation);
                break;
            case ReactionType.Vacina:
                MainData.Vacina += Count;
                break;
            case ReactionType.CoefInfectInDistrict:
                MainData.NewHouseDopCoef += Count;
                break;
            case ReactionType.CoefInfectOutDistrict:
                MainData.AnotherDistrictDopCoef += Count;
                break;
            case ReactionType.Volunteer:
                MainData.MaxVolunteers += Count;
                MainData.Volunteers += Count;
                if(MainData.MaxVolunteers < 0)
                {
                    MainData.MaxVolunteers = 0;
                    MainData.Volunteers = 0;
                }

                MainScript.Instance.UpdateUI();
                break;
        }
    }
    

}