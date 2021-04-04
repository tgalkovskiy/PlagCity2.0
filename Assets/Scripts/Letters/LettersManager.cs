using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersManager : MonoBehaviour
{
    public static LettersManager Instance;

    //Список всех хранящихся писем
    private List<Letter> Letters;
    //Очередь писем на вывод на экран (Письма, которые активируются сразу, если их несколько пришло)
    private List<Letter> LettersToShow;

    //Объекты для отображения письма на экране
    [SerializeField] private GameObject LetterView;
    [SerializeField] private Image LetterMainImage;
    [SerializeField] private Text LetterMainText;
    [SerializeField] private Image LetterAnswer1Image;
    [SerializeField] private Text LetterAnswer1Text;
    [SerializeField] private Text LetterAnswer1ErrorText;
    [SerializeField] private Image LetterAnswer2Image;
    [SerializeField] private Text LetterAnswer2Text;
    [SerializeField] private Text LetterAnswer2ErrorText;
    [SerializeField] private Image LetterActionPut;
    [SerializeField] private Image LetterActionThrow;

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

        ScrollViewRect = ScrollViewOfLetters.GetComponent<ScrollRect>();
        ScrollViewContent = ScrollViewRect.content;
    }

    /// <summary>
    /// Просто убрать текущее письмо
    /// </summary>
    public void PutLetter()
    {
        LetterView.SetActive(false);
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
        LetterView.SetActive(false);
        Letters.Remove(curLetter);

        curLetter.LetterSelected -= OnSelectedLetter;

        Destroy(curLetter.gameObject);

        curLetter = null;
    }

    /// <summary>
    /// Выбор первого варианта ответа
    /// </summary>
    public void OnChooseAnswer_1()
    {
        curLetter.Answer_1.AnswerChosen();
    }

    /// <summary>
    /// Выбор второго варианта ответа
    /// </summary>
    public void OnChooseAnswer_2()
    {
        curLetter.Answer_2.AnswerChosen();
    }

    /// <summary>
    /// Отображение письма на экране при его выборе
    /// </summary>
    /// <param name="letter"></param>
    public void OnSelectedLetter(Letter letter)
    {
        curLetter = letter;

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
        }

        if(!curLetter.Answer_1.IsConditionDone)
        {
            LetterAnswer1Image.GetComponent<Button>().interactable = false;
            LetterAnswer1ErrorText.gameObject.SetActive(true);
        }
        else
            LetterAnswer1ErrorText.gameObject.SetActive(false);

        if (!curLetter.Answer_2.IsConditionDone)
        {
            LetterAnswer2Image.GetComponent<Button>().interactable = false;
            LetterAnswer2ErrorText.gameObject.SetActive(true);
        }
        else
            LetterAnswer2ErrorText.gameObject.SetActive(false);

        LetterActionPut.sprite = curLetter.ActionSprite;
        LetterActionThrow.sprite = curLetter.ActionSprite;

        LetterMainText.text = curLetter.mainText;
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
            OnSelectedLetter(curLetter);
        }
    }

    /// <summary>
    /// Показать ящик с письмами
    /// </summary>
    public void ShowScrollView()
    {
        UpdateContentView();
        ScrollViewOfLetters.SetActive(true);
    }

    /// <summary>
    /// Настройка отображения ящика с письмами
    /// </summary>
    public void UpdateContentView()
    {
        ScrollViewContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,(Letters.Count + 1) * deltaX);
        ScrollViewRect.horizontalNormalizedPosition = 0;

        for (int i = 0; i < Letters.Count; i++)
        {
            RectTransform transform = Letters[i].GetComponent<RectTransform>();

            transform.SetPositionAndRotation(new Vector3((i + 1) * deltaX, Random.Range(minY, maxY), 0), Quaternion.identity);
        }
    }
}
