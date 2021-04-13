﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeState
{
    Houses,
    Districd,
    City
}
public class StateOBJ : MonoBehaviour
{
    public TypeState TypeStateDis;
    public int CountPeople;
    public int CountViol = 0;
    public int CountDeath = 0;
    public bool Lock = false;
    public bool StateViol = false;
    public bool AllDead = false;
    public GameObject ViolLine;
    public GameObject LockSprite;

    private void Awake()
    {
        CountPeople = Random.Range(7, 12);
    }
    public void ViolPeople()
    {
        if (StateViol)
        {
            if ((CountViol + CountDeath) < CountPeople)
            {
                var probabilityV = Random.Range(0, 100);
                if (probabilityV <= 85 && !Lock)
                {
                    CountViol += 1;
                }
            }
        }
    }
    public void DeathPeople()
    {
        if (StateViol)
        {
            if (CountViol > 0)
            {
                var probabilityD = Random.Range(0, 100);
                if (probabilityD <= 65)
                {
                    //локальное увелечение умерших
                    CountDeath += 1;
                    //уменьшение больных
                    CountViol -= 1;
                    //глобальное увеличение умерших
                    DeathStat.AllDeath += 1;
                    //Если все померли то дом помечается как вымерший
                    if (CountDeath == CountPeople)
                        AllDead = true;
                    //сброс заражения если в дому нет зараженных
                    if (CountViol == 0)
                    {
                        StateViol = false;
                    }
                }
            }
        }
    }
    public void Recovery()
    {
        if(CountViol > 0)
        {
            var probabilityR = Random.Range(0, 100);
            if(probabilityR <= 25)
            {
                CountViol -= 1;
            }

            if (CountViol == 0)
            {
                StateViol = false;
            }
        }
    }
    public void DefendLockValanters()
    {
        Lock = false;
    }

    public void IconViol()
    {
        switch(TypeStateDis)
        {
            case TypeState.Houses:
                if (CountDeath > 0 && StateViol)
                {
                    ViolLine.SetActive(true);
                }
                else
                {
                    ViolLine.SetActive(false);
                }

                if (LockSprite != null)
                    if (Lock)
                    {
                        LockSprite.SetActive(true);
                    }
                    else
                    {
                        LockSprite.SetActive(false);
                    }
                break;
            case TypeState.Districd:
                if(CountViol > 0)
                {
                    ViolLine.SetActive(true);
                }
                else
                {
                    ViolLine.SetActive(false);
                }
                break;
        }
    }
}
