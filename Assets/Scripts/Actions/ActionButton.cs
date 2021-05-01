using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour
{
    public bool IsActivated;

    public bool IsActive;
    public GameObject ActiveGO;
    public GameObject UnactiveGO;

    private void Start()
    {
        IsActivated = false;
        IsActive = false;
        UnactiveGO.SetActive(false);
        ActiveGO.SetActive(false);
    }

    public virtual void OnClick()
    {
        Debug.Log(this + " clicked");
    }

    public void Activate()
    {
        IsActivated = true;
        if (IsActive)
        {
            UnactiveGO.SetActive(false);
            ActiveGO.SetActive(true);
        }
        else
        {
            UnactiveGO.SetActive(true);
            ActiveGO.SetActive(false);
        }
    }

    public void Deactivate()
    {
        IsActivated = false;
        UnactiveGO.SetActive(false);
        ActiveGO.SetActive(false);
    }

    public virtual void OnNewDay()
    {
    }

    public virtual void OnDistrictChange()
    {
    }
}
