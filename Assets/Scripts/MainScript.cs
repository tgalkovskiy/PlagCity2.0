using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    City,
    Office,
    Pause
}

public class MainScript : MonoBehaviour
{
    public static MainScript Instance;

    public GameObject[] DistrictRed, District;
    [SerializeField] private SoundController SoundController;
    [SerializeField] private LayerMask MaskUI;
    [SerializeField] private float CameraSens;
    [SerializeField] private GameObject GuiPanel;
    [SerializeField] private Text DayText, TimeGameText, MoneyText, NameDistrictText, AllPeopleDistrictText, NumbersOfViolentText, NumbersOfDeathText, Vacina;
    [SerializeField] Transform PlaceGuiPanelActiv;
    [SerializeField] Transform PlaceGuiPanelDeactiv;
    [SerializeField] private DemoViol[] AllDistricts;
    [SerializeField] private DemoViol[] RichDistricts;
    public List<DemoViol> RichUnriotDistricts = new List<DemoViol>();
    [SerializeField] private HumanResourсeManager[] ChangeButton;
    [SerializeField] private Animator Animator;
    //удалить потом лист
    private List<GameObject> Districts = new List<GameObject>();
    //private SpriteRenderer SpriteRendererDistrict;
    private Camera MainCamera;
    private Vector3 ClickPosition;
    public StateOBJ City;
    private float MouseScrollWheel, CameraX, CameraY, TimeGame, Minutes;
    [HideInInspector] public GameObject NowGameObj;
    [HideInInspector] public StateOBJ NowStateObj;
    private bool FirstClick = false, GUIAKTIV = false, MainMap = true;
    private int Money, AllDeathPeople, AllViolPeople, Infected, Buried, AllPeople;
    [HideInInspector] public string NameDistrict, NumberPeopleDistrict, NumbersOfViolent, NumbersOfDeath;


    public GameState state;


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
        MainData.Money = 50;
        Vacina.text = "50";
        //AllDeathPeople = 0;
        //AllViolPeople = 0;
        Infected = 3;
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
        VisitorsManager.Instance.CheckVisitorToShow();

        RichUnriotDistricts.AddRange(RichDistricts);

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


    private void CameraTransform()
    {
        if (NameDistrict == "Sunland")
        {
            MainCamera.transform.position = new Vector3(-78, 13, -10);
            AllDistricts[0].ActiveDistrict = true;
            curDemoViol = AllDistricts[0];
        }
        else if (NameDistrict == "West River")
        {
            MainCamera.transform.position = new Vector3(-78, -19, -10);
            AllDistricts[1].ActiveDistrict = true;
            curDemoViol = AllDistricts[1];
        }
        else if (NameDistrict == "Grandstream")
        {
            MainCamera.transform.position = new Vector3(-78, -52, -10);
            AllDistricts[2].ActiveDistrict = true;
            curDemoViol = AllDistricts[2];
        }
        else
        {
            MainMap = true;
            return;
        }

        if (curDemoViol.IsRiot)
            SoundController.Instance.PlayRiotDistrict();

        foreach (var a in ActionButtons)
            a.OnDistrictChange();

    }

