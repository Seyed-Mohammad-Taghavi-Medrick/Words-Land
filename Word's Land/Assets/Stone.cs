using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private WordChecker _wordChecker;
    private Alphabet _alphabet;
    // Start is called before the first frame update
    void Start()
    {
        _wordChecker = FindObjectOfType<WordChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseEnter()
    {
        GameEvents.ClearSelectionMethod();
        _wordChecker.currentScore = 0;
        _wordChecker.ResetSelectedWord();
    }
}
