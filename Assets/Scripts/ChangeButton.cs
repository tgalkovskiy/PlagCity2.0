using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
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
            if (NowGameObj.GetComponent<StateOBJ>().TypeStateDis == TypeState.Houses && DeathStat.Volunteers > 0 && !NowGameObj.GetComponent<StateOBJ>().Lock)
            {
                DeathStat.Volunteers--;
                NowGameObj.GetComponent<StateOBJ>().Lock = true;
                NowGameObj.GetComponent<StateOBJ>().IconViol();
                MainScript.UpdateUI();
            }

            //Пока криво работает

            //if (NowGameObj.tag == "Global" && NowGameObj.GetComponent<DemoViol>())
            //{
            //}
        }
    }

}
