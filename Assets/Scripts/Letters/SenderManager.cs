using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderManager : MonoBehaviour
{
    public static SenderManager Instance;

    [SerializeField] private LetterSender Workers;
    [SerializeField] private LetterSender Imerator;

    [SerializeField] private GameObject EndGamePanel;

    private bool OnDay1Done = false;
    private bool OnDay2Done = false;
    private bool OnDay3Done = false;
    private bool OnDay6Done = false;
    private bool OnRegRepDone = false;
    private bool OnMoneyLess150Done = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //Проверка условий на создание письма
    public void CheckConditions()
    {
        Letter tempLetter; //Переменная для временного хранения нового письма

        //
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


        if (DeathStat.Day == 1 && !OnDay1Done)
        {
            tempLetter = Workers.AddLetter(true, 1, "", "Здравствуйте, мэр! Ваш город переживает не лучшие времена. Его постигла страшная кара - неведомая болезнь. Горожане за стеной, можно считать, уже обречены. Но людей внутри стен еще можно спасти. Хорошая новость: вакцина почти готова. Вам нужно только продержаться. Распределяйте свои ресурсы грамотно. Читайте обращения от граждан. Они действительны в течение дня. Но не торопитесь отвечать сразу! Сделанный выбор изменить нельзя. Ищите лучшие варианты.");

            //tempLetter = Workers.AddLetter(true, 1, "Врач", "Хотя мы и обезопасили этот район стеной, я рекомендую вам принять профилактические меры и отправить врачей и волонтеров обследовать жителей. Так мы сможем диагностировать болезнь на ранних этапах.");
            tempLetter = Workers.AddLetter(true, 1, "Полковник", "Сэр, по приказу императора перекрываем все въезды и выезды из города. Задача оградить наименее пострадавшую часть города от заразы. Наши медики говорят, что вакцина практически готова. От их успеха зависит наше выживание. Надеемся на ваше понимание и содействие.",
                new Answer("Хорошо, так и сделаем"),
                new Answer("Я против этого, но видимо мое мнение тут особого значения не имеет. Я донесу об этом до наших граждан"));
            tempLetter.Answer_1.ReactionText = "Император доволен вашим решением, а вот горожане не очень. (+10 репутации у Императора, -5 репутации у народа)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("ImperatorRep", 10),
                                                                  new AnswerReaction("RegionRep", -5)};
            tempLetter.Answer_2.ReactionText = "Император возмущен вашей дерзостью (-5 репутации у Императора)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -5) };
            tempLetter.IgnorText = "Полковник - это важное лицо. Он представляет самого Императора. Таких людей нельзя игнорировать. Император очень не доволен. (-15 репутации у Императора)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -15) };

            OnDay1Done = true;
        }

        if (DeathStat.Day == 2 && !OnDay2Done)
        {
            tempLetter = Workers.AddLetter(true, 1, "Врач", "Господин мэр, болезнь пробралась в периметр! А у нас заканчиваются медикаменты и элементарные средства защиты (тканевые повязки, марля). Мы знаем, что у графини Лоусенс есть запасы ткани на складе в ее поместье, но она отказывается их отдавать. Может пообщаетесь с ней?",
              new Answer("Городу нужнее, чем графине. Я отправлю полицейского к ней для конфискации. (1 полицейский)"),
              new Answer("Я вас прекрасно понимаю, но к сожалению я не могу отнять у графини ее имущество. Попробуйте справится своими силами."));
            tempLetter.Answer_1.ReactionText = "Насильственные меры понизили вашу репутацию, зато вы смогли ускорить работу над лекарством. (+5 к готовности вакцины, -5 репутации у народа)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("RegionRep", -5),
                                                                  new AnswerReaction("Vacina", 5)};
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Policeman", 1) };

            tempLetter.Answer_2.ReactionText = "Насилие - это не выход. Люди стали больше уважать вас. А болезнь подкралась ближе... (+1 зараженный дом)";
            tempLetter.IgnorText = "Вы проигнорировали просьбу врача. Болезнь стала на шаг ближе... (+1 зараженный дом)";

            OnDay2Done = true;
        }

        if (DeathStat.Day == 3 && !OnDay3Done)
        {
            tempLetter = Workers.AddLetter(true, 1, "Полковник", "Сэр, осмелюсь рекомендовать вам созвать побольше добровольцев, риск распространения болезней высок. Распорядитесь выделить на это средства и распространить объявления по району. Может кто из жителей решится присоединиться.",
              new Answer("Конечно, спасибо за совет. Так и сделаем. (500 монет за найм двоих добровольцев)"),
              new Answer("Ваше мнение важно для меня, но в данный момент мы справляемся."));
            tempLetter.Answer_1.ReactionText = "Вы получили дополнительных добровольцев и расположение Императора (+2 добровольца, +5 репутации у Императора)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("ImperatorRep", +5)/*,*/
                                                                  /*new AnswerReaction("Vacina", 5)*/};
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 500) };

            tempLetter.Answer_2.ReactionText = "Император не доволен, что вы пренебрегаете его советами (-10 репутации у Императора)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -10) };
            tempLetter.IgnorText = "Император недоволен, что вы игнорируете его письма (-15 репутации у Императора)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -15) };

            OnDay3Done = true;
        }

        if(DeathStat.Day == 6 && !OnDay6Done)
        {
            tempLetter = Workers.AddLetter(true, 1, "Рабочие", "Уважаемый мэр… хотя нет, о чем это я? Совсем не уважаемый! Ты бросил нас на произвол судьбы, а сам спрятался за высокими стенами. Нам конец и все наши смерти останутся на твоих руках. Однако мы просим о последней услуге и, возможно, хотя бы этим ты сможешь искупить свой грех. Нам нужна еда. У вас есть запасы на складах, спустите хотя бы на веревках часть еды. Возможно это позволит кому-то из нас прожить еще лишних несколько дней. В благодарность мы готовы предоставить несколько трупов зараженных с особо яркими проявлениями болезни для ваших богомерзких вскрытий, да простят нас за это высшие силы.",
              new Answer("Хорошо, мы предоставим вам пайки. (250 монет)"),
              new Answer("Нет, вы ничего не получите"));
            tempLetter.Answer_1.ReactionText = "Никто не ожидал, но рабочие сдержали свое слово. Вы потеряли немного денег и доверия граждан, но приблизились к победе над болезнью";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("ImperatorRep", -5)/*,*/
                                                                  /*new AnswerReaction("Vacina", 5)*/};
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 250) };

            tempLetter.Answer_2.ReactionText = "Вы проигнорировали просьбу рабочих, люди очень недовольны. (-15 репутации у народа)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -15) };
            tempLetter.IgnorText = "Вы проигнорировали просьбу рабочих, люди очень недовольны. (-15 репутации у народа)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -15) };

            OnDay6Done = true;
        }
        

        //    tempLetter = Workers.AddLetter(true, 1, "Событие произошло 2",
        //        new Answer("Получить 100  монеток"), new Answer("Получить 200 монеток"));
        //    tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Вы выбрали получить 100 монеток", "Money", 100) };
        //    tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Вы выбрали получить 200 монеток", "Money", 200) };
        //    tempLetter.IgnorReactions = new AnswerReaction[] { 
        //        new AnswerReaction("Вы проигнорировали выбор монеток, тогда мы заберем их у вас (-200)", "Money", -200) };

        //    tempLetter = Workers.AddLetter(false, 1, "Не показывается сразу, без вариантов ответа");

        //    tempLetter = Workers.AddLetter(false, 1, "Не показывается сразу, с вариантами ответа",
        //        new Answer("Работает"), new Answer("Кайф"));
        //    tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Вы выбрали Работает") };
        //    tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Вы выбрали Кайф") };

        //    OnStartDone = true;
        //}

        //if (DeathStat.Day == 3 && !OnDay3Done)
        //{
        //    tempLetter = Imerator.AddLetter(true, 1, "Наступил день 3, пора платить дань императору",
        //        new Answer("Потратить 5000 монеток"), new Answer("Ничего не делать"));
        //    tempLetter.Answer_1.Conditions = new AnswerCondition[] {new AnswerCondition("Money", 5000)};
        //    tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Вы выбрали потратить 5000 монеток", "Money", 200) };

        //    OnStartDone = true;
        //}

        //if(DeathStat.Money <= 150 && !OnMoneyLess150Done)
        //{
        //    tempLetter = Workers.AddLetter(true, 1, "Денег нет :(");

        //    OnMoneyLess150Done = true;
        //}
    }

    public void CheckEndsOfTheGame()
    {
        bool EndGame = false;
        //if (DeathStat.RegionReputation < 30 && !OnRegRepDone)
        //{
        //    EndGamePanel.SetActive(true);

        //    OnRegRepDone = true;
        //    EndGame = true;
        //}

        if (!EndGame)
            LettersManager.Instance.CheckLettersToShow();
    }
}
