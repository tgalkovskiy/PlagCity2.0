using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitorsManager : MonoBehaviour
{

    public static VisitorsManager Instance;

    public MainScript main;
    public SoundController SoundController;

    //Список всех хранящихся писем
    private List<Visitor> Visitors;
    //Очередь писем на вывод на экран (Письма, которые активируются сразу, если их несколько пришло)
    private List<Visitor> VisitorsToShow;

    private List<AnswerReaction> Reactions;

    //Объекты для отображения письма на экране
    [SerializeField] private GameObject VisitorView;
    [SerializeField] private Image VisitorImage;
    [SerializeField] private Text VisitorText;
    [SerializeField] private Text VisitorName;

    [SerializeField] private Button VisitorAnswer1Button;
    [SerializeField] private Text VisitorAnswer1Text;
    [SerializeField] private Text VisitorAnswer1ErrorText;
    [SerializeField] private Button VisitorAnswer2Button;
    [SerializeField] private Text VisitorAnswer2Text;
    [SerializeField] private Text VisitorAnswer2ErrorText;
    [SerializeField] private Button VisitorAnswer3Button;
    [SerializeField] private Text VisitorAnswer3Text;
    [SerializeField] private Text VisitorAnswer3ErrorText;

    //Текущее письмо, которое отображается на экране
    [SerializeField] private Visitor curVisitor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Visitors = new List<Visitor>();
        VisitorsToShow = new List<Visitor>();
        Reactions = new List<AnswerReaction>();

        main.state = GameState.City;
    }

    /// <summary>
    /// Получение нового письма
    /// </summary>
    /// <param name="visitor"></param>
    public void AddVisitor(Visitor visitor)
    {
        Visitors.Add(visitor);
        visitor.VisitorSelected += OnSelectedVisitor;
    }

    /// <summary>
    /// Выбросить текущее письмо
    /// </summary>
    public void DeleteVisitor()
    {
        Visitors.Remove(curVisitor);

        curVisitor.VisitorSelected -= OnSelectedVisitor;
        curVisitor.OnDelete();
        
        Destroy(curVisitor.gameObject);

        curVisitor = null;

        CloseVisitorView();
    }

    public void IgnorLetter()
    {
        curVisitor.Ignored();
        DeleteVisitor();
    }

    /// <summary>
    /// Выбор первого варианта ответа
    /// </summary>
    public void OnChooseAnswer_1()
    {
        curVisitor.Answer_1.AnswerChosen();
        DeleteVisitor();
    }

    /// <summary>
    /// Выбор второго варианта ответа
    /// </summary>
    public void OnChooseAnswer_2()
    {
        curVisitor.Answer_2.AnswerChosen();
        DeleteVisitor();
    }
    public void OnChooseAnswer_3()
    {
        curVisitor.Answer_3.AnswerChosen();
        DeleteVisitor();
    }

    private void OnSelectedVisitor(Visitor visitor)
    {
        ShowVisitor(visitor);
    }

    /// <summary>
    /// Отображение письма на экране
    /// </summary>
    /// <param name="letter"></param>
    public void ShowVisitor(Visitor visitor)
    {
        curVisitor = visitor;

        VisitorImage.sprite = curVisitor.VisitorSprite;

        if (!curVisitor.Answer_1.IsConditionsDone)
        {
            VisitorAnswer1Button.interactable = false;
            VisitorAnswer1ErrorText.gameObject.SetActive(true);
        }
        else
        {
            VisitorAnswer1Button.interactable = true;
            VisitorAnswer1ErrorText.gameObject.SetActive(false);
        }

        if (!curVisitor.Answer_2.IsConditionsDone)
        {
            VisitorAnswer2Button.interactable = false;
            VisitorAnswer2ErrorText.gameObject.SetActive(true);
        }
        else
        {
            VisitorAnswer2Button.interactable = true;
            VisitorAnswer2ErrorText.gameObject.SetActive(false);
        }

        if (curVisitor.Answer_3 == null)
            VisitorAnswer3Button.gameObject.SetActive(false);
        else
        {
            VisitorAnswer3Button.gameObject.SetActive(true);
            if (!curVisitor.Answer_3.IsConditionsDone)
            {
                VisitorAnswer3Button.interactable = false;
                VisitorAnswer3ErrorText.gameObject.SetActive(true);
            }
            else
            {
                VisitorAnswer3Button.interactable = true;
                VisitorAnswer3ErrorText.gameObject.SetActive(false);
            }
        }

        VisitorText.text = curVisitor.Text;
        VisitorName.text = curVisitor.Name;
        VisitorAnswer1Text.text = curVisitor.Answer_1.Text;
        VisitorAnswer2Text.text = curVisitor.Answer_2.Text;
        if(curVisitor.Answer_3 != null)
            VisitorAnswer3Text.text = curVisitor.Answer_3.Text;

        VisitorView.SetActive(true);
    }

    /// <summary>
    /// Добавить письмо в очередь на вывод на экран
    /// </summary>
    /// <param name="letter"></param>
    public void AddVisitorToShow(Visitor visitor)
    {
        Visitors.Add(visitor);
        VisitorsToShow.Add(visitor);
        visitor.VisitorSelected += OnSelectedVisitor;
    }

    /// <summary>
    /// Закрыть панель с отображение письма
    /// </summary>
    public void CloseVisitorView()
    {
        VisitorView.SetActive(false);
        CheckVisitorToShow();
    }

    /// <summary>
    /// Проверка очереди на вывод на экран
    /// </summary>
    public void CheckVisitorToShow()
    {
        if (VisitorsToShow.Count > 0)
        {
            curVisitor = VisitorsToShow[0];
            VisitorsToShow.RemoveAt(0);
            ShowVisitor(curVisitor);
        }
        else
            LettersManager.Instance.CheckLettersToShow();
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

        Reactions.Clear();
    }

    public void OnNewDay()
    {
        VisitorView.SetActive(false);

        foreach (var l in Visitors)
            l.Ignored();

        CheckReactions();
    }


}
