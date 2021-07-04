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

    public AudioClip EndGame;

    public AudioSource SoundSource;
    public AudioSource MusicSource;
    public AudioSource LowerSoundSource;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        UpdateValues();
    }


    public void OnSoundValueChanged()
    {
        SoundSource.volume = Settings.parametrs.soundValue;
    }
    public void OnMusicValueChanged()
    {
        MusicSource.volume = Settings.parametrs.musicValue;
        LowerSoundSource.volume = Settings.parametrs.musicValue / 4;
    }

    public void UpdateValues()
    {
        SoundSource.volume = Settings.parametrs.soundValue;
        MusicSource.volume = Settings.parametrs.musicValue;
        LowerSoundSource.volume = Settings.parametrs.musicValue / 4;

        Debug.Log($"Values updated. Sound: {Settings.parametrs.soundValue} Music: {Settings.parametrs.musicValue}");
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

    public void PlayEndGame()
    {
        MusicSource.clip = EndGame;
        MusicSource.Play();
    }
}
