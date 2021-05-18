using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graveyard : ActionButton
{
    public int CountVolunteers;
    [SerializeField] private Image[] VolunteersImages;
    [SerializeField] private Sprite ActiveVolunteerSprite;
    [SerializeField] private Sprite UnactiveVolunteerSprite;
    [SerializeField] private Text CountToBuryPeopleText;

    private void Start()
    {
        CountVolunteers = 0;
        CountToBury = 0;
        UpdateInfo();
    }

    public void SetCountOfVolunteers(int i)
    {
        MainData.Volunteers += CountVolunteers;
        if (MainData.Volunteers >= i)
        {
            CountVolunteers = i;
            MainData.Volunteers -= i;
        }
        else
        {
            CountVolunteers = MainData.Volunteers;
            MainData.Volunteers -= MainData.Volunteers;
        }
        Debug.Log($"Clicked to Graveyards Count = {i}\nFree volunteers = {MainData.Volunteers}");
        UpdateInfo();
    }


    private int CountToBury;

    private void UpdateInfo()
    {
        foreach (var a in VolunteersImages)
            a.sprite = UnactiveVolunteerSprite;

        for(int i = 0; i < CountVolunteers; i++)
        {
            VolunteersImages[i].sprite = ActiveVolunteerSprite;
        }

        CountToBury = 10 * CountVolunteers;
        if (MainData.UnburiedPeople < CountToBury)
            CountToBury = MainData.UnburiedPeople;

        CountToBuryPeopleText.text = (MainData.UnburiedPeople - CountToBury).ToString();

        MainScript.Instance.UpdateUI();
    }

    public override void OnClick()
    {
        base.OnClick();


        if (IsActive)
        {
            IsActive = false;
            MainData.Volunteers += CountVolunteers;
            CountVolunteers = 0;
            ActiveGO.SetActive(false);
            UnactiveGO.SetActive(true);
            foreach (var v in VolunteersImages)
                v.gameObject.SetActive(false);

            UpdateInfo();
        }
        else
        {
            SoundController.Instance.PlayDigGrave();
            IsActive = true;
            ActiveGO.SetActive(true);
            UnactiveGO.SetActive(false);
            int count = Mathf.CeilToInt((float)MainData.UnburiedPeople / 10);

            Debug.Log($"Volunteers count to graveyard = {count} ({(float)MainData.UnburiedPeople / 10}) ");

            UpdateInfo();


            //Исправить оверкап волонтеров
            for (int i = 0; i < count && i < 10; i++)
            {
                if (VolunteersImages.Length == i)
                    break;
                VolunteersImages[i].gameObject.SetActive(true);
            }
        }
    }

    public override void OnNewDay()
    {
        MainData.UnburiedPeople -= CountToBury;
        CountToBury = 0;
        if (IsActive)
        {
            IsActive = false;
            CountVolunteers = 0;
            ActiveGO.SetActive(false);
            UnactiveGO.SetActive(true);
        }
        UpdateInfo();
    }

    public override void OnDistrictChange()
    {
        Activate();
    }
}