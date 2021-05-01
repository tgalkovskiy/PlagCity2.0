using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotDown : ActionButton
{
    public override void OnClick()
    {
        base.OnClick();

        if (IsActive)
        {
            IsActive = false;
            ActiveGO.SetActive(false);
            UnactiveGO.SetActive(true);
            MainScript.Instance.curDemoViol.IsOnSuppressRiot = false;
            MainData.Volunteers++;
        }
        else
        {
            if (MainData.Volunteers >= 1)
            {
                IsActive = true;
                ActiveGO.SetActive(true);
                UnactiveGO.SetActive(false);
                MainScript.Instance.curDemoViol.IsOnSuppressRiot = true;
                MainData.Volunteers--;
                SoundController.Instance.PlayRiotDown();
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

        if (district == null)
            return;

        if (district.IsRiot == false)
        {
            return;
        }


        if(district.ActiveDistrict)
            transform.position = district.RiotButtonPointDistrict.position;
        else
            transform.position = district.RiotButtonPointMap.position;

        if (district.IsRiot == true && district.IsOnSuppressRiot == true)
        {
            IsActive = true;
            Activate();
        }
        else if(district.IsRiot == true && district.IsOnSuppressRiot == false)
        {
            IsActive = false;
            Activate();
        }
    }
}
