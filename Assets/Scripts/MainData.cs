using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainData : MonoBehaviour
{
    [HideInInspector] static public int Day = 1;
    //Все умершие люди
    [HideInInspector] static public int AllDeath=0;
    [HideInInspector] static public int AllInfected = 0;
    //Все не похороненные люди
    [HideInInspector] static public int UnburiedPeople = 0;
    [HideInInspector] static public int Vacina = 50;
    [HideInInspector] static public int Money = 50;
    [HideInInspector] static public int Volunteers = 2;
    [HideInInspector] static public int MaxVolunteers = 2;
    [HideInInspector] static public int Policemen = 0;
    [HideInInspector] static public int MaxPolicemen = 0;
    [HideInInspector] static public int Doctors = 0;
    [HideInInspector] static public int MaxDoctors = 0;
    [HideInInspector] static public int ImperatorReputation = 10;
    [HideInInspector] static public int WorkersReputation = 60;
    [HideInInspector] static public int MinWorkersReputation = 10;
    [HideInInspector] public static int WorkersRepPerDay = 5;
    [HideInInspector] static public int RichReputation = 60;
    [HideInInspector] static public int MinRichReputation = 20;
    [HideInInspector] static public int PoorReputation = 40;
    [HideInInspector] static public int MinPoorReputation = 20;

    [HideInInspector] static public int AllPeople;

    [HideInInspector] static public int NewInfectedPeople = 0;
    [HideInInspector] static public int NewDeadPeople = 0;

    public static int MoneyPerRichDistrict = 60;
    public static int MoneyPerRiotRichDistrict = 20;

    public static int NewHouseCoef = 85;
    public static int InHouseCoef = 95;
    public static int InHouseDefaultCoef = 95;
    public static int InHouseDopCoef = 0;
    public static int AnotherDistrictCoef = 25;
    public static int AnotherDistrictDefaultCoef = 25;
    public static int AnotherDistrictDopCoef = 0;
    public static int DeathCoef = 45;
    public static int RecoveryCoef = 0;

    public static int BreadPrice = 200;
    public static int RepPerBread = 20;

    public static int RepPerSearch = 6;
    public static int RepPerLockHouse = 4;
    public static int RepPerLockRoad = 6;

    public static bool IsFirstRiot = false;

    public static int DayTimeScaleDefault = 10;
    public static int DayTimeScale = 10;
    

    public static void Reload()
    {
        Day = 1;
        AllDeath = 0;
        AllInfected = 0;
        UnburiedPeople = 0;
        Vacina = 50;
        Money = 50;
        Volunteers = 2;
        MaxVolunteers = 2;
        Policemen = 0;
        MaxPolicemen = 0;
        Doctors = 0;
        MaxDoctors = 0;
        ImperatorReputation = 10;
        WorkersReputation = 60;
        MinWorkersReputation = 10;
        WorkersRepPerDay = 5;
        RichReputation = 60;
        MinRichReputation = 20;
        PoorReputation = 40;
        MinPoorReputation = 20;
        NewInfectedPeople = 0;
        NewDeadPeople = 0;
        MoneyPerRichDistrict = 60;
        MoneyPerRiotRichDistrict = 20;
        NewHouseCoef = 85;
        InHouseCoef = 95;
        InHouseDefaultCoef = 95;
        InHouseDopCoef = 0;
        AnotherDistrictCoef = 25;
        AnotherDistrictDefaultCoef = 25;
        AnotherDistrictDopCoef = 0;
        DeathCoef = 45;
        RecoveryCoef = 0;
        BreadPrice = 200;
        RepPerBread = 20;
        RepPerSearch = 6;
        RepPerLockHouse = 4;
        RepPerLockRoad = 6;
        IsFirstRiot = false;
        DayTimeScaleDefault = 10;
        DayTimeScale = 10;
    }


    public static void OnNewDay()
    {
        if (MaxVolunteers < 0)
            MaxVolunteers = 0;

        if (MaxPolicemen < 0)
            MaxPolicemen = 0;

        if (MaxDoctors < 0)
            MaxDoctors = 0;

        Volunteers = MaxVolunteers;
        Policemen = MaxPolicemen;
        Doctors = MaxDoctors;


        DayTimeScale = DayTimeScaleDefault;

        InHouseCoef = InHouseDefaultCoef;
        AnotherDistrictCoef = AnotherDistrictDefaultCoef;

        InHouseCoef += Mathf.CeilToInt(UnburiedPeople / 10) * 3;
        AnotherDistrictCoef += Mathf.CeilToInt(UnburiedPeople / 10) * 2;

        InHouseCoef += InHouseDopCoef;
        AnotherDistrictCoef += AnotherDistrictDopCoef;

        InHouseDopCoef = 0;
        AnotherDistrictDefaultCoef = 0;

        NewInfectedPeople = 0;
        NewDeadPeople = 0;

        Debug.Log($"Poor rep:{PoorReputation};Workers rep:{WorkersReputation};Rich rep:{RichReputation}");
    }
}
