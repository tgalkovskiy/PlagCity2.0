using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DemoViol : MonoBehaviour
{

    [SerializeField] public GameObject[] Houses;
    //перечень элементов в районе 
    [SerializeField] private MainScript MainScript;
    //отоброженеи и переход в новый день
    private int CountViolHouses = 0;
    [SerializeField] public bool ThisViol, ActiveDistrict = false;
    //[SerializeField] private DemoViol District2;
    //[SerializeField] private DemoViol District3;
    private GameObject ViolHouses;
    private StateOBJ DistricdStateObj1;
    [SerializeField] public StateOBJ DistricdStateObj2;
    private int TodayViol = 0;
    private int DistrictCoef = 70;
    private int HouseCoef = 85;
    private int DeatdhCoef = 65;
    private int HealCoef = 25;
    private int AnotherDistrCoef = 35;
    public int CorpsCount = 0;
    private int NumberHouse;
    private List<StateOBJ> HousesToViol = new List<StateOBJ>();
    private void Awake()
    {
        DistricdStateObj1 = GetComponent<StateOBJ>();
        //заражение одного рандомного дома
        NumberHouse = Random.Range(0, Houses.Length);
        ViolHouses = Houses[NumberHouse];
    }

    private void Start()
    {
        if (ThisViol)
        {
            int I = Random.Range(0, Houses.Length);
            Houses[I].GetComponent<StateOBJ>().StateViol = true;
            Houses[I].GetComponent<StateOBJ>().CountViol += 3;
            Debug.Log($"House {I} in {DistricdStateObj2} is viol now");
        }
        DistricdState();
    }

    public void NextDayStateObj()
    {
        //расчет аффекта
        CalculateAffect();
        Debug.Log($"Houses to viol count = {HousesToViol.Count}");
        //передача в новый дом
        if (HousesToViol.Count>0)
        {
            for (int i = 0; i < CountViolHouses; i++)
            {
                if (HousesToViol.Count > 0)
                {
                    //Расчет случайного дома
                    var NowObj = HousesToViol[Random.Range(0, HousesToViol.Count)];
                    //расчет шанса заражения
                    var probabilityV_H = Random.Range(0, 100);
                    if (!NowObj.Lock && probabilityV_H <= 70)
                    {
                        //заражение нового дома
                        NowObj.StateViol = true;
                        //добавление больных
                        int freePeople = NowObj.CountPeople - NowObj.CountDeath - NowObj.CountViol;
                        if (freePeople >= 3)
                            NowObj.CountViol += 3;
                        else
                            NowObj.CountViol += freePeople;
                        //обнуле спика не зараженных домов
                        HousesToViol.Remove(NowObj);

                        Debug.Log($"В {DistricdStateObj1} заразился новый дом {NowObj}. CountViol = {NowObj.CountViol}");
                    }
                }
                else
                {
                    break;
                }
            }
        }
        CountViolHouses = 0;
        //очистка списка 
        HousesToViol.Clear();
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
            NowObj.ViolPeople();
            //Снимаем блок
            NowObj.DefendLockValanters();
            //показываем зараженные дома
            NowObj.IconViol();
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
            if (NowHouses.StateViol && !NowHouses.Lock ) //И ДОМ НЕ В КАРАНТИНЕ!!!
            {
                CountViolHouses += 1;
                //КОРОЧЕ ВОТ ТУТ ВОТ ОН СЧИТАЕТ, ЧТО ДОМ НЕ В КАРАНТИНЕ, ХОТЯ ДОЛЖЕН БЫТЬ
            }
            else if(!NowHouses.AllDead)
            {
                DeathStat.Money += 3;
                HousesToViol.Add(NowHouses);
            }
        }
        return CountViolHouses;
    }

    private void DistricdState()
    {
        int CountPeopleDistricd = 0;
        int CountDeathDistricd = 0;
        int CountViolDistricd = 0;
        for(int i=0; i < Houses.Length; i++)
        {
            CountPeopleDistricd += Houses[i].GetComponent<StateOBJ>().CountPeople;
            CountDeathDistricd += Houses[i].GetComponent<StateOBJ>().CountDeath;
            CountViolDistricd += Houses[i].GetComponent<StateOBJ>().CountViol;
        }
        //данные для района внутри
        DistricdStateObj1.CountPeople = CountPeopleDistricd;
        DistricdStateObj1.CountDeath = CountDeathDistricd;
        DistricdStateObj1.CountViol = CountViolDistricd;
        //данные для глобальной карты
        DistricdStateObj2.CountPeople = CountPeopleDistricd;
        DistricdStateObj2.CountDeath = CountDeathDistricd;
        DistricdStateObj2.CountViol = CountViolDistricd;
        DistricdStateObj2.IconViol();
        //расчет степени заражения
        float powerColor = (float)((CountViolDistricd + CountDeathDistricd) * 2)/(float)CountPeopleDistricd;
        //изменение альфы района на шлобальной карте
        DistricdStateObj2.ViolLine.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, powerColor);


    }
}

    