    private void CameraTransformDefold()
    {
        //Смотреть в миро
        MainCamera.transform.position = new Vector3(0, 0, -10);
        for (int i = 0; i < AllDistricts.Length; i++)
        {
            AllDistricts[i].ActiveDistrict = false;
        }

        SoundController.Instance.StopPlayingLowerSounds();
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
            Event.LoadElement += CameraTransformDefold;
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
            Debug.Log($"GUIAKTIV: {GUIAKTIV}");
            if (GUIAKTIV == false)
            {
                //позиция выстрела луча
                ClickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                //создение масива хитов для записи полученых данных 
                RaycastHit2D[] hit2D = Physics2D.RaycastAll(ClickPosition, Vector2.zero);
                //проверка хитов
                GuiPanelMetod();
                //вывод инфы о районах/домах/кварталах и прочей херне(написал универсально, сам горжусь) 
                if (hit2D.Length > 0)
                {
                    Debug.Log($"Hits count = {hit2D.Length}");


                    foreach (var h in hit2D)
                        Debug.Log($"{h.collider.gameObject}");

                    foreach (var h in hit2D)
                        if (h.collider.GetComponent<ActionButton>())
                        {
                            if (h.collider.GetComponent<ActionButton>().IsActivated)
                            {
                                h.collider.GetComponent<ActionButton>().OnClick();
                                return;
                            }
                        }

                    if (curDemoViol != null)
                    {
                        foreach (var a in ActionButtons)
                            a.Deactivate();
                        //Кладбище всегда активно
                        ActionButtons[0].Activate();
                        curDemoViol.DeactivateRoads();
                        curDemoViol = null;
                    }


                    if (NowGameObj != null && NowStateObj != null)
                    {
                        if (NowStateObj.TypeStateDis != TypeState.City && NowStateObj.TypeStateDis != TypeState.LocalDistrict)
                        {
                            NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
                            NowGameObj = null;
                            NowStateObj = null;
                        }
                    }

                    GameObject temp = null;

                    foreach (var h in hit2D)
                    {
                        if (NowStateObj == null)
                        {
                            NowGameObj = h.collider.gameObject;
                            NowStateObj = NowGameObj.GetComponent<StateOBJ>();

                            if (NowStateObj.TypeStateDis != TypeState.City && NowStateObj.TypeStateDis != TypeState.LocalDistrict)
                            {
                                NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            }

                            NameDistrict = NowGameObj.name;
                            NumberPeopleDistrict = NowStateObj.CountPeople.ToString();
                            NumbersOfDeath = NowStateObj.CountDeath.ToString();
                            NumbersOfViolent = NowStateObj.CountInfected.ToString();
                        }
                        else
                        {
                            temp = NowGameObj;

                            NowGameObj = h.collider.gameObject;
                            NowStateObj = NowGameObj.GetComponent<StateOBJ>();

                            if (NowStateObj.TypeStateDis != TypeState.City && NowStateObj.TypeStateDis != TypeState.LocalDistrict)
                            {
                                NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;

                                NameDistrict = NowGameObj.name;
                                NumberPeopleDistrict = NowStateObj.CountPeople.ToString();
                                NumbersOfDeath = NowStateObj.CountDeath.ToString();
                                NumbersOfViolent = NowStateObj.CountInfected.ToString();
                            }
                        }

                        if (curDemoViol == null)
                        {
                            curDemoViol = NowGameObj.GetComponentInChildren<DemoViol>();
                            if (curDemoViol != null)
                                foreach (var a in ActionButtons)
                                    a.OnDistrictChange();
                        }

                        if (!(NowStateObj.TypeStateDis != TypeState.City && NowStateObj.TypeStateDis != TypeState.LocalDistrict) && temp != null)
                        {
                            NowGameObj = temp;
                            NowStateObj = NowGameObj.GetComponent<StateOBJ>();
                        }
                    }

                    //if (hit2D.Length > 1)
                    //{
                    //    hit2D[1].collider.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    //    NameDistrict = hit2D[1].collider.name;
                    //    if (FirstClick == false)
                    //    {
                    //        NowGameObj = hit2D[1].collider.gameObject;
                    //        FirstClick = true;
                    //    }
                    //    if (FirstClick == true && NowGameObj != hit2D[1].collider.gameObject)
                    //    {
                    //        NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    //        NowGameObj = hit2D[1].collider.gameObject;
                    //    }
                    //    if (hit2D[1].collider.gameObject?.GetComponent<StateOBJ>())
                    //    {
                    //        NowGameObj = hit2D[1].collider.gameObject;
                    //        NumberPeopleDistrict = NowGameObj?.GetComponent<StateOBJ>().CountPeople.ToString();
                    //        NumbersOfDeath = NowGameObj?.GetComponent<StateOBJ>().CountDeath.ToString();
                    //        NumbersOfViolent = NowGameObj?.GetComponent<StateOBJ>().CountInfected.ToString();
                    //    }
                    //    curDemoViol = NowGameObj.GetComponentInChildren<DemoViol>();
                    //    if (curDemoViol != null)
                    //        foreach (var a in ActionButtons)
                    //            a.OnDistrictChange();
                    //    NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    //}
                    //else
                    //{
                    //    if (hit2D[0].collider.gameObject?.GetComponent<StateOBJ>())
                    //    {
                    //        if (NowGameObj != null)
                    //            if (NowGameObj != hit2D[0].collider.gameObject)
                    //                if (NowGameObj.GetComponent<StateOBJ>().TypeStateDis != TypeState.City)
                    //                    NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = -1;

                    //        NowGameObj = hit2D[0].collider.gameObject;
                    //        curDemoViol = NowGameObj.GetComponentInChildren<DemoViol>();
                    //        if (curDemoViol != null)
                    //        {
                    //            if (!curDemoViol.ActiveDistrict)
                    //                NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    //            foreach (var a in ActionButtons)
                    //                a.OnDistrictChange();
                    //        }
                    //        NameDistrict = NowGameObj.name;
                    //        NumbersOfDeath = NowGameObj?.GetComponent<StateOBJ>().CountDeath.ToString();
                    //        NumbersOfViolent = NowGameObj?.GetComponent<StateOBJ>().CountInfected.ToString();
                    //        NumberPeopleDistrict = NowGameObj?.GetComponent<StateOBJ>().CountPeople.ToString();
                    //    }
                    //}
                }
                else
                {
                    NameDistrict = "Portstream";
                }
            }

            if (curDemoViol != null)
                curDemoViol.ActivateRoads();

            SoundController.PlayClickInGame();

            //Debug.Log($"Выбран {NameDistrict}");
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
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - MouseScrollWheel * 4, 5, 14);
            //при максимальном отдалении уход на стартовую позицию камеры
            if (Camera.main.orthographicSize == 14 && MainMap == true)
            {
                transform.position = new Vector3(0, 0, transform.position.z);
            }
        }
        //движение камеры
        if ((Input.GetMouseButton(1) || Input.GetMouseButton(2)) && Camera.main.orthographicSize < 14 && MainMap == true)
        {
            CameraX = Input.GetAxis("Mouse X") * CameraSens;
            CameraY = Input.GetAxis("Mouse Y") * CameraSens;
            //добавить делегат для новой карты
            float CameraPosX = Mathf.Clamp(-CameraX + transform.position.x, -11f, 11f);
            float CameraPosY = Mathf.Clamp(-CameraY + transform.position.y, -10f, 10f);
            transform.position = new Vector3(CameraPosX, CameraPosY, transform.position.z);
        }

    }

    private void MoneyCalculate()
    {
        foreach (var d in RichDistricts)
            if (!d.IsRiot)
                MainData.Money += MainData.MoneyPerRichDistrict;
            else
                MainData.Money += MainData.MoneyPerRiotRichDistrict;
    }

    [Header("UI")]

    public GameObject menuPanel;

    public Slider impRepSlider;
    public Image impRepFillImage;

    public Slider workersRepSlider;
    public Image workersRepFillImage;

    public Slider richRepSlider;
    public Image richRepFillImage;

    public Slider poorRepSlider;
    public Image poorRepFillImage;

    public Text policeMax;
    public Text policeCur;
    public Text doctorMax;
    public Text doctorCur;
    public Text volontMax;
    public Text volontCur;

    public Text UnburiedPeople;
    /// <summary>
    /// Кнопка нового дня
    /// </summary>

    [SerializeField] private ActionButton[] ActionButtons;

    public void NextDay()
    {
        SoundController.PlayNewDay();

        MainMap = true;
        MainData.Day += 1;
        TimeGame = 0;
        MainData.Vacina += 3;
        City.CountDeath = MainData.AllDeath;
        MainData.WorkersReputation -= MainData.WorkersRepPerDay;

        CheckDistrictsToSearch();

        foreach (var d in AllDistricts)
            d.NextDayStateObj();

        if (NowGameObj != null)
        {
            FirstClick = false;
            if (NowGameObj != null && NowStateObj != null)
            {
                if (NowStateObj.TypeStateDis != TypeState.City && NowStateObj.TypeStateDis != TypeState.LocalDistrict)
                {
                    NowGameObj.GetComponent<SpriteRenderer>().sortingOrder = -1;
                    NowGameObj = null;
                    NowStateObj = null;
                }
            }
            if (curDemoViol != null)
                curDemoViol.DeactivateRoads();
            curDemoViol = null;
        }

        CameraTransformDefold();

        if (ViolWestRiver)
            ViolWestRiverNow();

        if (InfectTwoHousesInSunland)
            InfectTwoHousesInSunlandNow();

        if (TimeStartDayPlus4)
        {
            TimeGame = 4;
            TimeStartDayPlus4 = false;
        }

        if (InfectRandomHouse)
            InfectRandomHouseNow();

        if (Recovery5Houses)
            Recovery5HousesNow();

        LettersManager.Instance.OnNewDay();
        SenderManager.Instance.CheckConditions();


        if (MainData.RichReputation <= MainData.MinRichReputation)
        {
            if (RichUnriotDistricts.Count > 0)
            {
                int i = UnityEngine.Random.Range(0, RichUnriotDistricts.Count);
                RichUnriotDistricts[i].MakeRiot();
                RichUnriotDistricts.RemoveAt(i);
            }
        }

        foreach (var d in AllDistricts)
            if (d.IsOnSuppressRiot)
                d.SuppressRiot();

        MoneyCalculate();

        MainData.OnNewDay();

        foreach (var a in ActionButtons)
            a.OnNewDay();

        UpdateUI();
        TextInfoAll();

        isDayDone = false;

        state = GameState.Pause;
        Debug.Log($"Day: {MainData.Day}");
    }

    public void UpdateUI()
    {
        Vacina.text = MainData.Vacina.ToString();

        impRepSlider.value = MainData.ImperatorReputation;
        workersRepSlider.value = MainData.WorkersReputation;
        richRepSlider.value = MainData.RichReputation;
        poorRepSlider.value = MainData.PoorReputation;

        if (impRepSlider.value <= 15)
            impRepFillImage.color = Color.red;
        else if (impRepSlider.value <= 50 && impRepSlider.value > 15)
            impRepFillImage.color = Color.yellow;
        else
            impRepFillImage.color = Color.green;

        if (workersRepSlider.value <= MainData.MinWorkersReputation)
            workersRepFillImage.color = Color.red;
        else if (workersRepSlider.value <= 50 && workersRepSlider.value > MainData.MinWorkersReputation)
            workersRepFillImage.color = Color.yellow;
        else
            workersRepFillImage.color = Color.green;

        if (richRepSlider.value <= MainData.MinRichReputation)
            richRepFillImage.color = Color.red;
        else if (richRepSlider.value <= 50 && richRepSlider.value > MainData.MinRichReputation)
            richRepFillImage.color = Color.yellow;
        else
            richRepFillImage.color = Color.green;

        if (poorRepSlider.value <= MainData.MinPoorReputation)
            poorRepFillImage.color = Color.red;
        else if (poorRepSlider.value <= 50 && poorRepSlider.value > MainData.MinPoorReputation)
            poorRepFillImage.color = Color.yellow;
        else
            poorRepFillImage.color = Color.green;

        policeMax.text = MainData.MaxPolicemen.ToString();
        policeCur.text = MainData.Policemen.ToString();
        doctorMax.text = MainData.MaxDoctors.ToString();
        doctorCur.text = MainData.Doctors.ToString();
        volontMax.text = MainData.MaxVolunteers.ToString();
        volontCur.text = MainData.Volunteers.ToString();

        UnburiedPeople.text = MainData.UnburiedPeople.ToString();
    }

    public Text MinuteGameText;
    private void TextInfoAll()
    {
        TimeGameText.text = $"{(int)TimeGame:00}";
        MinuteGameText.text = $"{(int)Minutes:00}";
        DayText.text = "День: " + MainData.Day.ToString();
        MoneyText.text = MainData.Money.ToString();
        NameDistrictText.text = NameDistrict;
        AllPeopleDistrictText.text = "Всего людей: " + NumberPeopleDistrict;
        NumbersOfViolentText.text = "Заражённых: " + NumbersOfViolent;
        NumbersOfDeathText.text = "Умерших: " + NumbersOfDeath;
    }

    bool isDayDone = false; //чтобы 300 раз не подписывался на НекстДей
    bool IsGoNextDay = false;

    public void GoNextDay()
    {
        IsGoNextDay = true;
    }

    public void OpenMenu()
    {
        PauseGame();
        GUIAKTIV = true;
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        GUIAKTIV = false;
        UnpauseGame();
        menuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        state = GameState.Pause;
    }

    public void UnpauseGame()
    {
        state = GameState.City;
    }

    void Update()
    {
        if (state == GameState.Pause)
            return;

        TimeGame += Time.deltaTime / MainData.DayTimeScale;
        Minutes = TimeGame % 1;
        if (Minutes >= 0 && Minutes < 0.25)
            Minutes = 0f;
        else if (Minutes >= 0.25 && Minutes < 0.5)
            Minutes = 15f;
        else if (Minutes >= 0.5 && Minutes < 0.75)
            Minutes = 30f;
        else if (Minutes >= 0.75 && Minutes < 1)
            Minutes = 45f;

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
        if (MainData.Vacina >= 100)
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
            switch(d.type)
            {
                case DistrictType.Poor:
                    MainData.PoorReputation -= MainData.RepPerSearch;
                    break;
                case DistrictType.Workers:
                    MainData.WorkersReputation -= MainData.RepPerSearch;
                    break;
                case DistrictType.Rich:
                    MainData.RichReputation -= MainData.RepPerSearch;
                    break;
            }
            var HousesDistrict = d.Houses;
            for (int i = 0; i < HousesDistrict.Length; i++)
            {
                var HousesDistrictNow = HousesDistrict[i].GetComponent<StateOBJ>();
                if (HousesDistrictNow.CountHideInfected > 0)
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

        for (int i = 0; i < AllDistricts[1].Houses.Length; i++)
        {
            var house = AllDistricts[1].Houses[i].GetComponent<StateOBJ>();
            if (!house.IsInfected)
            {
                house.CountHideInfected += 3;
                house.IsInfected = true;
                house.CountDeath += 1;
                house.CountInfected += 3;
                MainData.AllDeath += 1;
                MainData.AllInfected += 3;
                MainData.NewDeadPeople += 1;
                MainData.NewInfectedPeople += 3;
                house.IsHide = false;
                house.IsInfected = true;
                house.UpdateIcons();
                AllDistricts[1].DistricdState();
                break;
            }
        }

    }

    public bool InfectTwoHousesInSunland = false;

    public void InfectTwoHousesInSunlandNow()
    {
        InfectTwoHousesInSunland = false;

        int c = 2;

        for(int i = 0; i < AllDistricts[0].Houses.Length; i++)
        {
            var house = AllDistricts[0].Houses[i].GetComponent<StateOBJ>();
            if (!house.IsInfected)
            {
                house.CountHideInfected += 3;
                house.IsInfected = true;
            }
            c--;
            if (c == 0)
                break;
        }
    }

    public bool TimeStartDayPlus4 = false;
    public bool InfectRandomHouse = false;
    public bool Recovery5Houses = false;

    public void InfectRandomHouseNow()
    {
        InfectRandomHouse = false;

        List<StateOBJ> houses = new List<StateOBJ>();

        foreach(var d in RichDistricts)
            foreach(var h in d.Houses)
            {
                var house = h.GetComponent<StateOBJ>();
                if (!house.IsInfected)
                    houses.Add(house);
            }

        if(houses.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, houses.Count);
            houses[i].CountHideInfected += 3;
            houses[i].IsInfected = true;
        }
    }

    public bool CheckAllRichRiot()
    {
        foreach (var d in RichDistricts)
            if (!d.IsRiot)
                return false;

        return true;
    }

    public bool Check50percentInfectedHouses()
    {
        int allCount = 0;
        int infectedCount = 0;

        foreach(var d in RichDistricts)
            foreach(var h in d.Houses)
            {
                allCount++;
                if (h.GetComponent<StateOBJ>().IsInfected)
                    infectedCount++;
            }

        if (allCount * 0.5 < infectedCount)
            return true;
        else
            return false;
    }
    public bool Check70percentInfectedHouses()
    {
        int allCount = 0;
        int infectedCount = 0;

        foreach (var d in RichDistricts)
            foreach (var h in d.Houses)
            {
                allCount++;
                if (h.GetComponent<StateOBJ>().IsInfected)
                    infectedCount++;
            }

        if (allCount * 0.7 < infectedCount)
            return true;
        else
            return false;
    }

    private void Recovery5HousesNow()
    {
        int count = 5;
        foreach(var h in AllDistricts[0].Houses)
        {
            var house = h.GetComponent<StateOBJ>();
            if(house.IsInfected)
            {
                house.CountInfected = 0;
                house.IsInfected = false;
                house.IsHide = false;
                house.UpdateIcons();

                count--;
            }

            if (count == 0)
                return;
        }    
    }

    public void ReloadScene()
    {
        MainData.Reload();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
