using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanResourсeManager : MonoBehaviour
{
    //[SerializeField] private DemoViol[] DemoViol;
    [SerializeField] private MainScript MainScript;
    //private int NumberHouse;
    private void Start()
    {
        
    }

    public void Volonteer()
    {
        if (MainScript.NowGameObj !=null)
        {
            GameObject NowGameObj = MainScript.NowGameObj;
            if (NowGameObj.GetComponent<StateOBJ>().TypeStateDis == TypeState.Houses && MainData.Volunteers > 0 && !NowGameObj.GetComponent<StateOBJ>().IsLocked)
            {
                SoundController.Instance.PlayHouseLocked();

                MainData.Volunteers--;
                NowGameObj.GetComponent<StateOBJ>().IsLocked = true;
                NowGameObj.GetComponent<StateOBJ>().UpdateIcons();
                MainScript.UpdateUI();
            }
            else if (NowGameObj.GetComponent<StateOBJ>().TypeStateDis == TypeState.Houses && NowGameObj.GetComponent<StateOBJ>().IsLocked)
            {
                MainData.Volunteers++;
                NowGameObj.GetComponent<StateOBJ>().IsLocked = false;
                NowGameObj.GetComponent<StateOBJ>().UpdateIcons();
                MainScript.UpdateUI();
            }



            //Пока криво работает

            //if (NowGameObj.tag == "Global" && NowGameObj.GetComponent<DemoViol>())
            //{
            //}
        }
    }

}
