using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Alphabet : MonoBehaviour
{
    public bool inGoldenBlock;
    private Color firstColor;
    public int pointOfLetter;
    private WordChecker wordChecker;
    [SerializeField] private string letter;
    [SerializeField, Range(0, 100)] private float _chance = 100f;
    [SerializeField] private float lerpSpeed = 2f;
    public bool isPartOfWord;

    public float Chance
    {
        get => _chance;
    }

    public Vector2Int pos;

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
        firstColor = GetComponent<SpriteRenderer>().color;
        isPartOfWord = false;
        _selected = false;
        _clicked = false;
        _corect = false;
        wordChecker = FindObjectOfType<WordChecker>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y, 0), Time.deltaTime * lerpSpeed);
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
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }


    private void OnMouseDown()
    {
        if (blocksMaker.currentState == BlocksMaker.BoardState.move)
        {
            GameEventsOnEnableSqaureSelection();
            GameEvents.EnableSqaureSelectionMethod();
            CheckSqaure();
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnMouseEnter()
    {
        if (blocksMaker.currentState == BlocksMaker.BoardState.move)
        {
            CheckSqaure();
            wordChecker.wordText.text = wordChecker._word;
            
        }
    }

    private void OnMouseUp()
    {
        GameEvents.ClearSelectionMethod();
        GameEvents.DisableSqaureSelectionMethod();
        wordChecker.CheckWord();
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
            gameObject.GetComponent<SpriteRenderer>().color = firstColor;
            if (_selected == true)
            {
            }
        }
    }


//******************************************************************************************************************
    public void SetUpAlphabet(Vector2Int posIndex, BlocksMaker theBlocksMaker)
    {
        this.pos = posIndex;
        blocksMaker = theBlocksMaker;
    }

    public void Destory()
    {
        // sfx
        // rotation
        /*blocksMaker.AddNewAlphabet(pos);*/

        Destroy(gameObject);
        blocksMaker.DecreaseRowCoRutin();
    }
}