using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDistrict : ActionButton
{
    public override void OnClick()
    {
        base.OnClick();

        if (IsActive)
        {
            IsActive = false;
            ActiveGO.SetActive(false);
            UnactiveGO.SetActive(true);
            MainScript.Instance.curDemoViol.UnsearchDistrict();
            MainData.Volunteers++;
        }
        else
        {
            if (MainData.Volunteers >= 1)
            {
                IsActive = true;
                ActiveGO.SetActive(true);
                UnactiveGO.SetActive(false);
                MainScript.Instance.curDemoViol.SearchDistrict();
                MainData.Volunteers--;
                SoundController.Instance.PlaySearch();
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
            transform.position = district.SearchButtonPointDistrict.position;
        else
            transform.position = district.SearchButtonPointMap.position;

        if (district.IsOnSearch == true)
        {
            IsActive = true;
        }
        else
        {
            IsActive = false;
        }

        Activate();

        base.OnDistrictChange();
    }
}
