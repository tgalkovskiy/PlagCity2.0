using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    [HideInInspector] public static Action LoadElement = delegate { };
    private string NameMetodEvent;
    public void LoadGameElement()
    {
        LoadElement?.Invoke();
        
    }
    public void RepairMetod()
    {
        LoadElement -= LoadElement;
       
    }
    
}
