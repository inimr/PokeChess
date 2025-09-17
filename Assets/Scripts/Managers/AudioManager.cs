using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    public const string MixerMusic = "MusicVolume";
    public const string MixerSFX = "SFXVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(GameManager.MusicKey, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(GameManager.SFXKey, 1f);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(GameManager.MusicKey, musicSlider.value);
        PlayerPrefs.SetFloat(GameManager.SFXKey, sfxSlider.value);
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MixerMusic, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MixerSFX, Mathf.Log10(value) * 20);
    }
}
