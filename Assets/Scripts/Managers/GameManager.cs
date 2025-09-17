using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio")]
    
    public AudioSource clickClip;
    [SerializeField] private AudioMixer mixer;
    public const string MusicKey = "MusicVolume";
    public const string SFXKey = "SFXVolume";

    // QUIZA EL AUDIO TENGAMOS QUE TENERLO EN EL AUDIOMANAGER QUE SOLO ESTE EN LA ESCENA INICIAL Y NO EN EL GAME MANAGER
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }


    public void ClickSFX()
    {
        clickClip.Play();

    }

    public void LoadVolume()
    {
        float MusicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        float SFXVolume = PlayerPrefs.GetFloat(SFXKey, 1f);

        mixer.SetFloat(AudioManager.MixerMusic, Mathf.Log10(MusicVolume) * 20);
        mixer.SetFloat(AudioManager.MixerSFX, Mathf.Log10(SFXVolume) * 20);
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Salir()
    {
        Application.Quit();
    }

}