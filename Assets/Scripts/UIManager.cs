using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static List<TranslatableText> TextsToTranslate;

    private static string[] textsUI;

    [SerializeField] private Dropdown languageDropdown;
    [SerializeField] private GameObject infoLanguagePanel;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private Settings settings;

    private void Awake()
    {
        settings = GetComponent<Settings>();
        TextsToTranslate = new List<TranslatableText>();
    }

    private void Start()
    {
        UpdateText();
        UpdateUI();
    }

    public static void OnDataLoad()
    {
        Debug.LogWarning("UIMANager OnDataLoaded");
        textsUI = TextLoader.data.UIdata;

        LettersManager.Instance.itogiNoEvents = textsUI[48 - 1]; //Как бы UI но не UI, поэтому так (когда не произошло событий в книжку что написать)

    }

    public static void AddTextToTranslate(TranslatableText text)
    {
        if (TextsToTranslate == null)
            TextsToTranslate = new List<TranslatableText>();

        TextsToTranslate.Add(text);
    }

    public static void UpdateText()
    {
        if (TextsToTranslate == null)
            return;

        if (textsUI == null)
            return;

        foreach (var t in TextsToTranslate)
        {
            if(t.textUI != null)
            {
                t.textUI.text = textsUI[t.TextId - 1]; //в экселе с 1 начинается, просто для удобства
            }
            else
            {
                t.textTooltip.text = textsUI[t.TextId - 1];
            }
        }
    }

    public void OnSoundValueChanged()
    {
        Settings.parametrs.soundValue = soundSlider.value;
        SoundController.Instance.OnSoundValueChanged();
    }

    public void OnMusicValueChanged()
    {
        Settings.parametrs.musicValue = musicSlider.value;
        SoundController.Instance.OnMusicValueChanged();
    }

    public void OnLanguageChanged()
    {
        infoLanguagePanel.SetActive(true);
    }

    public void OnCancelLanguageChange()
    {
        languageDropdown.value = (int)Settings.parametrs.language;
        infoLanguagePanel.SetActive(false);
    }

    public void OnApplyLanguageChange()
    {
        Settings.parametrs.language = (Language)(languageDropdown.value);
        settings.SaveSettings(Settings.parametrs);
        Application.Quit();
    }

    public void UpdateUI()
    {
        OnCancelLanguageChange(); // костыль, потому что ивент смены валуе в дропдаун срабатывает и через код, поэтому обноляю так
        soundSlider.value = Settings.parametrs.soundValue;
        musicSlider.value = Settings.parametrs.musicValue;
    }
}
