using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public Sound[] bgmSounds;
    public Sound[] sfxSound;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioMixer mixer;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const string mixserMaster = "Master";
    private const string mixerBGM = "BGM";
    private const string mixerSFX = "SFX";

    private void Awake()
    {
        Instance = this;

        bgmSlider.minValue = -40f;
        bgmSlider.maxValue = 0;

        sfxSlider.minValue = -40f;
        sfxSlider.maxValue = 0;

        bgmSlider.value = 0;
        sfxSlider.value = 0;

        //bgmSlider.onValueChanged.AddListener(SetMusicVolume);
        //sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }
}
