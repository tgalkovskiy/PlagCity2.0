using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenderManager : MonoBehaviour
{
    public static SenderManager Instance;

    public MainScript MainScript;

    [SerializeField] private LetterSender Workers;
    [SerializeField] private LetterSender Imerator;

    [SerializeField] private GameObject VisitorPref;

    [SerializeField] private GameObject EndGamePanel;
    [SerializeField] private Text EndGameText;

    private bool OnDay1Done = false;
    private bool OnDay2Done = false;
    private bool OnDay3Done = false;
    private bool OnDay4Done = false;
    private bool OnDay5Done = false;
    private bool OnDay6Done = false;
    private bool OnDay7Done = false;
    private bool OnDay8Done = false;
    private bool OnDay9Done = false;
    private bool OnDay10Done = false;
    private bool OnDay11Done = false;
    private bool OnDay12Done = false;
    private bool OnDay13Done = false;
    private bool OnDay14Done = false;
    private bool OnWorkersRepDone = false;
    private bool OnMoneyLess150Done = false;
    private bool Event10Done = false;
    private bool Dialog9answer1Done = false;
    private bool Dialog10answer1Done = false;
    private bool Dialog10answer2Done = false;
    private bool Event25answer1Done = false;
    private bool Event25answer2Done = false;
    private bool IsFirstRiotDone = false;
    private bool IsUnburied5PeopleDone = false;
    private bool IsVacina95Done = false;
    private bool Event30Done = false;
    private bool Event33Done = false;
    private bool IsAllRiotDone = false;
    private bool Is50PercentDone = false;
    private bool Is60PercentDone = false;
    private bool Dialog7Answer1Done = false;
    private bool Dialog15Done = false;
    private bool Dialog2answer1done = false;

    [SerializeField] private bool[] LettersActivate;
    [SerializeField] private int[] LettersDuration;

    [SerializeField] private string[] LettersNames;
    [SerializeField] private string[] LettersTexts;
    [SerializeField] private string[] LettersAnswer1Texts;
    [SerializeField] private string[] LettersAnswer1BookTexts;
    [SerializeField] private string[] LettersAnswer2Texts;
    [SerializeField] private string[] LettersAnswer2BookTexts;
    [SerializeField] private string[] LettersIgnorTexts;

    [SerializeField] private string[] DialogsNames;
    [SerializeField] private string[] DialogsTexts;
    [SerializeField] private string[] DialogsAnswer1Texts;
    [SerializeField] private string[] DialogsAnswer1BookTexts;
    [SerializeField] private string[] DialogsAnswer2Texts;
    [SerializeField] private string[] DialogsAnswer2BookTexts;
    [SerializeField] private string[] DialogsAnswer3Texts;
    [SerializeField] private string[] DialogsAnswer3BookTexts;
    [SerializeField] private string[] DialogsIgnorTexts;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Debug.Log("AWAKE");
    }

    //Проверка условий на создание письма
    public void CheckConditions()
    {
        Letter tempLetter; //Переменная для временного хранения нового письма
        Visitor visitor;

        #region Rules
        //Для создания ивента с письмо необходимо:
        //  1. Создать поле bool, обозначающее сработал ли ивент
        //  2. Создать условие ивента 
        //  3. Создать письмо по шаблону:
        //      1. tempLetter = Отправитель письма(Workers, Imperator...).AddLetter(...);
        //      2. Передаваемые параметры в AddLetter:
        //              - bool Нужно ли показать письмо на старте дня
        //              - int Количество дней, через которое письмо будет считаться проигнорированным
        //              - string Имя отправителя
        //              - string основной текст письма
        //              - (Опционально) Два new Answer("Текст ответа"); - если вариантов ответы не предусмотренны, то не надо
        //      3. (Опционально) Добавить tempLetter.IgnorReactions = new AnswerReaction[] - реакции при игноре письма
        //                                                                          (игнор считается только по времени)
        //      4. (Опционально) Добавить tempLetter.Answer_1/2.Conditions = new AnswerCondition[] - условия, необходимые для выбора ответа
        //      5. (Опционально) Добавить tempLetter.Answer_1/2.Reactions = new AnwerReaction[] - реакции, при выборе данного варианта ответа
        //                                                через AnswerReaction реализованы реакции, связанные с цифрами(Деньги,полцмены и т.п.)
        //                                                Реакции, которые вызывают другие события, необходимо добавить через событие Answer.Chosen
        //                                                добавить тут метод и подписать его на это событие (я не придумал как сделать лучше)
        //                                                                  
        //
        #endregion

        if (MainData.Day == 1 && !OnDay1Done)
        {
            //событие 0
            tempLetter = Workers.AddLetter(LettersActivate[0], LettersDuration[0], LettersNames[0], LettersTexts[0]);
            //событие 43
            tempLetter = Workers.AddLetter(LettersActivate[43], LettersDuration[43], LettersNames[43], LettersTexts[43]);
            //событие 1
            tempLetter = Workers.AddLetter(LettersActivate[1], LettersDuration[1], LettersNames[1], LettersTexts[1], Resources.Load<Sprite>("Sprites/Tutorial_Search"));
           

            OnDay1Done = true;
        }

        if (MainData.Day == 2 && !OnDay2Done)
        {
            //диалог 4
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Policeman");
            visitor.Text = DialogsTexts[4];
            visitor.IgnorText = DialogsIgnorTexts[4];
            visitor.Name = DialogsNames[4];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[4]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -5), new AnswerReaction("ImperatorRep", 15) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[4];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[4]);
            visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -20) };
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[4];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[4]);
            visitor.Answer_3.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", 5) };
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[4];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 2
            tempLetter = Workers.AddLetter(LettersActivate[2], LettersDuration[2], LettersNames[2], LettersTexts[2],
                new Answer(LettersAnswer1Texts[2]),
                new Answer(LettersAnswer2Texts[2]), Resources.Load<Sprite>("Sprites/Tutorial_Letters"));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[2];
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 2) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[2];
            tempLetter.IgnorText = LettersIgnorTexts[2];
            //событие 3
            tempLetter = Workers.AddLetter(LettersActivate[3], LettersDuration[3], LettersNames[3], LettersTexts[3], Resources.Load<Sprite>("Sprites/Tutorial_LockHouse"));
            //событие 4
            tempLetter = Workers.AddLetter(LettersActivate[4], LettersDuration[4], LettersNames[4], LettersTexts[4],
                new Answer(LettersAnswer1Texts[4]),
                new Answer(LettersAnswer2Texts[4]));
            tempLetter.Answer_1.Chosen += InfectTwoHousesInSunland;
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[4];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[4];
            tempLetter.IgnorText = LettersIgnorTexts[4];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", 5) };
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -5) };
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -7) };

            //событие 44
            tempLetter = Workers.AddLetter(LettersActivate[44], LettersDuration[44], LettersNames[44], LettersTexts[44]);

            OnDay2Done = true;
        }

        if (MainData.Day == 3 && !OnDay3Done)
        {
            //диалог 0
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Sister");
            visitor.Text = DialogsTexts[0];
            visitor.IgnorText = DialogsIgnorTexts[0];
            visitor.Name = DialogsNames[0];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[0]);
            visitor.Answer_1.Chosen += ViolWestRiver;
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[0];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[0]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[0];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 5
            tempLetter = Workers.AddLetter(LettersActivate[5], LettersDuration[5], LettersNames[5], LettersTexts[5]);
            //событие 6
            tempLetter = Workers.AddLetter(LettersActivate[6], LettersDuration[6], LettersNames[6], LettersTexts[6], Resources.Load<Sprite>("Sprites/Tutorial_LockRoad"));
            //событие 7
            tempLetter = Workers.AddLetter(LettersActivate[7], LettersDuration[7], LettersNames[7], LettersTexts[7], Resources.Load<Sprite>("Sprites/Tutorial_Bread"));
            //событие 8
            tempLetter = Workers.AddLetter(LettersActivate[8], LettersDuration[8], LettersNames[8], LettersTexts[8],
                new Answer(LettersAnswer1Texts[8]),
                new Answer(LettersAnswer2Texts[8]));
            tempLetter.Answer_1.Chosen += GoToPir4Hours;
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[8];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[8];
            tempLetter.IgnorText = LettersIgnorTexts[8];

            //событие 45
            tempLetter = Workers.AddLetter(LettersActivate[45], LettersDuration[45], LettersNames[45], LettersTexts[45]);

            OnDay3Done = true;
        }

        if (MainData.Day == 4 && !OnDay4Done)
        {
            //диалог 8
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Drinker");
            visitor.Text = DialogsTexts[8];
            visitor.IgnorText = DialogsIgnorTexts[8];
            visitor.Name = DialogsNames[8];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[8]);
            visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 10) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[8];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[8]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[8];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 9
            tempLetter = Workers.AddLetter(LettersActivate[9], LettersDuration[9], LettersNames[9], LettersTexts[9],
                new Answer(LettersAnswer1Texts[9]),
                new Answer(LettersAnswer2Texts[9]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[9];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 2) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 200) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[9];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 4) };
            tempLetter.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 350) };
            tempLetter.IgnorText = LettersIgnorTexts[9];
            //событие 10
            tempLetter = Workers.AddLetter(LettersActivate[10], LettersDuration[10], LettersNames[10], LettersTexts[10],
                new Answer(LettersAnswer1Texts[10]),
                new Answer(LettersAnswer2Texts[10]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[10];
            tempLetter.Answer_1.Chosen += Event10;
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Money", 100) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[10];
            tempLetter.IgnorText = LettersIgnorTexts[10];
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -5) };

            //событие 46
            tempLetter = Workers.AddLetter(LettersActivate[46], LettersDuration[46], LettersNames[46], LettersTexts[46]);

            OnDay4Done = true;
        }

        if (MainData.Day == 5 && !OnDay5Done)
        {
            //диалог 5
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Policeman");
            visitor.Text = DialogsTexts[5];
            visitor.IgnorText = DialogsIgnorTexts[5];
            visitor.Name = DialogsNames[5];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[5]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", 10) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[5];
            visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", MainData.Money) };
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[5]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[5];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[5]);
            visitor.Answer_3.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", 30) };
            visitor.Answer_3.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 1000) };
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[5];
            VisitorsManager.Instance.AddVisitorToShow(visitor);


            //диалог 1
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Sister");
            visitor.Text = DialogsTexts[1];
            visitor.IgnorText = DialogsIgnorTexts[1];
            visitor.Name = DialogsNames[1];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[1]);
            visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", -1) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[1];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[1]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[1];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[1]);
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[1];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            if (Event10Done)
            {
                //событие 11
                tempLetter = Workers.AddLetter(LettersActivate[11], LettersDuration[11], LettersNames[11], LettersTexts[11],
                    new Answer(LettersAnswer1Texts[11]),
                    new Answer(LettersAnswer2Texts[11]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[11];
                tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -3) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[11];
                tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                tempLetter.IgnorText = LettersIgnorTexts[11];
                tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };

                //событие 12
                tempLetter = Workers.AddLetter(LettersActivate[12], LettersDuration[12], LettersNames[12], LettersTexts[12],
                    new Answer(LettersAnswer1Texts[12]),
                    new Answer(LettersAnswer2Texts[12]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[12];
                tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -3) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[12];
                tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                tempLetter.IgnorText = LettersIgnorTexts[12];
                tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };

                //событие 13
                tempLetter = Workers.AddLetter(LettersActivate[13], LettersDuration[13], LettersNames[13], LettersTexts[13],
                    new Answer(LettersAnswer1Texts[13]),
                    new Answer(LettersAnswer2Texts[13]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[13];
                tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -3) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[13];
                tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                tempLetter.IgnorText = LettersIgnorTexts[13];
                tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                //событие 14
                tempLetter = Workers.AddLetter(LettersActivate[14], LettersDuration[14], LettersNames[14], LettersTexts[14],
                    new Answer(LettersAnswer1Texts[14]),
                    new Answer(LettersAnswer2Texts[14]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[14];
                tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -3) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[14];
                tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                tempLetter.IgnorText = LettersIgnorTexts[14];
                tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                //событие 15
                tempLetter = Workers.AddLetter(LettersActivate[15], LettersDuration[15], LettersNames[15], LettersTexts[15],
                    new Answer(LettersAnswer1Texts[15]),
                    new Answer(LettersAnswer2Texts[15]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[15];
                tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -3) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[15];
                tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };
                tempLetter.IgnorText = LettersIgnorTexts[15];
                tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -3) };

            }

            //событие 16
            tempLetter = Workers.AddLetter(LettersActivate[16], LettersDuration[16], LettersNames[16], LettersTexts[16],
                new Answer(LettersAnswer1Texts[16]),
                new Answer(LettersAnswer2Texts[16]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[16];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Vacina", 3), new AnswerReaction("RichRep", -5) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[16];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", 5) };
            tempLetter.IgnorText = LettersIgnorTexts[16];
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", 5) };

            //событие 47
            tempLetter = Workers.AddLetter(LettersActivate[47], LettersDuration[47], LettersNames[47], LettersTexts[47]);

            OnDay5Done = true;
        }


        if (MainData.Day == 6 && !OnDay6Done)
        {
            //диалог 12
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Worker");
            visitor.Text = DialogsTexts[12];
            visitor.IgnorText = DialogsIgnorTexts[12];
            visitor.Name = DialogsNames[12];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[12]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("WorkersRep", -10) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[12];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[12]);
            visitor.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 200) };
            visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("WorkersRep", 25) };
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[12];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 17
            tempLetter = Workers.AddLetter(LettersActivate[17], LettersDuration[17], LettersNames[17], LettersTexts[17],
                new Answer(LettersAnswer1Texts[17]),
                new Answer(LettersAnswer2Texts[17]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[17];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", 10) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[17];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -10) };
            tempLetter.IgnorText = LettersIgnorTexts[17];
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RichRep", -10) };

            //событие 18
            tempLetter = Workers.AddLetter(LettersActivate[18], LettersDuration[18], LettersNames[18], LettersTexts[18],
                new Answer(LettersAnswer1Texts[18]),
                new Answer(LettersAnswer2Texts[18]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[18];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[18];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Money", 600) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            tempLetter.IgnorText = LettersIgnorTexts[18];

            //событие 21
            tempLetter = Workers.AddLetter(LettersActivate[21], LettersDuration[21], LettersNames[21], LettersTexts[21],
                new Answer(LettersAnswer1Texts[21]),
                new Answer(LettersAnswer2Texts[21]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[21];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[21];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("RichRep", -5) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            tempLetter.IgnorText = LettersIgnorTexts[21];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("InfectIn", 7),
                                                                   new AnswerReaction("InfectOut", 5) };
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("InfectIn", 7),
                                                                   new AnswerReaction("InfectOut", 5) };
            //событие 48
            tempLetter = Workers.AddLetter(LettersActivate[48], LettersDuration[48], LettersNames[48], LettersTexts[48]);

            OnDay6Done = true;
        }

        if (MainData.Day == 7 && !OnDay7Done)
        {
            //диалог 6
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Policeman");
            visitor.Text = DialogsTexts[6];
            visitor.IgnorText = DialogsIgnorTexts[6];
            visitor.Name = DialogsNames[6];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[6]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", 20) };
            visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 1000) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[6];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[6]);
            visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -30) };
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[6];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[6]);
            visitor.Answer_3.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -10) };
            visitor.Answer_3.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 2) };
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[6];
            VisitorsManager.Instance.AddVisitorToShow(visitor);


            //событие 19
            tempLetter = Workers.AddLetter(LettersActivate[19], LettersDuration[19], LettersNames[19], LettersTexts[19],
                new Answer(LettersAnswer1Texts[19]),
                new Answer(LettersAnswer2Texts[19]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[19];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 2) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 250) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[19];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 4) };
            tempLetter.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 400) };
            tempLetter.IgnorText = LettersIgnorTexts[19];

            //событие 49
            tempLetter = Workers.AddLetter(LettersActivate[49], LettersDuration[49], LettersNames[49], LettersTexts[49]);

            OnDay7Done = true;
        }

        if(MainData.Day == 8 && !OnDay8Done)
        {
            //диалог 2
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Sister");
            visitor.Text = DialogsTexts[2];
            visitor.IgnorText = DialogsIgnorTexts[2];
            visitor.Name = DialogsNames[2];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[2]);
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[2];
            visitor.Answer_1.Chosen += Recovery5Houses;
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[2]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[2];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[2]);
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[2];
            VisitorsManager.Instance.AddVisitorToShow(visitor);


            //диалог 9
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Drinker");
            visitor.Text = DialogsTexts[9];
            visitor.IgnorText = DialogsIgnorTexts[9];
            visitor.Name = DialogsNames[9];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[9]);
            visitor.Answer_1.Chosen += Dialog9answer1;
            visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 60) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[9];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[9]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[9];
            visitor.Answer_3 = new Answer(DialogsAnswer3Texts[9]);
            visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[9];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 50
            tempLetter = Workers.AddLetter(LettersActivate[50], LettersDuration[50], LettersNames[50], LettersTexts[50]);

            OnDay8Done = true;
        }

        if(MainData.Day == 9 && !OnDay9Done)
        {
            //диалог 7
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Policeman");
            visitor.Text = DialogsTexts[7];
            visitor.IgnorText = DialogsIgnorTexts[7];
            visitor.Name = DialogsNames[7];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[7]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", 15), new AnswerReaction("WorkersRep", -20) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[7];
            visitor.Answer_1.Chosen += Dialog7Answer1;
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[7]);
            visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -15), new AnswerReaction("RichRep", -20), new AnswerReaction("Volunteer", -1)};
            visitor.Answer_2.Chosen += InfectRandomHouse;
            visitor.Answer_2.Chosen += InfectRandomHouse;
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[7];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            //событие 51
            tempLetter = Workers.AddLetter(LettersActivate[51], LettersDuration[51], LettersNames[51], LettersTexts[51]);

            OnDay9Done = true;
        }
    
        if (MainData.Day == 10 && !OnDay10Done)
        {
            if(Dialog7Answer1Done == true)
            {
                //диалог 13
                visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
                visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Worker");
                visitor.Text = DialogsTexts[13];
                visitor.IgnorText = DialogsIgnorTexts[13];
                visitor.Name = DialogsNames[13];
                visitor.IgnorReactions = null;
                visitor.Answer_1 = new Answer(DialogsAnswer1Texts[13]);
                visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("WorkersRep", -20) };
                visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[13];
                visitor.Answer_2 = new Answer(DialogsAnswer2Texts[13]);
                visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[13];
                VisitorsManager.Instance.AddVisitorToShow(visitor);
            }

            if(Dialog2answer1done == true)
            {
                //диалог 3
                visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
                visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Sister");
                visitor.Text = DialogsTexts[3];
                visitor.IgnorText = DialogsIgnorTexts[3];
                visitor.Name = DialogsNames[3];
                visitor.IgnorReactions = null;
                visitor.Answer_1 = new Answer(DialogsAnswer1Texts[3]);
                visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[3];
                visitor.Answer_2 = new Answer(DialogsAnswer2Texts[3]);
                visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[3];
                visitor.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 2) };
                visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", -2) };
                VisitorsManager.Instance.AddVisitorToShow(visitor);
            }

            //событие 22
            tempLetter = Workers.AddLetter(LettersActivate[22], LettersDuration[22], LettersNames[22], LettersTexts[22],
                new Answer(LettersAnswer1Texts[22]),
                new Answer(LettersAnswer2Texts[22]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[22];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[22];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", -2) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 150) };
            tempLetter.IgnorText = LettersIgnorTexts[22];
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("Volunteer", -2) };

            //событие 23
            tempLetter = Workers.AddLetter(LettersActivate[23], LettersDuration[23], LettersNames[23], LettersTexts[23],
                new Answer(LettersAnswer1Texts[23]),
                new Answer(LettersAnswer2Texts[23]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[23];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[23];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("InfectIn", -7),
                                                                   new AnswerReaction("InfectOut", -5) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 2) };
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("InfectIn", 5),
                                                                   new AnswerReaction("InfectOut", 3) };
            tempLetter.IgnorText = LettersIgnorTexts[23];
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("InfectIn", 5),
                                                                   new AnswerReaction("InfectOut", 3) };

            //событие 52
            tempLetter = Workers.AddLetter(LettersActivate[52], LettersDuration[52], LettersNames[52], LettersTexts[52]);

            OnDay10Done = true;
        }

        if (MainData.Day == 11 && !OnDay11Done)
        {
            if (Dialog9answer1Done == true)
            {
                //диалог 10
                visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
                visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Drinker");
                visitor.Text = DialogsTexts[10];
                visitor.IgnorText = DialogsIgnorTexts[10];
                visitor.Name = DialogsNames[10];
                visitor.IgnorReactions = null;
                visitor.Answer_1 = new Answer(DialogsAnswer1Texts[10]);
                visitor.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
                visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[10];
                visitor.Answer_1.Chosen += Dialog10answer1;
                visitor.Answer_2 = new Answer(DialogsAnswer2Texts[10]);
                visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[10];
                visitor.Answer_2.Chosen += Dialog10answer2;
                visitor.Answer_3 = new Answer(DialogsAnswer3Texts[10]);
                visitor.Answer_3.ReactionText = DialogsAnswer3BookTexts[10];
                VisitorsManager.Instance.AddVisitorToShow(visitor);
            }

            //событие 20
            tempLetter = Workers.AddLetter(LettersActivate[20], LettersDuration[20], LettersNames[20], LettersTexts[20],
                new Answer(LettersAnswer1Texts[20]),
                new Answer(LettersAnswer2Texts[20]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[20];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[20];
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volunteer", 1) };
            tempLetter.IgnorText = LettersIgnorTexts[20];
            tempLetter.Ignored += InfectRandomHouse;
            tempLetter.Answer_2.Chosen += InfectRandomHouse;


            //событие 24
            tempLetter = Workers.AddLetter(LettersActivate[24], LettersDuration[24], LettersNames[24], LettersTexts[24],
                new Answer(LettersAnswer1Texts[24]),
                new Answer(LettersAnswer2Texts[24]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[24];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 2) };
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[24];
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 4) };
            tempLetter.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 500) };
            tempLetter.IgnorText = LettersIgnorTexts[24];

            if (Dialog10answer2Done == true)
            {
                //событие 25
                tempLetter = Workers.AddLetter(LettersActivate[25], LettersDuration[25], LettersNames[25], LettersTexts[25],
                    new Answer(LettersAnswer1Texts[25]),
                    new Answer(LettersAnswer2Texts[25]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[25];
                tempLetter.Answer_1.Chosen += Event25answer1;
                tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[25];
                tempLetter.Answer_1.Chosen += Event25answer2;
                tempLetter.IgnorText = LettersIgnorTexts[25];
            }

            //событие 53
            tempLetter = Workers.AddLetter(LettersActivate[53], LettersDuration[53], LettersNames[53], LettersTexts[53]);

            OnDay11Done = true;
        }

        if (MainData.Day == 12 && !OnDay12Done)
        {
            //диалог 14
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Worker");
            visitor.Text = DialogsTexts[14];
            visitor.IgnorText = DialogsIgnorTexts[14];
            visitor.Name = DialogsNames[14];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[14]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", -2) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[14];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[14]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[14];
            visitor.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("WorkersRep", -5) };
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            if (Dialog10answer1Done == true)
            {
                //событие 26
                tempLetter = Workers.AddLetter(LettersActivate[26], LettersDuration[26], LettersNames[26], LettersTexts[26],
                    new Answer(LettersAnswer1Texts[26]),
                    new Answer(LettersAnswer2Texts[26]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[26];
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[26];
                tempLetter.IgnorText = LettersIgnorTexts[26];
            }
            //событие 54
            tempLetter = Workers.AddLetter(LettersActivate[54], LettersDuration[54], LettersNames[54], LettersTexts[54]);

            OnDay12Done = true;
        }

        if (Event25answer1Done == true || Event25answer2Done == true)
        {
            //событие 26
            tempLetter = Workers.AddLetter(LettersActivate[26], LettersDuration[26], LettersNames[26], LettersTexts[26],
                new Answer(LettersAnswer1Texts[26]),
                new Answer(LettersAnswer2Texts[26]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[26];
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[26];
            tempLetter.IgnorText = LettersIgnorTexts[26];
        }

        if (MainData.Day == 13 && !OnDay13Done)
        {
            //событие 27
            tempLetter = Workers.AddLetter(LettersActivate[27], LettersDuration[27], LettersNames[27], LettersTexts[27]);
            tempLetter.IgnorText = LettersIgnorTexts[27];

            OnDay13Done = true;
        }

        if( MainData.Day == 14 && !OnDay14Done)
        {
            //диалог 11
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/Policeman");
            visitor.Text = DialogsTexts[11];
            visitor.IgnorText = DialogsIgnorTexts[11];
            visitor.Name = DialogsNames[11];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[11]);
            visitor.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -15) };
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[11];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[11]);
            visitor.Answer_2.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 100) };
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[11];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            OnDay14Done = true;
        }

        if (MainData.IsFirstRiot && !IsFirstRiotDone)
        {
            IsFirstRiotDone = true;
            //Событие 28
            tempLetter = Workers.AddLetter(LettersActivate[28], LettersDuration[28], LettersNames[28], LettersTexts[28], Resources.Load<Sprite>("Sprites/Tutorial_Riot"));
            tempLetter.IgnorText = LettersIgnorTexts[28];
        }

        if(MainData.UnburiedPeople > 5 && !IsUnburied5PeopleDone)
        {
            IsUnburied5PeopleDone = true;

            //Событие 29
            tempLetter = Workers.AddLetter(LettersActivate[29], LettersDuration[29], LettersNames[29], LettersTexts[29], Resources.Load<Sprite>("Sprites/Tutorial_Graveyard"));
            tempLetter.IgnorText = LettersIgnorTexts[29];
        }
        
        if(MainData.Money < 30 && MainData.Day > 7 && !Event30Done)
        {
            Event30Done = true;


            //событие 30
            tempLetter = Workers.AddLetter(LettersActivate[30], LettersDuration[30], LettersNames[30], LettersTexts[30],
                new Answer(LettersAnswer1Texts[30]),
                new Answer(LettersAnswer2Texts[30]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[30];
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Money", 300) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[30];
            tempLetter.IgnorText = LettersIgnorTexts[30];
        }
        
        if(MainData.ImperatorReputation < 20 && !Event33Done)
        {
            Event33Done = true;

            //Событие 33
            tempLetter = Workers.AddLetter(LettersActivate[33], LettersDuration[33], LettersNames[33], LettersTexts[33]);
            tempLetter.IgnorText = LettersIgnorTexts[33];
        }

        if (!IsAllRiotDone)
        {
            if (MainScript.CheckAllRichRiot())
            {
                IsAllRiotDone = true;


                //Событие 34
                tempLetter = Workers.AddLetter(LettersActivate[34], LettersDuration[34], LettersNames[34], LettersTexts[34]);
                tempLetter.IgnorText = LettersIgnorTexts[34];
            }
        }

        if (!Is50PercentDone)
        {
            if (MainScript.Check50percentInfectedHouses())
            {
                Is50PercentDone = true;

                //событие 39
                tempLetter = Workers.AddLetter(LettersActivate[39], LettersDuration[39], LettersNames[39], LettersTexts[39],
                    new Answer(LettersAnswer1Texts[39]),
                    new Answer(LettersAnswer2Texts[39]));
                tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[39];
                tempLetter.Answer_1.Chosen += GoWithWife;
                tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[39];
                tempLetter.IgnorText = LettersIgnorTexts[39];
            }
        }

        if (MainData.MaxVolunteers < 3 && MainData.Day > 5)
        {
            //событие 41
            tempLetter = Workers.AddLetter(LettersActivate[41], LettersDuration[41], LettersNames[41], LettersTexts[41],
                new Answer(LettersAnswer1Texts[41]),
                new Answer(LettersAnswer2Texts[41]));
            tempLetter.Answer_1.ReactionText = LettersAnswer1BookTexts[41];
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 250) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volunteer", 1) };
            tempLetter.Answer_2.ReactionText = LettersAnswer2BookTexts[41];
            tempLetter.IgnorText = LettersIgnorTexts[41];
        }

        if(MainData.WorkersReputation < MainData.MinWorkersReputation && !Dialog15Done)
        {

            //диалог 15
            visitor = Instantiate(VisitorPref, VisitorsManager.Instance.transform).GetComponent<Visitor>();
            visitor.VisitorSprite = Resources.Load<Sprite>("Sprites/WorkerAngry");
            visitor.Text = DialogsTexts[15];
            visitor.IgnorText = DialogsIgnorTexts[15];
            visitor.Name = DialogsNames[15];
            visitor.IgnorReactions = null;
            visitor.Answer_1 = new Answer(DialogsAnswer1Texts[15]);
            visitor.Answer_1.ReactionText = DialogsAnswer1BookTexts[15];
            visitor.Answer_2 = new Answer(DialogsAnswer2Texts[15]);
            visitor.Answer_2.ReactionText = DialogsAnswer2BookTexts[15];
            VisitorsManager.Instance.AddVisitorToShow(visitor);

            Dialog15Done = true;
        }
    }

    private void GoWithWife()
    {
        goAwayWithWife = true;
    }

    private bool goAwayWithWife = false;

    public void CheckEndsOfTheGame()
    {
        bool EndGame = false; 
        
        //событие 40
        if (goAwayWithWife)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = LettersTexts[40];

            EndGame = true;
        }

        //событие 34
        if (MainData.WorkersReputation < MainData.MinWorkersReputation && Dialog15Done)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = LettersTexts[34];
            EndGame = true;
        }

        //событие 31
        if (MainData.Vacina > 95)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = LettersTexts[31];
            EndGame = true;
        }

        //if(Statistics.AllInfected >= 300)
        //{
        //    EndGamePanel.SetActive(true);
        //    EndGameText.text = "Заражённых стало слишком много. Мэр тоже заболел. Температура не спадает, лихорадка продолжается. Здесь мы бессильны. Наш Мэр был хорошим человеком и боролся до конца. Помолимся же за его душу.";

        //    EndGame = true;
        //}

        //событие 35
        if (MainData.ImperatorReputation <= 0 && Event33Done)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = LettersTexts[35];

            EndGame = true;
        }

        //событие 42
        if (!Is60PercentDone)
        {
            if (MainScript.Check70percentInfectedHouses())
            {
                Is60PercentDone = true;

                EndGamePanel.SetActive(true);
                EndGameText.text = LettersTexts[42];

                EndGame = true;
            }
        }

        if (!EndGame)
            VisitorsManager.Instance.CheckVisitorToShow();
    }

    private void ViolWestRiver()
    {
        MainScript.ViolWestRiver = true;
    }

    private void InfectTwoHousesInSunland()
    {
        MainScript.InfectTwoHousesInSunland = true;
    }

    private void GoToPir4Hours()
    {
        MainScript.TimeStartDayPlus4 = true;
    }

    private void Event10()
    {
        Event10Done = true;
    }

    private void InfectRandomHouse()
    {
        MainScript.InfectRandomHouse = true;
    }

    private void Event25answer1()
    {
        Event25answer1Done = true;
    }
    private void Event25answer2()
    {
        Event25answer2Done = true;
        MainData.Money = 0;
    }

    private void Dialog7Answer1()
    {
        Dialog7Answer1Done = true;
    }

    private void Dialog15()
    {
        Dialog15Done = true;
    }

    private void Recovery5Houses()
    {
        Dialog2answer1done = true;
        MainScript.Recovery5Houses = true;
    }
    
    private void Dialog10answer1()
    {
        Dialog10answer1Done = true;
    }
    private void Dialog10answer2()
    {
        Dialog10answer2Done = true;
    }

    private void Dialog9answer1()
    {
        Dialog9answer1Done = true;
    }
}
