using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    [HideInInspector] public static Action LoadElement = delegate { };
    [HideInInspector] public static Action<bool, string, string, string> EventTrigger;
    [HideInInspector] public static Action<bool, string> EventTriggerNoAnswers;

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
