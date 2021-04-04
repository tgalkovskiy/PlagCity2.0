using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderManager : MonoBehaviour
{
    public static SenderManager Instance;

    [SerializeField] private LetterSender Workers;
    [SerializeField] private LetterSender Imerator;

    private bool OnStartDone = false;
    private bool OnDay3Done = false;
    private bool OnMoneyLess150Done = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckConditions()
    {
        Letter tempLetter;

        if (DeathStat.Day == 1 && !OnStartDone)
        {
            Workers.AddLetter(true, "Начало игры");

            Workers.AddLetter(true, "Событие произошло 1",
                new Answer("Ответ 1"), new Answer("Ответ 2"));

            tempLetter = Workers.AddLetter(true, "Событие произошло 2",
                new Answer("Получить 100 голды"), new Answer("Получить 200 голды"));
            tempLetter.Answer_1.Chosen += AddMoney100;
            tempLetter.Answer_2.Chosen += AddMoney200;

            Workers.AddLetter(false, "Не показывается сразу, без вариантов ответа");

            Workers.AddLetter(false, "Не показывается сразу, с вариантами ответа",
                new Answer("Работает"), new Answer("Кайф"));

            OnStartDone = true;
        }

        if (DeathStat.Day == 3 && !OnDay3Done)
        {
            tempLetter = Imerator.AddLetter(true, "Наступил день 3, пора платить дань императору",
                new AnswerPrice("Потратить 5500 голды", 5500), new Answer("Ничего не делать"));
            tempLetter.Answer_1.Chosen += SpendMoney5500;

            OnStartDone = true;
        }

        if(DeathStat.Money <= 150 && !OnMoneyLess150Done)
        {
            Workers.AddLetter(true, "Денег нет :(");

            OnMoneyLess150Done = true;
        }
    }

    private void AddMoney100()
    {
        DeathStat.Money += 100;
    }

    private void AddMoney200()
    {
        DeathStat.Money += 200;
    }

    private void SpendMoney5500()
    {
        DeathStat.Money -= 5500;
    }
}
