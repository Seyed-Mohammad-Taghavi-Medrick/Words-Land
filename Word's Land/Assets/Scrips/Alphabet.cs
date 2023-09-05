using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Alphabet : MonoBehaviour
{
    private WordChecker WordChecker;
    [SerializeField] private string letter;
    [SerializeField, Range(0,100)] private float _chance = 100f;

    public float Chance
    {
        get => _chance;
    }
    
    public Vector2 pos;

    private BlocksMaker blocksMaker;
//*********************

    private bool _selected;

    private bool _clicked;
    private bool _corect;

    private int _index = -1;

    public void SetIndex(int index)
    {
        _index = index;
    }

    public int GetIndex()
    {
        return _index;
    }


    // Start is called before the first frame update
    void Start()
    {
        _selected = false;
        _clicked = false;
        _corect = false;
        WordChecker = FindObjectOfType<WordChecker>();
    }

    private void OnEnable()
    {
        GameEvents.OnEnableSqaureSelection += GameEventsOnEnableSqaureSelection;
        GameEvents.OnDisableSqaureSelection += GameEventsOnDisableSqaureSelection;
        GameEvents.OnSelectSqaure += GameEventsOnSelectSqaure;
        GameEvents.OnCheckSqaure += GameEventsOnOnCheckSqaure;
    }

    private void GameEventsOnOnCheckSqaure(Alphabet obj, string arg1, Vector3 arg2, int arg3)
    {
        GameEvents.SelectSqaureMethod(arg2);
    }


    private void OnDisable()
    {
        GameEvents.OnEnableSqaureSelection -= GameEventsOnEnableSqaureSelection;
        GameEvents.OnDisableSqaureSelection -= GameEventsOnDisableSqaureSelection;
        GameEvents.OnSelectSqaure -= GameEventsOnSelectSqaure;
        GameEvents.OnCheckSqaure -= GameEventsOnOnCheckSqaure;
    }

    private void GameEventsOnSelectSqaure(Vector3 position)
    {
        if (this.transform.position == position)
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }


    private void OnMouseDown()
    {
        GameEventsOnEnableSqaureSelection();
        GameEvents.EnableSqaureSelectionMethod();
        CheckSqaure();
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void OnMouseEnter()
    {
        CheckSqaure();
    }

    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSqaureSelectionMethod();
        WordChecker.CheckWord();
    }

    public void CheckSqaure()
    {
        if (_selected == false && _clicked == true)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            _selected = true;
            GameEvents.CheckSqaureMethod(this, letter, gameObject.transform.position, _index);
        }
    }


    //*************************************************

    private void GameEventsOnEnableSqaureSelection()
    {
        _clicked = true;
        _selected = false;
    }

    private void GameEventsOnDisableSqaureSelection()
    {
        _clicked = false;
        _selected = false;

        if (_corect == true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (_selected == true)
            {
            }
        }
    }


//******************************************************************************************************************
    public void SetUpAlphabet(Vector2 posIndex, BlocksMaker theBlocksMaker)
    {
        this.pos = posIndex;
        blocksMaker = theBlocksMaker;
    }

    public void Destory()
    {
        // sfx
        // rotation
        blocksMaker.AddNewAlphabet(pos);
        Destroy(gameObject);
    }
}