using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loupe : ActionButton
{
    private void Start()
    {
        IsActive = true;
        IsActivated = false;
        UnactiveGO.SetActive(false);
        ActiveGO.SetActive(false);
    }

    public override void OnClick()
    {
        base.OnClick();
        MainScript.Instance.TransformCamera();
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

        //if (district.IsRiot == false)
        //{
        //    return;
        //}


        if (district.ActiveDistrict)
            transform.position = district.LoupeButtonPointDistrict.position;
        else
            transform.position = district.LoupeButtonPointMap.position;

        Activate();
    }
}
