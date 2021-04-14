using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public static MainScript Instance;

    public GameObject[] DistrictRed, District;
    [SerializeField] private SoundScript SoundScript;
    [SerializeField] private LayerMask MaskUI;
    [SerializeField] private float CameraSens;
    [SerializeField] private GameObject GuiPanel;
    [SerializeField] private Text DayText, TimeGameText, MoneyText, NameDistrictText, AllPeopleDistrictText, NumbersOfViolentText, NumbersOfDeathText, Vacina;
    [SerializeField] Transform PlaceGuiPanelActiv;
    [SerializeField] Transform PlaceGuiPanelDeactiv;
    [SerializeField] private DemoViol[] DemoViol;
    [SerializeField] private ChangeButton[] ChangeButton;
    [SerializeField] private Animator Animator;
    //удалить потом лист
    private List<GameObject> Districts = new List<GameObject>();
    //private SpriteRenderer SpriteRendererDistrict;
    private Camera MainCamera;
    private Vector3 ClicPosition;
    public StateOBJ City;
    private float MouseScrollWheel, CameraX, CameraY, TimeGame;
    [HideInInspector] public GameObject NowGameObj;
    private bool FirstClic = false, GUIAKTIV = false, MainMap = true;
    private int Money, AllDeathPeople, AllViolPeople, Infected, Buried, AllPeople;
    [HideInInspector] public string NameDistrict, NumberPeopleDistrict, NumbersOfViolent, NumbersOfDeath;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //комопонент камеры
        MainCamera = GetComponent<Camera>();
        //City = GetComponent<StateOBJ>();
        TimeGame = 0;
        DeathStat.Money = 250;
        Vacina.text = "50";
        //AllDeathPeople = 0;
        //AllViolPeople = 0;
        Infected = 1;
        Buried = 0;
        //AllPeople = 2100;
        //NumberDistrict = 0;
        //отключение всех зараженых маркеров сразу
        for (int i = 0; i < DistrictRed.Length; i++)
        {
            Districts.Add(District[i]);
        }
        GuiPanel.SetActive(false);

    }

    private void Start()
    {

        SenderManager.Instance.CheckConditions();
        LettersManager.Instance.CheckLettersToShow();


        UpdateUI();
    }

    /// <summary>
    /// Вызов панели сообщений
    /// </summary>
    private void GuiPanelMetod()
    {
        GuiPanel.SetActive(true);
        //Анимация вызова панели 
        var Sequence1 = DOTween.Sequence();
        //трасформ к точке
        Sequence1.Append(GuiPanel.transform.DOMove(PlaceGuiPanelActiv.position, 1f));
        //измение скейла
        Sequence1.Join(GuiPanel.transform.DOScale(Vector3.one * 1.2f, 1f));

    }

    public DemoViol curDemoViol;
    public GameObject buttonSearch;
    public GameObject buttonUnsearch;

    public void SearchDemoViol()
    {
        buttonSearch.SetActive(false);
        buttonUnsearch.SetActive(true);
        curDemoViol.SearchDistrict();
    }
    public void UnSearchDemoViol()
    {
        buttonSearch.SetActive(true);
        buttonUnsearch.SetActive(false);
        curDemoViol.UnsearchDistrict();
    }

    private void CameraTransform()
    {
        if (NameDistrict == "sunland")
        {
            MainCamera.transform.position = new Vector3(-78, 13, -10);
            DemoViol[0].ActiveDistrict = true;
            curDemoViol = DemoViol[0];
        }
        else if (NameDistrict == "west river")
        {
            MainCamera.transform.position = new Vector3(-78, -19, -10);
            DemoViol[1].ActiveDistrict = true;
            curDemoViol = DemoViol[1];
        }
        else if (NameDistrict == "grandstream")
        {
            MainCamera.transform.position = new Vector3(-78, -52, -10);
            DemoViol[2].ActiveDistrict = true;
            curDemoViol = DemoViol[2];
        }


        if (curDemoViol.IsOnSearch)
        {
            buttonSearch.SetActive(false);
            buttonUnsearch.SetActive(true);
        }
        else
        {
            buttonSearch.SetActive(true);
            buttonUnsearch.SetActive(false);
        }
    }

    private void CameraTransforDefold()
    {
        buttonSearch.SetActive(false);
        buttonUnsearch.SetActive(false);
        //Смотреть в миро
        MainCamera.transform.position = new Vector3(0, 0, -10);
        for (int i = 0; i < DemoViol.Length; i++)
        {
            DemoViol[i].ActiveDistrict = false;
        }
        AnShowPanel();
    }

    public void TransformCamera()
    {

        if (MainMap)
        {
            //подписка на события загрузки камеры
            Event.LoadElement += CameraTransform;
            //анимация с двумя ивентами(то что подписали в ивент, и отписка от ивента)
            Animator.SetTrigger("NewWindow");
            //CameraTransform();
            MainMap = false;
        }
        else
        {
            Event.LoadElement += CameraTransforDefold;
            //анимация с двумя ивентами(то что подписали в ивент, и отписка от ивента)
            Animator.SetTrigger("NewWindow");
            //CameraTransforDefold();
            MainMap = true;
        }
    }

    /// <summary>
    /// метод который прячет панель (это классическая баттомка, может быть нажата в любом месте)
    /// </summary>
    public void AnShowPanel()
    {
        var Sequence2 = DOTween.Sequence();
        //движение к трансформу
        Sequence2.Append(GuiPanel.transform.DOMove(PlaceGuiPanelDeactiv.position, 1f));
        //изменение скейла
        Sequence2.Join(GuiPanel.transform.DOScale(Vector3.one * 0.5f, 1f));
    }
    public void EnterMouse()
    {
        GUIAKTIV = true;
    }
    public void ExitMouse()
    {
        GUIAKTIV = false;
    }
    public void Ray()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundScript.ClickAudioSource.PlayOneShot(SoundScript.ClickSound);
            if (GUIAKTIV == false)
            {
                //позиция выстрела луча
                ClicPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                //создение масива хитов для записи полученых данных 
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(ClicPosition, Vector2.zero);
                //проверка хитов
                GuiPanelMetod();
                //вывод инфы о районах/домах/кварталах и прочей херне(написал универсально, сам горжусь) 
                if (hit2D.Length > 0)
                {
                    if (hit2D.Length > 1)
                    {
                        hit2D[1].collider.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        NameDistrict = hit2D[1].collider.name;
                        if (FirstClic == false)
                        {
                            NowGameObj = hit2D[1].collider.gameObject;
                            FirstClic = true;
                        }
                        if (FirstClic == true && NowGameObj != hit2D[1].collider.gameObject)
                        {

                            NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
                            NowGameObj = hit2D[1].collider.gameObject;
                        }
                        if (hit2D[1].collider.gameObject?.GetComponent<StateOBJ>())
                        {
                            NowGameObj = hit2D[1].collider.gameObject;
                            NumberPeopleDistrict = NowGameObj?.GetComponent<StateOBJ>().CountPeople.ToString();
                            NumbersOfDeath = NowGameObj?.GetComponent<StateOBJ>().CountDeath.ToString();
                            NumbersOfViolent = NowGameObj?.GetComponent<StateOBJ>().CountViol.ToString();
                        }
                    }
                    else
                    {
                        if (hit2D[0].collider.gameObject?.GetComponent<StateOBJ>())
                        {
                            NowGameObj = hit2D[0].collider.gameObject;
                            NameDistrict = NowGameObj.name;
                            NumbersOfDeath = NowGameObj?.GetComponent<StateOBJ>().CountDeath.ToString();
                            NumbersOfViolent = NowGameObj?.GetComponent<StateOBJ>().CountViol.ToString();
                            NumberPeopleDistrict = NowGameObj?.GetComponent<StateOBJ>().CountPeople.ToString();
                        }
                    }
                }
                else
                {
                    NameDistrict = "Portstream";
                }
            }
            Debug.Log($"Выбран {NameDistrict}");
        }
    }

    /// <summary>
    /// движение камеры(настроил+Делагаты)
    /// </summary>
    private void ControlCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //считывание вращения колесика
            MouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            //приближение и отдаление
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + MouseScrollWheel * 2, 5, 14);
            //при максимальном отдалении уход на стартовую позицию камеры
            if (Camera.main.orthographicSize == 14 && MainMap == true)
            {
                transform.position = new Vector3(0, 0, transform.position.z);
            }
        }
        //движение камеры
        if (Input.GetMouseButton(1) && Camera.main.orthographicSize < 14 && MainMap == true)
        {
            CameraX = Input.GetAxis("Mouse X") * CameraSens;
            CameraY = Input.GetAxis("Mouse Y") * CameraSens;
            //добавить делегат для новой карты
            float CameraPosX = Mathf.Clamp(CameraX + transform.position.x, -11f, 11f);
            float CameraPosY = Mathf.Clamp(CameraY + transform.position.y, -10f, 10f);
            transform.position = new Vector3(CameraPosX, CameraPosY, transform.position.z);
        }

    }

    private void MoneyCalculate()
    {
        //Переписать
        DeathStat.Money += 120;
    }


    public Slider impRepSlider;
    public Slider peopleRepSlider;

    public Image impRepFillImage;
    public Image peopleRepFillImage;

    public Text policeMax;
    public Text policeCur;
    public Text doctorMax;
    public Text doctorCur;
    public Text volontMax;
    public Text volontCur;

    /// <summary>
    /// Кнопка нового дня
    /// </summary>
    public void NextDay()
    {
        DeathStat.Day += 1;
        TimeGame = 0;
        DeathStat.Vacina += 3;
        City.CountDeath = DeathStat.AllDeath;
        MoneyCalculate();

        CheckDistrictsToSearch();

        foreach (var d in DemoViol)
            d.NextDayStateObj();

        CameraTransforDefold();

        if (ViolWestRiver)
            ViolWestRiverNow();

        LettersManager.Instance.OnNewDay();
        SenderManager.Instance.CheckConditions();


        DeathStat.OnNewDay();
        UpdateUI();

        isDayDone = false;
        Debug.Log($"Day: {DeathStat.Day}");
    }

    public void UpdateUI()
    {
        Vacina.text = DeathStat.Vacina.ToString();

        impRepSlider.value = DeathStat.ImperatorReputation;
        peopleRepSlider.value = DeathStat.RegionReputation;

        if (impRepSlider.value <= 15)
            impRepFillImage.color = Color.red;
        else if (impRepSlider.value <= 50 && impRepSlider.value > 15)
            impRepFillImage.color = Color.yellow;
        else
            impRepFillImage.color = Color.green;

        if (peopleRepSlider.value <= 15)
            peopleRepFillImage.color = Color.red;
        else if (peopleRepSlider.value <= 50 && peopleRepSlider.value > 15)
            peopleRepFillImage.color = Color.yellow;
        else
            peopleRepFillImage.color = Color.green;

        policeMax.text = DeathStat.MaxPolicemen.ToString();
        policeCur.text = DeathStat.Policemen.ToString();
        doctorMax.text = DeathStat.MaxDoctors.ToString();
        doctorCur.text = DeathStat.Doctors.ToString();
        volontMax.text = DeathStat.MaxVolunteers.ToString();
        volontCur.text = DeathStat.Volunteers.ToString();
    }

    private void TextInfoAll()
    {
        TimeGameText.text = ((int)TimeGame).ToString();
        DayText.text = "Day: " + DeathStat.Day.ToString();
        MoneyText.text = DeathStat.Money.ToString();
        NameDistrictText.text = NameDistrict;
        AllPeopleDistrictText.text = "People: " + NumberPeopleDistrict;
        NumbersOfViolentText.text = "Vialent: " + NumbersOfViolent;
        NumbersOfDeathText.text = "Death: " + NumbersOfDeath;
    }

    bool isDayDone = false; //чтобы 300 раз не подписывался на НекстДей
    bool IsGoNextDay = false;

    public void GoNextDay()
    {
        IsGoNextDay = true;
    }

    void Update()
    {
        TimeGame += Time.deltaTime / 4;
        TextInfoAll();
        if ((Input.GetKeyDown(KeyCode.Space) || TimeGame >= 24 || IsGoNextDay) && !isDayDone)
        {
            isDayDone = true;
            IsGoNextDay = false;
            //подписка на события загрузки камеры
            Event.LoadElement += NextDay;
            //анимация с двумя ивентами(то что подписали в ивент, и отписка от ивента)
            Animator.SetTrigger("NewWindow");
        }
        Ray();
        ControlCamera();
        if (DeathStat.Vacina >= 100)
        {
            Debug.Log("Победа");
        }
    }

    public List<DemoViol> districsToSearch = new List<DemoViol>();

    public void CheckDistrictsToSearch()
    {
        if (districsToSearch.Count == 0)
            return;

        foreach (var d in districsToSearch)
        {
            var HousesDistrict = d.Houses;
            for (int i = 0; i < HousesDistrict.Length; i++)
            {
                var HousesDistrictNow = HousesDistrict[i].GetComponent<StateOBJ>();
                if (HousesDistrictNow.CountHideViol > 0)
                {
                    HousesDistrictNow.ViolLine.SetActive(true);
                    HousesDistrictNow.SearchHouse();
                }
            }

            d.IsOnSearch = false;
        }

        districsToSearch.Clear();
    }

    public bool ViolWestRiver = false;
    public void ViolWestRiverNow()
    {
        ViolWestRiver = false;

        DemoViol[1].Houses[3].GetComponent<StateOBJ>().CountDeath += 1;
        DemoViol[1].Houses[3].GetComponent<StateOBJ>().CountViol += 3;
        DeathStat.AllDeath += 1;
        DeathStat.AllViol += 3;
        DeathStat.NewDeadPeople += 1;
        DeathStat.NewViolPeople += 3;
        DemoViol[1].Houses[3].GetComponent<StateOBJ>().IsHide = false;
        DemoViol[1].Houses[3].GetComponent<StateOBJ>().StateViol = true;
        DemoViol[1].Houses[3].GetComponent<StateOBJ>().IconViol();

        DemoViol[1].DistricdState();
    }
}
