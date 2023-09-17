using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private RoundManager _roundManager;
    [SerializeField] private int timeToWait;
    private int currentSceneIndext;

    // Start is called before the first frame update
    void Start()
    {
        _roundManager = FindObjectOfType<RoundManager>();
        currentSceneIndext = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndext == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndext);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndext + 1);
    }


    public void LoadLoseScreen()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if (_roundManager.isPaused)
        {
            _roundManager.UnPause();
        }
        else
        {
            _roundManager.Pause();
        }
    }
}