using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip splash;
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip game;

    AudioSource audioSource;

    #region Singleton

    public static MusicPlayer Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        UpdateMusicClip();
    }

    #endregion

    // Use this for initialization
    void Start()
    {
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
        
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        UpdateMusicClip();
    }

    private void UpdateMusicClip()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            audioSource.clip = mainMenu;
            audioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Splash")
        {
            audioSource.clip = splash;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = game;
            audioSource.Play();
        }
    }
    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefsController.SetMasterVolume(volume);
    }
}