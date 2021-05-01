using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocksRoad : ActionButton
{
    private void LockRoad()
    {
        if(MainData.Volunteers > 0)
        {
            SoundController.Instance.PlayBlockRoad();
            IsActive = true;
            MainData.Volunteers--;
            ActiveGO.SetActive(true);
            UnactiveGO.SetActive(false);
            MainScript.Instance.UpdateUI();
        }
    }

    private void UnlockRoad()
    {
        IsActive = false;
        MainData.Volunteers++;
        ActiveGO.SetActive(false);
        UnactiveGO.SetActive(true);
        MainScript.Instance.UpdateUI();
    }

    public override void OnClick()
    {
        base.OnClick();

        if (IsActive)
            UnlockRoad();
        else
        {
            LockRoad();
        }
    }
}
