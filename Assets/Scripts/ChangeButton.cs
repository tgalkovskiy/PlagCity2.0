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
            if (NowGameObj.GetComponent<StateOBJ>())
            {
                NowGameObj.GetComponent<StateOBJ>().Lock = true;
                
            }
            if(NowGameObj.tag == "Global" && NowGameObj.GetComponent<DemoViol>())
            {
                var HousesDistrict = NowGameObj.GetComponent<DemoViol>().Houses;
                for(int i=0; i<HousesDistrict.Length; i++)
                {
                    var HousesDistrictNow = HousesDistrict[i].GetComponent<StateOBJ>();
                    if (HousesDistrictNow.CountViol > 0)
                    {
                        HousesDistrictNow.ViolLine.SetActive(true);
                    }
                }
            }
            
        }
    }
}
