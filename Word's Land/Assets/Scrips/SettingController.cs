using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    private RoundManager _roundManager;
    private BlocksMaker _blocksMaker;
    [SerializeField] private GameObject mainCanvasPos;
     [SerializeField] private Slider volumeSlider;

     private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        _roundManager = FindObjectOfType<RoundManager>();
        _blocksMaker = FindObjectOfType<BlocksMaker>();
        initPos = transform.position;
        
        volumeSlider.SetValueWithoutNotify(PlayerPrefsController.GetMasterVolume());
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void SaveAndexit()
    {
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }

    private void UpdateVolume(float volume)
    {
        MusicPlayer.Instance.SetVolume(volume);
    }
    
    public void SetUpSetting()
    {
        if (_blocksMaker)
        _blocksMaker.currentState = BlocksMaker.BoardState.wait; 

        transform.DOMove( mainCanvasPos.transform.position, 0.5f);
        _roundManager.Pause();
    }

    public void BackToGame()
    {
        transform.DOMove(initPos, 0.5f).OnComplete(() => 
        {
            _blocksMaker.currentState = BlocksMaker.BoardState.move; 
            _roundManager.UnPause();
        });
    }
}