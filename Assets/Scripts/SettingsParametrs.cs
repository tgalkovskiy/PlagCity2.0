using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    Russian,
    English
}


[System.Serializable]
public class SettingsParametrs
{
    public Language language;
    public float soundValue;
    public float musicValue;
}
