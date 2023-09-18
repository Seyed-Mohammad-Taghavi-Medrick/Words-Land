using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Vector3 = System.Numerics.Vector3;

public class RoundManager : MonoBehaviour
{
    public bool isWin;
    public bool isLose;
    public bool PlayLoseSound;
    public bool PlayWinSound;
    private LevelLoader _levelLoader;
    private BlocksMaker _blocksMaker;
    [SerializeField] private GameObject MAinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;
    public TMP_Text shuffleCountTex;
    public int shuffleCount = 3;
    public float roundTime = 60f;
    private UIManager uiMan;

    private bool endingRound = false;
    private bool endGame;
    private BlocksMaker board;

    public int currentScore;
   

    public bool isPaused;
   

    // Start is called before the first frame update
    void Awake()
    {
        isWin = false;
        isLose = false;
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<BlocksMaker>();
    }

    private void Start()
    {
        PlayWinSound = false;
        PlayLoseSound = false;
        _levelLoader = FindObjectOfType<LevelLoader>();
        _blocksMaker = FindObjectOfType<BlocksMaker>();
    }

    // Update is called once per frame
    void Update()
    {
        shuffleCountTex.text = shuffleCount.ToString();

        if (!isPaused)
        {
            if (roundTime > 0)
            {
                roundTime -= Time.deltaTime;

                if (roundTime <= 0)
                {
                    roundTime = 0;

                    endingRound = true;
                }
            }
        }

        if (endGame)
             return;
      
        if (endingRound && currentScore < 2100)
        {
            endGame = true;
            StartCoroutine(LoseMethod());
        }

        if (currentScore >= 2100 && !endingRound)
        {
            endGame = true;
            StartCoroutine(WinMethod());
        }


       

        uiMan.timeText.text = roundTime.ToString("0.0") + "s";

       

    }

    private IEnumerator LoseMethod()
    {
        MusicPlayer.Instance.PlayLoseSound();
        // PlayLoseSound = true;
        LosePanel.transform.DOMove(MAinPanel.transform.position, .5f);
        _blocksMaker.currentState = BlocksMaker.BoardState.wait;
        yield return new WaitForSeconds(2);
        isLose = true;
    }

    private IEnumerator WinMethod()
    {
        MusicPlayer.Instance.PlayWinSound();
        // PlayWinSound = true;
        WinPanel.transform.DOMove(MAinPanel.transform.position, .5f);
        _blocksMaker.currentState = BlocksMaker.BoardState.wait;
        Pause();
        yield return new WaitForSeconds(2);
        isWin = true;
    }


    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}