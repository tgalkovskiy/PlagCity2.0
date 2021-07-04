using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainData : MonoBehaviour
{
    [HideInInspector] static public int Day;
    //Все умершие люди
    [HideInInspector] static public int AllDeath;
    [HideInInspector] static public int AllInfected;
    //Все не похороненные люди
    [HideInInspector] static public int UnburiedPeople;
    [HideInInspector] static public int Vacina;
    [HideInInspector] static public int Money;
    [HideInInspector] static public int Volunteers;
    [HideInInspector] static public int MaxVolunteers;
    [HideInInspector] static public int Policemen;
    [HideInInspector] static public int MaxPolicemen;
    [HideInInspector] static public int Doctors;
    [HideInInspector] static public int MaxDoctors;
    [HideInInspector] static public int ImperatorReputation;
    [HideInInspector] static public int MinImperatorReputation;
    [HideInInspector] static public int WorkersReputation;
    [HideInInspector] static public int MinWorkersReputation;
    [HideInInspector] public static int WorkersRepPerDay;
    [HideInInspector] static public int RichReputation;
    [HideInInspector] static public int MinRichReputation;
    [HideInInspector] static public int PoorReputation;
    [HideInInspector] static public int MinPoorReputation;

    [HideInInspector] static public int AllPeople;

    [HideInInspector] static public int NewInfectedPeople;
    [HideInInspector] static public int NewDeadPeople;

    public static int MoneyPerRichDistrict;
    public static int MoneyPerRiotRichDistrict;

    public static int InHouseCoef;
    public static int NewHouseCoef;
    public static int NewHouseDefaultCoef;
    public static int NewHouseDopCoef;
    public static int AnotherDistrictCoef;
    public static int AnotherDistrictDefaultCoef;
    public static int AnotherDistrictDopCoef;
    public static int DeathCoef;
    public static int RecoveryCoef;

    public static int BreadPrice;
    public static int RepPerBread;

    public static int RepPerSearch;
    public static int RepPerLockHouse;
    public static int RepPerLockRoad;

    public static bool IsFirstRiot;

    public static int DayTimeScaleDefault;
    public static int DayTimeScale;

    public static int preAverageCoef;
    public static int preImperatorRep;
    public static int preRichRep;
    public static int preWorkersRep;

    public static int savedHouses;

    private void Awake()
    {
        Reload();
    }

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
        WorkersReputation = 50;
        MinWorkersReputation = 0;
        WorkersRepPerDay = 5;
        RichReputation = 60;
        MinRichReputation = 20;
        PoorReputation = 40;
        MinPoorReputation = 20;
        NewInfectedPeople = 0;
        NewDeadPeople = 0;
        MoneyPerRichDistrict = 60;
        MoneyPerRiotRichDistrict = 20;
        NewHouseCoef = 70;
        InHouseCoef = 85;
        NewHouseDefaultCoef = 70;
        NewHouseDopCoef = 0;
        AnotherDistrictCoef = 15;
        AnotherDistrictDefaultCoef = 15;
        AnotherDistrictDopCoef = 0;
        DeathCoef = 45;
        RecoveryCoef = 0;
        BreadPrice = 30;
        RepPerBread = 20;
        RepPerSearch = 5;
        RepPerLockHouse = 3;
        RepPerLockRoad = 5;
        IsFirstRiot = false;
        DayTimeScaleDefault = 7;
        DayTimeScale = 7;
        preAverageCoef = 0;
        preImperatorRep = 0;
        preRichRep = 0;
        preWorkersRep = 0;
        MinImperatorReputation = 10;
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

        preAverageCoef = (NewHouseCoef + AnotherDistrictCoef) / 2;

        NewHouseCoef = NewHouseDefaultCoef;
        AnotherDistrictCoef = AnotherDistrictDefaultCoef;

        NewHouseCoef += Mathf.CeilToInt(UnburiedPeople / 10) * 3;
        AnotherDistrictCoef += Mathf.CeilToInt(UnburiedPeople / 10) * 2;

        ItogiUI.Instance.ShowCoef();

        NewHouseCoef += NewHouseDopCoef;
        AnotherDistrictCoef += AnotherDistrictDopCoef;



        NewHouseDopCoef = 0;
        AnotherDistrictDopCoef = 0;

        NewInfectedPeople = 0;
        NewDeadPeople = 0;

        savedHouses = 0;

    }


    public static void PreStatistics()
    {
        preImperatorRep = ImperatorReputation;
        preRichRep = RichReputation;
        preWorkersRep = WorkersReputation;
    }

    public static int CheckClamp(int i)
    {
        if (i < 0)
            return 0;

        if (i > 100)
            return 100;

        return i;
    }
}
