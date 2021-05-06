using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersManager : MonoBehaviour
{
    public static LettersManager Instance;

    public MainScript main;
    public SoundController SoundController;

    //Список всех хранящихся писем
    private List<Letter> Letters;
    //Очередь писем на вывод на экран (Письма, которые активируются сразу, если их несколько пришло)
    private List<Letter> LettersToShow;

    private List<AnswerReaction> Reactions;

    //Объекты для отображения письма на экране
    [SerializeField] private GameObject LetterView;
    [SerializeField] private Image LetterMainImage;
    [SerializeField] private Text LetterMainText;

    [SerializeField] private Image TutorialImage;

    [SerializeField] private Text LetterSenderName;
    [SerializeField] private Image LetterAnswer1Image;
    [SerializeField] private Text LetterAnswer1Text;
    [SerializeField] private Text LetterAnswer1ErrorText;
    [SerializeField] private Image LetterAnswer2Image;
    [SerializeField] private Text LetterAnswer2Text;
    [SerializeField] private Text LetterAnswer2ErrorText;
    [SerializeField] private Image LetterActionPut;
    [SerializeField] private Image LetterActionThrow;

    public string itogiText;

    [SerializeField] private GameObject ItogiView;
    [SerializeField] private Text ItogiText;
    [SerializeField] private Text DayText;
    [SerializeField] private Text NewViolText;
    [SerializeField] private Text NewDeathsText;
    [SerializeField] private Text AllViolText;
    [SerializeField] private Text AllDeathsText;
    [SerializeField] private Text VacinaText;

    //Текущее письмо, которое отображается на экране
    [SerializeField] private Letter curLetter;


    //Скролл для отображения писем в ящике
    [SerializeField] private GameObject ScrollViewOfLetters;
    //Объекты для настройки скролла и писем внутри
    private ScrollRect ScrollViewRect;
    private RectTransform ScrollViewContent;
    /// <summary>
    /// Родитель для новых писем
    /// </summary>
    public RectTransform LettersParent => ScrollViewContent;

    //Циферки для отображения писем в ящике
    [SerializeField] private int minY;
    [SerializeField] private int maxY;
    [SerializeField] private int deltaX;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Letters = new List<Letter>();
        LettersToShow = new List<Letter>();
        Reactions = new List<AnswerReaction>();

        ScrollViewRect = ScrollViewOfLetters.GetComponent<ScrollRect>();
        ScrollViewContent = ScrollViewRect.content;

        main.state = GameState.City;
    }

    /// <summary>
    /// Получение нового письма
    /// </summary>
    /// <param name="letter"></param>
    public void AddLetter(Letter letter)
    {
        Letters.Add(letter);
        letter.LetterSelected += OnSelectedLetter;
    }

    /// <summary>
    /// Выбросить текущее письмо
    /// </summary>
    public void DeleteLetter()
    {
        Letters.Remove(curLetter);

        curLetter.LetterSelected -= OnSelectedLetter;
        curLetter.OnDelete();

        Destroy(curLetter.gameObject);

        curLetter = null;

        CloseLetterView();
    }

    public void PutLetter()
    {
        SoundController.PlayCloseLetter();
        CloseLetterView();
    }

    public void ThrowAwayLetter()
    {
        SoundController.PlayThrowawayLetter();
        IgnorLetter();
    }

    public void IgnorLetter()
    {
        curLetter.OnIgnored();
        DeleteLetter();
    }

    /// <summary>
    /// Выбор первого варианта ответа
    /// </summary>
    public void OnChooseAnswer_1()
    {
        if (curLetter.IsActual)
        {
            SoundController.PlayAnswerLetter();
            curLetter.Answer_1.AnswerChosen();
        }
        else
            SoundController.PlayThrowawayLetter();
        DeleteLetter();
    }

    /// <summary>
    /// Выбор второго варианта ответа
    /// </summary>
    public void OnChooseAnswer_2()
    {
        if (curLetter.IsActual)
        {
            SoundController.PlayAnswerLetter();
            curLetter.Answer_2.AnswerChosen();
        }
        else
            SoundController.PlayThrowawayLetter();
        DeleteLetter();
    }

    private void OnSelectedLetter(Letter letter)
    {
        SoundController.PlayOpenLetter();
        ShowLetter(letter);
    }

    /// <summary>
    /// Отображение письма на экране
    /// </summary>
    /// <param name="letter"></param>
    public void ShowLetter(Letter letter)
    {
        curLetter = letter;

        var rect = LetterMainText.GetComponent<RectTransform>();
        if (curLetter.TutorialSprite != null)
        {
            TutorialImage.gameObject.SetActive(true);
            TutorialImage.sprite = curLetter.TutorialSprite;
            rect.offsetMin = new Vector2(rect.offsetMin.x, 430f);
        }
        else
        {
            TutorialImage.gameObject.SetActive(false);
            rect.offsetMin = new Vector2(rect.offsetMin.x, 101f);
        }

        LetterAnswer1Image.GetComponent<Button>().interactable = true;
        LetterAnswer2Image.GetComponent<Button>().interactable = true;

        if (!curLetter.Answer_1.IsConditionsDone)
        {
            LetterAnswer1Image.GetComponent<Button>().interactable = false;
            LetterAnswer1ErrorText.gameObject.SetActive(true);
        }
        else
        {
            LetterAnswer1ErrorText.gameObject.SetActive(false);
        }

        if (!curLetter.Answer_2.IsConditionsDone)
        {
            LetterAnswer2Image.GetComponent<Button>().interactable = false;
            LetterAnswer2ErrorText.gameObject.SetActive(true);
        }
        else
        {
            LetterAnswer2ErrorText.gameObject.SetActive(false);
        }

        //Настройка картиночек и текста для письма
        if (curLetter.IsActual) //Если письмо еще актуальное(не просрочилось)
        {
            LetterMainImage.sprite = curLetter.ActualSprite;
            LetterAnswer1Image.sprite = curLetter.AnswerActualSprite;
            LetterAnswer2Image.sprite = curLetter.AnswerActualSprite;
        }
        else
        {
            LetterMainImage.sprite = curLetter.UnactualSprite;
            LetterAnswer1Image.sprite = curLetter.AnswerUnactualSprite;
            LetterAnswer2Image.sprite = curLetter.AnswerUnactualSprite;
            LetterAnswer1Image.GetComponent<Button>().interactable = false;
            LetterAnswer2Image.GetComponent<Button>().interactable = false;
        }

        LetterActionPut.sprite = curLetter.ActionSprite;
        LetterActionThrow.sprite = curLetter.ActionSprite;

        LetterMainText.text = curLetter.mainText;
        LetterSenderName.text = curLetter.SenderName;
        LetterAnswer1Text.text = curLetter.Answer_1.Text;
        LetterAnswer2Text.text = curLetter.Answer_2.Text;
        
        if (curLetter.Answer_1.Text == "") //Если текста в варианте ответа нет, скрываются кнопки с вариантами ответов
        {
            LetterAnswer1Image.gameObject.SetActive(false);
            LetterAnswer2Image.gameObject.SetActive(false);
        }
        else
        {
            LetterAnswer1Image.gameObject.SetActive(true);
            LetterAnswer2Image.gameObject.SetActive(true);
        }

        ScrollViewOfLetters.SetActive(false);
        LetterView.SetActive(true);
        
    }

    /// <summary>
    /// Добавить письмо в очередь на вывод на экран
    /// </summary>
    /// <param name="letter"></param>
    public void AddLetterToShow(Letter letter)
    {
        Letters.Add(letter);
        LettersToShow.Add(letter);
        letter.LetterSelected += OnSelectedLetter;
    }
    
    /// <summary>
    /// Закрыть панель с отображение письма
    /// </summary>
    public void CloseLetterView()
    {
        LetterView.SetActive(false);
        if (main.state == GameState.Office)
            ShowScrollView();
        else
            CheckLettersToShow();
    }

    /// <summary>
    /// Проверка очереди на вывод на экран
    /// </summary>
    public void CheckLettersToShow()
    {
        if (LettersToShow.Count > 0)
        {
            curLetter = LettersToShow[0];
            LettersToShow.RemoveAt(0);
            ShowLetter(curLetter);
        }
    }
    
    /// <summary>
    /// Показать ящик с письмами
    /// </summary>
    public void ShowScrollView()
    {
        main.state = GameState.Office;
        UpdateContentView();
        ScrollViewOfLetters.SetActive(true);
    }
    
    public void CloseScrollView()
    {
        main.state = GameState.City;
        ScrollViewOfLetters.SetActive(false);
    }

    /// <summary>
    /// Настройка отображения ящика с письмами
    /// </summary>
    public void UpdateContentView()
    {
        ScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,(Letters.Count) * deltaX * 2 + deltaX);
        ScrollViewRect.horizontalNormalizedPosition = 0;

        for (int i = 0; i < Letters.Count; i++)
        {
            RectTransform transform = Letters[i].GetComponent<RectTransform>();

            transform.SetPositionAndRotation(new Vector3((i + 1) * deltaX, Random.Range(minY, maxY), 0), Quaternion.identity);
        }
    }

    public void AddConditions(AnswerCondition[] conditions)
    {
        foreach (var c in conditions)
            c.ApplyCondition();

        main.UpdateUI();
    }

    public void AddReactions(AnswerReaction[] reactions)
    {
        Reactions.AddRange(reactions);
    }

    public void CheckReactions()
    {
        foreach (var c in Reactions)
        {
            c.ApplyReaction();
        }

        if (itogiText == "")
            itogiText = "Особых событий не произошло";

        Reactions.Clear();
    }

    public void ShowItogi()
    {
        ItogiView.SetActive(true);
        ItogiText.text = itogiText;
        DayText.text = MainData.Day.ToString();
        NewViolText.text = MainData.NewInfectedPeople.ToString();
        NewDeathsText.text = MainData.NewDeadPeople.ToString();
        AllViolText.text = MainData.AllInfected.ToString();
        AllDeathsText.text = MainData.AllDeath.ToString();
        VacinaText.text = MainData.Vacina.ToString();

        itogiText = "";
    }

    public void OnNewDay()
    {
        LetterView.SetActive(false);
        ItogiView.SetActive(false);
        ScrollViewOfLetters.SetActive(false);

        foreach(var l in Letters)
            if(l.IsActual)
            {
                l.LifeTimeInDays--;
                if (l.LifeTimeInDays <= 0)
                    l.OnIgnored();
            }

        CheckReactions();

        ShowItogi();
    }
}
