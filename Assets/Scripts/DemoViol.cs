using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public enum DistrictType 
{
    Rich,
    Workers,
    Poor
}

public class DemoViol : MonoBehaviour
{
    public DistrictType type;
    [SerializeField] public GameObject[] Houses;
    //перечень элементов в районе 
    [SerializeField] private MainScript MainScript;
    //отоброженеи и переход в новый день
    private int CountInfectedHouses = 0;
    public bool InfectOnStart, ActiveDistrict = false;
    private StateOBJ DistricdStateObj1;
    [SerializeField] public StateOBJ DistricdStateObj2;
    //public int CorpsCount = 0;
    private List<StateOBJ> HousesToInfect = new List<StateOBJ>();
    private List<StateOBJ> NeighbourHousesToInfect = new List<StateOBJ>();

    [SerializeField] private LocksRoad[] Roads;
    [SerializeField] private DemoViol[] NeighbourDistricts;

    public bool IsOnSearch = false;
    public bool IsRiot = false;
    public bool IsOnSuppressRiot = false;
    public bool IsGivenBread = false;

    public Transform RiotButtonPointMap;
    public Transform RiotButtonPointDistrict;

    public Transform SearchButtonPointMap;
    public Transform SearchButtonPointDistrict;

    public Transform BreadButtonPointMap;
    public Transform BreadButtonPointDistrict;

    public Transform LoupeButtonPointMap;
    public Transform LoupeButtonPointDistrict;

    public GameObject RiotGO;

    public Sprite RiotSprite;
    public Sprite UnRiotSprite;

    public bool InfectAll;

    private void Awake()
    {
        DistricdStateObj1 = GetComponent<StateOBJ>();
    }

    private void Start()
    {
        if (InfectOnStart)
        {
            InfectDistrict();
        }

        if(InfectAll)
        {
            foreach(var h in Houses)
            {
                var house = h.GetComponent<StateOBJ>();
                house.CountInfected = house.CountPeople;
            }
        }

        DistricdState();
        IsRiot = false;
        IsOnSuppressRiot = false;
        IsOnSearch = false;
        if(RiotGO != null)
            RiotGO.SetActive(false);
    }

    public void ActivateRoads()
    {
        foreach(var r in Roads)
        {
            r.Activate();
        }
    }

    public void DeactivateRoads()
    {
        foreach (var r in Roads)
        {
            r.Deactivate();
        }
    }

    public void InfectDistrict()
    {
        for (int i = 0; i < 3; i++)
        {
            int I = Random.Range(0, Houses.Length);
            if (Houses[I].GetComponent<StateOBJ>().IsInfected)
            {
                i--;
                continue;
            }
            Houses[I].GetComponent<StateOBJ>().IsInfected = true;
            Houses[I].GetComponent<StateOBJ>().CountHideInfected += 3;
            Debug.Log($"{this.name}, {Houses[I].GetComponent<StateOBJ>().name} infected");
        }
    }


    private bool IsRepPerRoadDone = false;

