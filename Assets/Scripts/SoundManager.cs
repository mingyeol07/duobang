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

    [SerializeField] private Sound[] bgmSounds;
    [SerializeField] private Sound[] sfxSounds;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

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

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetBGMVolume(float value)
    {
        if (value == -40)
        {
            mixer.SetFloat(mixerBGM, -80);
        }
        else
        {
            mixer.SetFloat(mixerBGM, value);
        }
    }

    void SetSFXVolume(float value)
    {
        if (value == -40)
        {
            mixer.SetFloat(mixerSFX, -80);
        }
        else
        {
            mixer.SetFloat(mixerSFX, value);
        }
    }

    public void PlayBGM(string name)
    {
        Sound sound = Array.Find(bgmSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            bgmSource.clip = sound.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
