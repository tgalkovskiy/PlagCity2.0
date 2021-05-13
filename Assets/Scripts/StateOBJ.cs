using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeState
{
    Houses,
    District,
    LocalDistrict,
    City
}

public class StateOBJ : MonoBehaviour
{
    public TypeState TypeStateDis;
    public int CountPeople;
    public int CountInfected = 0;
    public int CountHideInfected = 0;
    public int CountDeath = 0;
    public bool IsLocked = false;
    public bool IsHide = true;
    public bool IsInfected = false;
    public bool AllDead = false;
    public GameObject ViolLine;
    public GameObject DeadIcon;
    public GameObject LockSprite;

    private void Awake()
    {
        CountPeople = Random.Range(7, 12);
    }

    private void Start()
    {
        IsHide = true;
    }

    public void InfectPeople()
    {
        if (IsInfected)
        {
            if ((CountInfected + CountDeath + CountHideInfected) < CountPeople)
            {
                var probabilityV = Random.Range(0, 100);
                if (probabilityV <= MainData.InHouseCoef && !IsLocked)
                {
                    if (!IsHide)
                    {
                        CountInfected += 1;
                        MainData.AllInfected += 1;
                        MainData.NewInfectedPeople += 1;
                    }
                    else
                        CountHideInfected += 1;
                }
            }
        }
    }

    public void TestDeaths()
    {
        if(CountInfected > 0)
        {
            int c = Random.Range(0, 100);
            if (c < 50)
                return;
            else if (c < 80)
            {
                CountInfected -= 1;
                CountDeath += 1;
            }
            else if (c < 10)
            {
                if (CountInfected - 2 >= 0)
                {
                    CountInfected -= 2;
                    CountDeath += 2;
                }
                else
                {
                    CountInfected -= 1;
                    CountDeath += 1;
                }
            }
        }
    }

    public void DeathPeople()
    {
        if (IsInfected)
        {
            if (CountInfected > 0 || CountHideInfected > 0)
            {
                var probabilityD = Random.Range(0, 100);
                if (probabilityD <= MainData.DeathCoef)
                {
                    //локальное увелечение умерших
                    CountDeath += 1;
                    //уменьшение больных
                    if (!IsHide)
                        CountInfected -= 1;
                    else
                        CountHideInfected -= 1;
                    //глобальное увеличение умерших
                    MainData.AllDeath += 1;
                    MainData.UnburiedPeople += 1;
                    MainData.NewDeadPeople += 1;
                    //Если все померли то дом помечается как вымерший
                    if (CountDeath == CountPeople)
                        AllDead = true;

                    SearchHouse();
                    //сброс заражения если в дому нет зараженных
                    if (CountInfected == 0 && CountHideInfected == 0)
                    {
                        IsInfected = false;
                        IsHide = true;
                    }
                }
            }
        }
    }

    public void Recovery()
    {
        if(CountInfected > 0 || CountHideInfected > 0)
        {
            var probabilityR = Random.Range(0, 100);
            if(probabilityR <= MainData.RecoveryCoef)
            {
                if (!IsHide)
                    CountInfected -= 1;
                else
                    CountHideInfected -= 1;
            }

            if (CountInfected == 0 && CountHideInfected == 0)
            {
                IsInfected = false;
                IsHide = true;
            }
        }
    }

    public void DefendLockValanters()
    {
        IsLocked = false;
    }

    public void SearchHouse()
    {
        if (IsHide == false)
            return;

        CountInfected += CountHideInfected;

        MainData.AllInfected += CountHideInfected;
        MainData.NewInfectedPeople += CountHideInfected;

        CountHideInfected = 0;
        IsHide = false;

        UpdateIcons();
    }

    public void UpdateIcons()
    {
        switch(TypeStateDis)
        {
            case TypeState.Houses:
                if (!IsHide && IsInfected)
                {
                    ViolLine.SetActive(true);
                }
                else
                {
                    ViolLine.SetActive(false);
                }
                if (LockSprite != null)
                    if (IsLocked)
                    {
                        LockSprite.SetActive(true);
                    }
                    else
                    {
                        LockSprite.SetActive(false);
                    }

                if(AllDead)
                {
                    DeadIcon.SetActive(true);
                    ViolLine.SetActive(false);
                }
                break;
            case TypeState.District:
                if(CountInfected > 0)
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
