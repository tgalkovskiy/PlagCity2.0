using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStat : MonoBehaviour
{

    //Все умершие люди
    [HideInInspector]static public int AllDeath=0;
    //Все похороненные люди
    [HideInInspector] static public int BuriedPeople;
    [HideInInspector] static public int Vacina = 50;
    [HideInInspector] static public int Money = 0;
}
