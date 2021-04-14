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

    [SerializeField] private GameObject EndGamePanel;
    [SerializeField] private Text EndGameText;

    private bool OnDay1Done = false;
    private bool OnDay2Done = false;
    private bool OnDay3Done = false;
    private bool OnDay4Done = false;
    private bool OnDay5Done = false;
    private bool OnDay6Done = false;
    private bool OnDay7Done = false;
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
            tempLetter = Workers.AddLetter(true, 1, "представитель Императора\nсэр Лонгерпот", "Здравствуйте, Мэр! Император недоволен тем, как Вы управляете городом. Почти все районы заражены. Эти люди уже безнадежны! Оставим их на волю Господа. Безопасными осталось только три района. Здесь живут самые богатые и влиятельные люди, друзья Императора. Вы должны сохранить их во что бы то ни стало. Император решил помочь Вам и велел установить заграждение по периметру. Дальше - дело за Вами. Не подведите.",
                new Answer("Это разумно, поддерживаю"),
                new Answer("Я против этого, но видимо мое мнение тут особого значения не имеет."));
            tempLetter.Answer_1.ReactionText = "Император доволен вашим решением, а вот горожане не очень (+10 репутации у Императора, -5 репутации у народа)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("ImperatorRep", 10),
                                                                  new AnswerReaction("RegionRep", -5)};
            tempLetter.Answer_2.ReactionText = "Император возмущен вашей дерзостью (-5 репутации у Императора)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -5) };
            tempLetter.IgnorText = "Император очень не доволен, что вы проигнорировали его представителя (-15 репутации у Императора)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("ImperatorRep", -15) };
            //
            tempLetter = Workers.AddLetter(true, 1, "Начальник полиции\nДжеймс Хаммерхед", "В районе Sunland замечен подозрительный дом с задернутыми шторами. Советую Вам отправить доборовольца на обыск. Мы должны убедиться, что болезнь не пробралась с периметр.", Resources.Load<Sprite>("Sprites/Sprite_1"));
            //
            tempLetter = Workers.AddLetter(true, 1, "Секретарь\nЭшли", "Каждый день Вам приходят новые письма, они хранятся в ящике. Не забывайте его проверять! Там же вы сможете найти инструкции по управлению городом.", Resources.Load<Sprite>("Sprites/Sprite_2"));

            OnDay1Done = true;
        }

        if (DeathStat.Day == 2 && !OnDay2Done)
        {

            tempLetter = Workers.AddLetter(true, 1, "Секретарь\nЭшли", "День закончится автоматически, когда придет время. Если вы справитесь со своими делами раньше, можете закончить его самостоятельно, нажав кнопку \"Новый день\" или клавишу \"Пробел\".", Resources.Load<Sprite>("Sprites/Sprite_4"));
            //
            tempLetter = Workers.AddLetter(true, 1, "Секретарь\nЭшли", "Вы можете отложить письмо и вернуться к нему позже, если Вам не хватает ресурсов. Некоторые из писем показываются сразу, остальные хранятся в вашем ящике. Но они не будут ждать бесконечно. Ненужные письма лучше выбрасывать, чтобы не хратить лишний хлам в своем ящике.", Resources.Load<Sprite>("Sprites/Sprite_2"));
            //
            tempLetter = Workers.AddLetter(true, 1, "Начальник полиции\nДжеймс Хаммерхед", "Мы обнаружили умерших в районе Sunland. Дома, где они проживали лучше взять в карантин, чтобы обезопасить остальных жителей. Отправьте добровольцев на это задание.", Resources.Load<Sprite>("Sprites/Sprite_3"));
            //
            tempLetter = Workers.AddLetter(false, 3, "Начальник полиции\nДжеймс Хаммерхед", "Эпидемия длится уже несколько месяцев. С этих пор закрыты заводы и фабрики. Многие лишились рабочих мест и умирают буквально умирают от голода. Они готовы выполнять любую грязную работу за еду. Сегодня к нам пришли еще два \"добровольца\". Желаете нанять их?",
                new Answer("Конечно! Лишние руки нам не помешают (300 денег)"),
                new Answer("Нет."));
            tempLetter.Answer_1.ReactionText = "Вчера Вы наняли на службу двоих человек (+2 добровольца)";
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", 2) };
            tempLetter.IgnorText = "Предложение Джеймса Хаммерхеда больше не действительно";


            OnDay2Done = true;
        }

        if (DeathStat.Day == 3 && !OnDay3Done)
        {
            tempLetter = Workers.AddLetter(true, 1, "Cестра милосердия\nАнна", "Сегодня я видела маленькую девочку из бедных на границе Sunland. Она собирала цветы. Я поговорила с ней. Оказалось, она осталась сироткой после смерти семьи. Можем ли мы что-то для нее сделать?",
              new Answer("Я думаю да. В доме графини Лоусенс в West River есть несколько свободных комнат. Поселим девочку у нее"),
              new Answer("Не думаю, что это возможно"));
            tempLetter.Answer_1.ReactionText = "У девочки-сиротки началась лихорадка, и вскоре она умерла (район West River заражен)";
            tempLetter.Answer_1.Chosen += ViolWestRiver;

            tempLetter.Answer_2.ReactionText = "Сестра Анна рассказала о Вашей бессердечности. Народ это не одобряет (-10 репутации у народа)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -10) };
            tempLetter.IgnorText = "Вы проигнорировали письмо сестры Анны. Она расстроена (-5 репутации у народа)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -5) };
            //
            tempLetter = Workers.AddLetter(true, 1, "рабочий\nИтан Стоунер", "Господин Мэр, мои друзья с фабрики остались за стеной. Там совсем не осталось провизии. Прошу вас выделить немного еды и людей, чтобы ее доставить.",
              new Answer("Согласен. Выделяю Вам все необходимое (200 монет и один доброволец)"),
              new Answer("У нас нет на это средств"));
            tempLetter.Answer_1.ReactionText = "Люди благодарны Вам за щедрость (+15 репутации)";
            tempLetter.Answer_1.Conditions = new AnswerCondition[] {new AnswerCondition("Money", 200),
                                                                    new AnswerCondition("Volonteer", 1)};
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", 15) };
            tempLetter.Answer_2.ReactionText = "Голодные люди поражены вашем жестокосердечием (-20 репутации)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -20) };
            tempLetter.IgnorText = "Люди огорчены тем, что вы их игнорируете (-30 репутации)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -30) };
            //
            OnDay3Done = true;
        }

        if (DeathStat.Day == 4 && !OnDay4Done)
        {
            tempLetter = Workers.AddLetter(false, 3, "Начальник полиции\nДжеймс Хаммерхед", "К нам обратились еще двое добровольцев. Хотите нанять их?",
                new Answer("Да, мы наймем их (400 денег)"),
                new Answer("Не стоит."));
            tempLetter.Answer_1.ReactionText = "Вы наняли еще двоих людей (+2 добровольца)";
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", 2) };
            tempLetter.IgnorText = "Предложение Джеймса Хаммерхеда больше не действительно";
            //
            tempLetter = Workers.AddLetter(true, 1, "Доктор\nфон Шауб", "Господин мэр, болезнь свирепствует в городе! У нас заканчиваются медикаменты и элементарные средства защиты (тканевые повязки, марля). Мы знаем, что у графини Лоусенс есть запасы ткани на складе в ее поместье, может пообщаетесь с ней?",
                new Answer("Городу нужнее, чем графине. Я отправлю добровольца к ней для конфискации. (один доброволец)"),
                new Answer("Я вас прекрасно понимаю, но к сожалению я не могу отнять у графини ее имущество. Попробуйте справится своими силами."));
            tempLetter.Answer_1.ReactionText = "Насильственные меры понизили вашу репутацию, зато вы смогли ускорить работу над лекарством. (+5 к готовности вакцины, -5 репутации у народа)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] {new AnswerReaction("RegionRep", -5),
                                                                  new AnswerReaction("Vacina", 5)};
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Volonteer", 1) };

            tempLetter.Answer_2.ReactionText = "Насилие - это не выход. Люди стали больше уважать вас. А болезнь подкралась ближе... (+5 репутации у народа, +1 скрытный зараженный дом)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", 5) };
            tempLetter.IgnorText = "Вы проигнорировали просьбу врача. Болезнь стала на шаг ближе... (+1 зараженный дом)";

            OnDay4Done = true;
        }
        if (DeathStat.Day == 5 && !OnDay5Done)
        {
            tempLetter = Workers.AddLetter(true, 2, "Начальник полиции\nДжеймс Хаммерхед", "Господин мэр, сегодня один из наших добровольцев был ранен. Есть веские основания думать, что это месть людей из-за стены. В ближайшее время он не сможет нести службу.",
                new Answer("Печально это слышать. Давайте поддержим его семью (пожертвовать 50 монет)"),
                new Answer("Видимо по хорошему они не понимают. Подготовьте листовки и раскидайте их вдоль стены с предупреждением о том, что все, кто приблизится к стенам будет расстрелян (200 монет)"));
            tempLetter.Answer_1.ReactionText = "Семья раненого передает вам большое спасибо";
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", 2) };
            tempLetter.Answer_2.ReactionText = "Вы потратили немного денег, но горожане теперь чувствуют себя спокойнее (+5 к репутации у народа)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("RegionRep", 5) };
            tempLetter.IgnorText = "Вы проигнорировали обращение начальника полиции. Это вызвало недовольство (-5 к репутации у народа)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("RegionRep", -5) };
            //
            tempLetter = Workers.AddLetter(false, 1, "Доброволец", "Господин Мэр, нам катастрофически не хватает людей, а работа наша сопряжена с большими опасностями, может быть Вы найдете возможно простимулировать волонтеров в это непростое время? Работы сейчас нет, а наши семьи хотят кушать.",
                new Answer("Конечно, я велю выделить средства на предоставление вам дополнительных пайков (150 монет)"),
                new Answer("При всем моем уважении я не могу сейчас этого сделать. Лекарство почти готово. Прошу потерпеть вас буквально пару-тройку дней"));
            tempLetter.Answer_1.ReactionText = "Ваши люди довольны за дополнительные деньги и продолжают служить вам";
            tempLetter.Answer_2.ReactionText = "Люди не хотят работать бесплатно. Двое добровольцев ушли со службы (-2 добровольца)";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", -2), new AnswerReaction("RegionRep", -5) };
            tempLetter.IgnorText = "Вы проигнорировали просьбу добровольцев, и несколько из них покинули службу (-2 добровольца)";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("Volonteer", -2), new AnswerReaction("RegionRep", -5) };

            OnDay5Done = true;
        }


        if (DeathStat.Day == 6 && !OnDay6Done)
        {
            //////////
            tempLetter = Workers.AddLetter(true, 1, "Невеста\nЭлизабет", "Здравствуй, мой милый мой, дорогой. Мой свет, моя жизнь! Я не спала сегодня всю ночь. Мне так страшно! Кажется, болезнь добралась уже до дома Бакстеров на соседней улице. Пожалуйста, давай уедем! Я уверена тетя Рита с радостью приютит нас. Что теперь для нас деньги и должность, когда на кону наша жизнь и будущее!",
              new Answer("Да, это будет лучшим решением. Здесь уже никому не поможешь"),
              new Answer("Я не могу бросить свой город - это для меня дело чести"));
            tempLetter.Answer_1.ReactionText = "Вы предали свой город. Счастливого пути!";
            tempLetter.Answer_1.Chosen += GoWithWife;

            tempLetter.Answer_2.ReactionText = "Элизабет уехала одна.";
            tempLetter.IgnorText = "Вы проигнорировали предложение Элизабет. Возможно, это был ваш последний шанс на спасение";
            //
            tempLetter = Workers.AddLetter(false, 3, "Начальник полиции\nДжеймс Хаммерхед", "К нам обратились еще двое добровольцев. Хотите нанять их?",
                new Answer("Да, мы наймем их (400 денег)"),
                new Answer("Не стоит."));
            tempLetter.Answer_1.ReactionText = "Вы наняли еще двоих людей (+2 добровольца)";
            tempLetter.Answer_1.Conditions = new AnswerCondition[] { new AnswerCondition("Money", 300) };
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", 2) };
            tempLetter.IgnorText = "Предложение Джеймса Хаммерхеда больше не действительно";
            OnDay6Done = true;
        }

        if (DeathStat.Day == 7 && !OnDay7Done)
        {
            //////////
            tempLetter = Workers.AddLetter(true, 1, "Рабочий\nИтан Стоунер", "Уважаемый мэр… хотя нет, о чем это я? Совсем не уважаемый! Ты бросил нас на произвол судьбы, а сам спрятался за высокими стенами. Нам конец и все наши смерти останутся на твоих руках. Однако мы просим о последней услуге и, возможно, хотя бы этим ты сможешь искупить свой грех. Нам нужна еда. У вас есть запасы на складах, спустите хотя бы на веревках часть еды. Возможно это позволит кому-то из нас прожить еще лишних несколько дней. В благодарность мы готовы предоставить несколько трупов зараженных с особо яркими проявлениями болезни для ваших богомерзких вскрытий, да простят нас за это высшие силы.",
              new Answer("Хорошо, мы предоставим вам пайки (100 монет)"),
              new Answer("Нет, вы ничего не получите"));
            tempLetter.Answer_1.ReactionText = "Никто не ожидал, но рабочие сдержали свое слово. Вы потеряли немного денег и доверия граждан, но приблизились к победе над болезнью (+5 репутации у народа, +3 к готовности лекарства)";
            tempLetter.Answer_1.Reactions = new AnswerReaction[] { new AnswerReaction("Vacina", 3), new AnswerReaction("RegionRep", 5) };

            tempLetter.Answer_2.ReactionText = "Вы проигнорировали просьбу рабочих, и один из добровольцев поплатился за это жизнью.";
            tempLetter.Answer_2.Reactions = new AnswerReaction[] { new AnswerReaction("Volonteer", -1) };
            tempLetter.IgnorText = "Вы проигнорировали просьбу рабочих, и один из добровольцев поплатился за это жизнью.";
            tempLetter.IgnorReactions = new AnswerReaction[] { new AnswerReaction("Volonteer", -1) };
            //
            OnDay7Done = true;
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
        
        if (goAwayWithWife)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Вы уехали со своей невестой, оставив город на произвол судьбы.";

            EndGame = true;
        }

        if (DeathStat.RegionReputation <= 20 && !OnRegRepDone)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Опасность пришла, откуда не ждали. Вы полагали, что стена защитит вас, но бедняки взбунтовались и разрушили лабораторию, похоронив вас и ваши труды вместе с собой.";

            OnRegRepDone = true;
            EndGame = true;
        }

        if(DeathStat.AllViol >= 110)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Температура не спадает, лихорадка продожается. Здесь мы бессильны. Наш Мэр был хорошим человеком и боролся до конца. Помолимся же за его душу.";

            EndGame = true;
        }

        if (DeathStat.ImperatorReputation <= 15)
        {
            EndGamePanel.SetActive(true);
            EndGameText.text = "Император: Я возлагал на вас большие надежды. Как жаль, что я ошибся. Кажется, исправлять ничего Вы не собираетесь. Что ж, тогда будем решать вопрос другими методами. На Ваше место я опрделил господина Грина. А Вас ждет тюрьма за государственную измену";

            EndGame = true;
        }

        if (!EndGame)
            LettersManager.Instance.CheckLettersToShow();
    }

    public void ViolWestRiver()
    {
        MainScript.ViolWestRiver = true;
    }
}