    public void NextDayStateObj()
    {
        if (type == DistrictType.Rich)
        {

            //расчет аффекта
            CalculateAffect();
            //передача в новый дом
            for (int i = 0; i < CountInfectedHouses; i++)
            {
                if (HousesToInfect.Count > 0)
                {
                    //Расчет случайного дома
                    var NowObj = HousesToInfect[Random.Range(0, HousesToInfect.Count)];
                    //расчет шанса заражения
                    var probabilityV_H = Random.Range(0, 100);
                    if (!NowObj.IsLocked && probabilityV_H <= MainData.NewHouseCoef)
                    {
                        //заражение нового дома
                        NowObj.IsInfected = true;
                        //добавление больных
                        int freePeople = NowObj.CountPeople - NowObj.CountDeath - NowObj.CountInfected - NowObj.CountHideInfected;
                        if (freePeople >= 3)
                            NowObj.CountHideInfected += Random.Range(2, 4);
                        else
                            NowObj.CountHideInfected += freePeople;
                        //удаление из списка не зараженных домов
                        HousesToInfect.Remove(NowObj);
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < CountInfectedHouses; i++)
            {
                int neighbourNum = Random.Range(0, NeighbourDistricts.Length);
                var neighbour = NeighbourDistricts[neighbourNum];
                if (neighbour.Roads[neighbourNum].IsActive)
                {
                    if (!IsRepPerRoadDone)
                    {
                        IsRepPerRoadDone = true;
                        switch (type)
                        {
                            case DistrictType.Poor:
                                MainData.PoorReputation -= MainData.RepPerLockRoad;
                                break;
                            case DistrictType.Workers:
                                MainData.WorkersReputation -= MainData.RepPerLockRoad;
                                break;
                            case DistrictType.Rich:
                                MainData.RichReputation -= MainData.RepPerLockRoad;
                                break;
                        }
                    }
                    continue;
                }

                foreach (var h in neighbour.Houses)
                {
                    var NowHouses = h.GetComponent<StateOBJ>();
                    if (!NowHouses.IsInfected && !NowHouses.AllDead && !NowHouses.IsLocked)
                        NeighbourHousesToInfect.Add(NowHouses);
                }

                if (NeighbourHousesToInfect.Count > 0)
                {
                    //Расчет случайного дома
                    var NowObj = NeighbourHousesToInfect[Random.Range(0, NeighbourHousesToInfect.Count)];
                    //расчет шанса заражения
                    var probabilityV_H = Random.Range(0, 100);
                    if (!NowObj.IsLocked && probabilityV_H <= MainData.AnotherDistrictCoef)
                    {
                        //заражение нового дома
                        NowObj.IsInfected = true;
                        //добавление больных
                        int freePeople = NowObj.CountPeople - NowObj.CountDeath - NowObj.CountInfected - NowObj.CountHideInfected;
                        if (freePeople >= 3)    
                            NowObj.CountHideInfected += Random.Range(2,4);
                        else
                            NowObj.CountHideInfected += freePeople;
                        //удаление из спика не зараженных домов 
                        NeighbourHousesToInfect.Remove(NowObj);
                    }
                }
                else
                {
                    break;
                }
            }


            CountInfectedHouses = 0;
            //очистка списков
            HousesToInfect.Clear();
            NeighbourHousesToInfect.Clear();
            foreach (var r in Roads)
                r.IsActive = false;
            //Действия внутри домов
            for (int i = 0; i < Houses.Length; i++)
            {
                var NowObj = Houses[i]?.GetComponent<StateOBJ>();
                //Если дом вымерший, не надо ничего делать
                if (NowObj.AllDead)
                    continue;

                //Лечим
                NowObj.Recovery();
                //Убиваем
                NowObj.DeathPeople();
                //Заражаем
                NowObj.InfectPeople();
                //Снимаем блок
                if (NowObj.IsLocked)
                {
                    switch (type)
                    {
                        case DistrictType.Poor:
                            MainData.PoorReputation -= MainData.RepPerLockHouse;
                            break;
                        case DistrictType.Workers:
                            MainData.WorkersReputation -= MainData.RepPerLockHouse;
                            break;
                        case DistrictType.Rich:
                            MainData.RichReputation -= MainData.RepPerLockHouse;
                            break;
                    }
                    NowObj.DefendLockValanters();
                }
                //показываем зараженные дома
                NowObj.UpdateIcons();
            }
        }
        else
        {
            foreach (var h in Houses)
                h.GetComponent<StateOBJ>().TestDeaths();
        }

        if(IsGivenBread == true)
        {
            IsGivenBread = false;
            switch (type)
            {
                case DistrictType.Poor:
                    MainData.PoorReputation += MainData.RepPerBread;
                    break;
                case DistrictType.Workers:
                    MainData.WorkersReputation += MainData.RepPerBread;
                    break;
                case DistrictType.Rich:
                    MainData.RichReputation += MainData.RepPerBread;
                    break;

            }
        }

        DistricdState();
    }


    /// <summary>
    /// Подсчет аффекта - сколько домов внутри района и соседей надо заразить
    /// </summary>

    private int CalculateAffect()
    {
        for (int i = 0; i < Houses.Length; i++)
        {
            var NowHouses = Houses[i].GetComponent<StateOBJ>();
            if (NowHouses.IsInfected && !NowHouses.IsLocked && NowHouses.CountDeath > 0) 
            {
                CountInfectedHouses += 1;
            }
            else if(!NowHouses.IsInfected && !NowHouses.AllDead && !NowHouses.IsLocked)
            {
                HousesToInfect.Add(NowHouses);
            }
        }
        if (IsRiot == true)
        {
            CountInfectedHouses += 2;
        }
        return CountInfectedHouses;
    }

    public void DistricdState()
    {
        IsRepPerRoadDone = false;
        int CountPeopleDistricd = 0;
        int CountDeathDistricd = 0;
        int CountInfectedDistricd = 0;
        for(int i=0; i < Houses.Length; i++)
        {
            CountPeopleDistricd += Houses[i].GetComponent<StateOBJ>().CountPeople;
            CountDeathDistricd += Houses[i].GetComponent<StateOBJ>().CountDeath;
            CountInfectedDistricd += Houses[i].GetComponent<StateOBJ>().CountInfected;
        }
        //данные для района внутри
        DistricdStateObj1.CountPeople = CountPeopleDistricd;
        DistricdStateObj1.CountDeath = CountDeathDistricd;
        DistricdStateObj1.CountInfected = CountInfectedDistricd;
        //данные для глобальной карты
        DistricdStateObj2.CountPeople = CountPeopleDistricd;
        DistricdStateObj2.CountDeath = CountDeathDistricd;
        DistricdStateObj2.CountInfected = CountInfectedDistricd;
        DistricdStateObj2.UpdateIcons();
        //расчет степени заражения
        float powerColor = (float)((CountInfectedDistricd + CountDeathDistricd) * 2)/(float)CountPeopleDistricd;
        //изменение альфы района на шлобальной карте
        DistricdStateObj2.ViolLine.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, powerColor);
    }


    public void SearchDistrict()
    {
        if (MainData.Volunteers == 0)
            return;

        IsOnSearch = true;
        MainScript.districsToSearch.Add(this);
    }

    public void UnsearchDistrict()
    {
        IsOnSearch = false;
        MainScript.districsToSearch.Remove(this);
    }

    public void SuppressRiot()
    {
        IsRiot = false;
        GetComponent<SpriteRenderer>().sprite = UnRiotSprite;
        RiotGO.SetActive(false);
        IsOnSuppressRiot = false;
        MainScript.RichUnriotDistricts.Add(this);
    }

    public void MakeRiot()
    {
        IsRiot = true;
        GetComponent<SpriteRenderer>().sprite = RiotSprite;
        RiotGO.SetActive(true);
        IsOnSuppressRiot = false;
    }
}

    
