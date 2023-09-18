using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Update = UnityEngine.PlayerLoop.Update;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip WinSound;
    [SerializeField] private AudioClip LoseSound;
    [SerializeField] private AudioClip splash;
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip game;

    private RoundManager _roundManager;
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
        _roundManager = FindObjectOfType<RoundManager>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();

        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void Update()
    {
        /*
                 if (_roundManager.PlayWinSound /*&& SceneManager.GetActiveScene().name == "SampleScene"#1#)
                {
                    audioSource.clip = WinSound;
                    audioSource.Play();
                }
                else if (_roundManager.PlayLoseSound /*&& SceneManager.GetActiveScene().name == "SampleScene"#1#)
                {
                    audioSource.clip = LoseSound;
                    audioSource.Play();
                }*/
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
        else if
            (SceneManager.GetActiveScene().name ==
             "SampleScene" /*&& !_roundManager.PlayLoseSound && !_roundManager.PlayWinSound*/)
        {
            audioSource.clip = game;
            audioSource.Play();
        }
    }

    public void PlayLoseSound()
    {
        audioSource.clip = LoseSound;
        audioSource.Play();
    } public void PlayWinSound()
    {
        audioSource.clip = WinSound;
        audioSource.Play();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefsController.SetMasterVolume(volume);
    }
}