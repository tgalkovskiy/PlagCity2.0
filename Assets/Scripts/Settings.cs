using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static SettingsParametrs parametrs;

    private void Awake()
    {
        parametrs = LoadSettings();
    }

    public void SaveSettings(SettingsParametrs parametrs)
    {
        string json = JsonUtility.ToJson(parametrs);

        Debug.Log($"Saved settings : {json}");

        File.WriteAllText("Settings.json", json);
    }

    public SettingsParametrs LoadSettings()
    {
        if (!File.Exists("Settings.json") || File.ReadAllText("Settings.json") == "")
        {
            Debug.Log("new settings def");
            SettingsParametrs set = new SettingsParametrs();
            set.soundValue = 0.25f;
            set.musicValue = 0.25f;
            set.language = Language.Russian;

            SaveSettings(set);
            return set;
        }

        string json = File.ReadAllText("Settings.json");

        Debug.Log($"Loaded settings : {json}");
        return JsonUtility.FromJson<SettingsParametrs>(json);
    }
}
