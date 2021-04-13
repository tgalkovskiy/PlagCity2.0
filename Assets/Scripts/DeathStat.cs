using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathStat : MonoBehaviour
{
    [HideInInspector] static public int Day = 1;
    //Все умершие люди
    [HideInInspector] static public int AllDeath=0;
    //Все похороненные люди
    [HideInInspector] static public int BuriedPeople;
    [HideInInspector] static public int Vacina = 50;
    [HideInInspector] static public int Money = 0;
    [HideInInspector] static public int Volunteers = 3;
    [HideInInspector] static public int MaxVolunteers = 3;
    [HideInInspector] static public int Policemen = 2;
    [HideInInspector] static public int MaxPolicemen = 2;
    [HideInInspector] static public int Doctors = 3;
    [HideInInspector] static public int MaxDoctors = 3;
    [HideInInspector] static public int ImperatorReputation = 60;
    [HideInInspector] static public int RegionReputation = 40;


    public static void OnNewDay()
    {
        Volunteers = MaxVolunteers;
        Policemen = MaxPolicemen;
        Doctors = MaxDoctors;
    }
}
