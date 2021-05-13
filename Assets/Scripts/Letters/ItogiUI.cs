using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItogiUI : MonoBehaviour
{
    public static ItogiUI Instance;

    [SerializeField] private Text ItogiText;

    [SerializeField] private Text DayText;

    [SerializeField] private Text NewViolText;
    [SerializeField] private Text NewDeathsText;

    [SerializeField] private Text AllViolText;
    [SerializeField] private Text AllDeathsText;

    [SerializeField] private Text VacinaText;

    [SerializeField] private Text CoefText;
    [SerializeField] private Text CoefDeltaText;

    [SerializeField] private Text ImperatorRepText;
    [SerializeField] private Text ImperatorRepDeltaText;

    [SerializeField] private Text RichRepText;
    [SerializeField] private Text RichRepDeltaText;

    [SerializeField] private Text WorkersRepText;
    [SerializeField] private Text WorkersRepDeltaText;

    [SerializeField] private Text SavedHousesText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowItogi(string itogiText)
    {
        ItogiText.text = itogiText;
        DayText.text = MainData.Day.ToString();
        VacinaText.text = MainData.Vacina.ToString();
        NewViolText.text = MainData.NewInfectedPeople.ToString();
        NewDeathsText.text = MainData.NewDeadPeople.ToString();
        AllViolText.text = MainData.AllInfected.ToString();
        AllDeathsText.text = MainData.AllDeath.ToString();

        SavedHousesText.text = MainData.savedHouses.ToString();

        int delta;

        #region ImperatorRep
        ImperatorRepText.text = MainData.ImperatorReputation.ToString();

        delta = MainData.ImperatorReputation - MainData.preImperatorRep;
        if (delta > 0)
        {
            ImperatorRepDeltaText.text = $"|+{delta}";
            ImperatorRepDeltaText.color = new Color(0, 0.5f, 0);
        }
        else if (delta < 0)
        {
            ImperatorRepDeltaText.text = $"|{delta}";
            ImperatorRepDeltaText.color = Color.red;
        }
        else
        {
            ImperatorRepDeltaText.text = $"|{delta}";
            ImperatorRepDeltaText.color = Color.black;
        }
        #endregion       
        #region RichRep
        RichRepText.text = MainData.RichReputation.ToString();

        delta = MainData.RichReputation - MainData.preRichRep;
        if (delta > 0)
        {
            RichRepDeltaText.text = $"|+{delta}";
            RichRepDeltaText.color = new Color(0, 0.5f, 0);
        }
        else if (delta < 0)
        {
            RichRepDeltaText.text = $"|{delta}";
            RichRepDeltaText.color = Color.red;
        }
        else
        {
            RichRepDeltaText.text = $"|{delta}";
            RichRepDeltaText.color = Color.black;
        }
        #endregion
        #region WorkersRep
        WorkersRepText.text = MainData.WorkersReputation.ToString();

        delta = MainData.WorkersReputation - MainData.preWorkersRep;
        if (delta > 0)
        {
            WorkersRepDeltaText.text = $"|+{delta}";
            WorkersRepDeltaText.color = new Color(0, 0.5f, 0);
        }
        else if (delta < 0)
        {
            WorkersRepDeltaText.text = $"|{delta}";
            WorkersRepDeltaText.color = Color.red;
        }
        else
        {
            WorkersRepDeltaText.text = $"|{delta}";
            WorkersRepDeltaText.color = Color.black;
        }
        #endregion
    }

    public void ShowCoef()
    {
        #region InfectCoef
        int num = (MainData.NewHouseCoef + MainData.AnotherDistrictCoef) / 2;
        CoefText.text = num.ToString();

        int delta = num - MainData.preAverageCoef;
        if (delta > 0)
        {
            CoefDeltaText.text = $"|+{delta}";
            CoefDeltaText.color = Color.red;
        }
        else if (delta < 0)
        {
            CoefDeltaText.text = $"|{delta}";
            CoefDeltaText.color = new Color(0, 0.5f, 0);
        }
        else
        {
            CoefDeltaText.text = $"|{delta}";
            CoefDeltaText.color = Color.black;
        }
        #endregion
    }
}
