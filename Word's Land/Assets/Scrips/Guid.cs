using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class Guid : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetUpGuid()
    {
        transform.DOMove(MainPanel.transform.position, .5f);
    }

    public void BackToMenu()
    {
        transform.DOMove(initPos, .5f);
    }
    
    
}