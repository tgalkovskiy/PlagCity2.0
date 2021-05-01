using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : ActionButton
{
    public override void OnClick()
    {
        base.OnClick();

        if (IsActive)
        {
            IsActive = false;
            ActiveGO.SetActive(false);
            UnactiveGO.SetActive(true);
            MainScript.Instance.curDemoViol.IsGivenBread = false;
            MainData.Money += MainData.BreadPrice;
            MainData.Volunteers++;
        }
        else
        {
            if (MainData.Volunteers >= 1 && MainData.Money >= MainData.BreadPrice)
            {
                IsActive = true;
                ActiveGO.SetActive(true);
                UnactiveGO.SetActive(false);
                MainScript.Instance.curDemoViol.IsGivenBread = true;
                MainData.Money -= MainData.BreadPrice;
                MainData.Volunteers--;
                SoundController.Instance.PlayBread();
            }
        }
        MainScript.Instance.UpdateUI();
    }

    public override void OnNewDay()
    {
        Deactivate();
        MainScript.Instance.UpdateUI();
    }

    public override void OnDistrictChange()
    {
        DemoViol district = MainScript.Instance.curDemoViol;

        if (district.ActiveDistrict)
            transform.position = district.BreadButtonPointDistrict.position;
        else
            transform.position = district.BreadButtonPointMap.position;

        if (district.IsGivenBread == true)
        {
            IsActive = true;
        }
        else
        {
            IsActive = false;
        }

        Activate();
    }
}
