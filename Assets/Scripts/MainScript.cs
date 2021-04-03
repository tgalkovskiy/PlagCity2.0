using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
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
    private int DayGame, Money, AllDeathPeople, AllViolPeople, Infected, Buried, AllPeople;
    [HideInInspector] public string NameDistrict, NumberPeopleDistrict, NumbersOfViolent, NumbersOfDeath;


    private void Awake()
    {
        //комопонент камеры
        MainCamera = GetComponent<Camera>();
        //City = GetComponent<StateOBJ>();
        DayGame = 1;
        TimeGame = 0;
        DeathStat.Money = 5000;
        Vacina.text = "50";
        //AllDeathPeople = 0;
        //AllViolPeople = 0;
        Infected = 1;
        Buried = 0;
        //AllPeople = 2100;
        //NumberDistrict = 0;
        //отключение всех зараженых маркеров сразу
        for(int i=0; i< DistrictRed.Length; i++)
        {
            Districts.Add(District[i]);
        }
        GuiPanel.SetActive(false);
        
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
    private void CameraTransform()
    {
        if (NameDistrict == "sunland")
        {
            MainCamera.transform.position = new Vector3(-78, 13, -10);
            DemoViol[0].ActiveDistrict = true;
        }
        else if (NameDistrict == "grandstream")
        {
            MainCamera.transform.position = new Vector3(-78, -19, -10);
            DemoViol[1].ActiveDistrict = true;
        }
        else if (NameDistrict == "west river")
        {
            MainCamera.transform.position = new Vector3(-78, -52, -10);
            DemoViol[2].ActiveDistrict = true;
        }
    }

    private void CameraTransforDefold()
    {
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
        }
    }
   
    /// <summary>
    /// движение камеры(настроил+Делагаты)
    /// </summary>
    private void ControlCamera()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //считывание вращения колесика
            MouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            //приближение и отдаление
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + MouseScrollWheel * 2, 5, 14);
            //при максимальном отдалении уход на стартовую позицию камеры
            if(Camera.main.orthographicSize == 14 && MainMap == true)
            {
                transform.position = new Vector3(0,0, transform.position.z);
            }
        }
        //движение камеры
        if (Input.GetMouseButton(1) && Camera.main.orthographicSize<14 && MainMap==true)
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
        Money += AllPeople - AllDeathPeople;
    }
    /// <summary>
    /// Кнопка нового дня
    /// </summary>
    public void NextDay()
    {
        DayGame += 1;
        TimeGame = 0;
        DeathStat.Vacina += 3;
        City.CountDeath = DeathStat.AllDeath;
        Vacina.text = DeathStat.Vacina.ToString();
        MoneyCalculate();
        DemoViol[0].NextDayStateObj();
        CameraTransforDefold();
    }
    private void TextInfoAll()
    {
        TimeGameText.text =((int)TimeGame).ToString();
        DayText.text = "Day: " + DayGame.ToString();
        MoneyText.text = DeathStat.Money.ToString();
        NameDistrictText.text = NameDistrict;
        AllPeopleDistrictText.text = "People: " + NumberPeopleDistrict;
        NumbersOfViolentText.text = "Vialent: " + NumbersOfViolent;
        NumbersOfDeathText.text = "Death: " + NumbersOfDeath;
    }
    void Update()
    {
        TimeGame += Time.deltaTime/3;
        TextInfoAll();
        if (Input.GetKeyDown(KeyCode.Space) || TimeGame >= 24)
        {
            //подписка на события загрузки камеры
            Event.LoadElement += NextDay;
            //анимация с двумя ивентами(то что подписали в ивент, и отписка от ивента)
            Animator.SetTrigger("NewWindow");
        }
        Ray();
        ControlCamera();
        if(DeathStat.Vacina >= 100)
        {
            Debug.Log("Победа");
        }
    }
}
