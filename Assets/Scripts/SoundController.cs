using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.IO;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public AudioClip ClickInGame;
    public AudioClip CloseLetter;
    public AudioClip OpenLetter;
    public AudioClip ThrowawayLetter;
    public AudioClip AnswerLetter;
    public AudioClip NewDay;
    public AudioClip DigGrave;
    public AudioClip HouseLocked;
    public AudioClip BlockRoad;
    public AudioClip Riot;
    public AudioClip LittleSick;
    public AudioClip BigSick;
    public AudioClip Quarantine;
    public AudioClip Church;
    public AudioClip Lab;
    public AudioClip RiotDown;
    public AudioClip Search;
    public AudioClip Bread;

    public AudioSource SoundSource;
    public AudioSource MusicSource;
    public AudioSource LowerSoundSource;

    public Slider soundSlider;
    public Slider musicSlider;

    private SoundSettings soundSettings;




    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        soundSettings = LoadSettings();
        UpdateValues();
    }


    public void OnSoundValueChanged()
    {
        soundSettings.soundValue = soundSlider.value;
        SoundSource.volume = soundSettings.soundValue;
    }
    public void OnMusicValueChanged()
    {
        soundSettings.musicValue = musicSlider.value;
        MusicSource.volume = soundSettings.musicValue;
        LowerSoundSource.volume = soundSettings.musicValue / 4;
    }

    public void UpdateValues()
    {
        SoundSource.volume = soundSettings.soundValue;
        MusicSource.volume = soundSettings.musicValue;
        LowerSoundSource.volume = soundSettings.musicValue / 4;

        soundSlider.value = soundSettings.soundValue;
        musicSlider.value = soundSettings.musicValue;

        Debug.Log($"Values updated. Sound: {soundSettings.soundValue} Music: {soundSettings.musicValue}");
    }

    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(soundSettings);

        Debug.Log($"Saved settings : {json}");

        File.WriteAllText("Settings.json", json);
    }

    public SoundSettings LoadSettings() 
    {
        if(!File.Exists("Settings.json") || File.ReadAllText("Settings.json") == "")
        {
            Debug.Log("new settings def");
            SoundSettings set = new SoundSettings();
            set.soundValue = 0.5f;
            set.musicValue = 0.5f;

            SaveSettings();
            return set;
        }

        string json = File.ReadAllText("Settings.json");

        Debug.Log($"Loaded settings : {json}");
        return JsonUtility.FromJson<SoundSettings>(json);
    }

    public void PlayClickInGame()
    {
        SoundSource.PlayOneShot(ClickInGame);
    }

    public void PlayCloseLetter()
    {
        SoundSource.PlayOneShot(CloseLetter);
    }

    public void PlayOpenLetter()
    {
        SoundSource.PlayOneShot(OpenLetter);
    }

    public void PlayThrowawayLetter()
    {
        SoundSource.PlayOneShot(ThrowawayLetter);
    }

    public void PlayAnswerLetter()
    {
        SoundSource.PlayOneShot(AnswerLetter);
    }

    public void PlayNewDay()
    {
        SoundSource.PlayOneShot(NewDay);
    }

    public void PlayDigGrave()
    {
        SoundSource.PlayOneShot(DigGrave);
    }

    public void PlayHouseLocked()
    {
        SoundSource.PlayOneShot(HouseLocked);
    }

    public void PlayBlockRoad()
    {
        SoundSource.PlayOneShot(BlockRoad);
    }
    
    public void PlayRiot()
    {
        SoundSource.PlayOneShot(Riot);
    }

    public void PlayLittleSick()
    {
        SoundSource.PlayOneShot(LittleSick);
    }

    public void PlayBigSick()
    {
        SoundSource.PlayOneShot(BigSick);
    }

    public void PlayQuarantine()
    {
        SoundSource.PlayOneShot(Quarantine);
    }

    public void PlayChurch()
    {
        SoundSource.PlayOneShot(Church);
    }

    public void PlayLab()
    {
        SoundSource.PlayOneShot(Lab);
    }

    public void PlayRiotDown()
    {
        SoundSource.PlayOneShot(RiotDown);
    }

    public void PlaySearch()
    {
        SoundSource.PlayOneShot(Search);
    }

    public void PlayBread()
    {
        SoundSource.PlayOneShot(Bread);
    }

    public void PlayRiotDistrict()
    {
        LowerSoundSource.clip = Riot;
        LowerSoundSource.Play();
    }

    public void StopPlayingLowerSounds()
    {
        LowerSoundSource.Stop();
    }
}
