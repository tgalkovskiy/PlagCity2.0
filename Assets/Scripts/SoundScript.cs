using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public AudioClip ClickSound;
    public AudioSource ClickAudioSource;

    private void Awake()
    {
        ClickAudioSource = GetComponent<AudioSource>(); 
    }

}
