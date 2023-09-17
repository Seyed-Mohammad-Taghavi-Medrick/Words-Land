using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text wordText;
    private  int score;
    public int sumOfPointOfLetters = 0;
    public int numOfTiles = 0;
    [SerializeField] private TextAsset database;
    private BlocksMaker _blocksMaker;
    private List<Alphabet> selectedWords = new List<Alphabet>();

    public string _word;
    // Start is called before the first frame update


    void Start()
    {
        _blocksMaker = FindObjectOfType<BlocksMaker>();
    }

    private void OnEnable()
    {
        GameEvents.OnCheckSqaure += SqaureSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSqaure -= SqaureSelected;
    }

    private void SqaureSelected(Alphabet obj, string letter, Vector3 position, int index)
    {
        numOfTiles++;
        sumOfPointOfLetters += obj.pointOfLetter;
        GameEvents.SelectSqaureMethod(position);
        _word += letter;
        selectedWords.Add(obj);
        Debug.Log("corent number of selected Tiles is " + numOfTiles);
        Debug.Log("corent Point is " + sumOfPointOfLetters);
    }

    public void CheckWord()
    {
        var words = database.text.Split();
        var wordToFind = _word.ToLower();

        var wordIndex = binarySearch(words, _word.ToLower());

        // if (wordIndex != -1)
        //  {
        DestroyCurrentWords();
        Debug.Log(_word);

        CalculateScore();

        // }

        numOfTiles = 0;
        sumOfPointOfLetters = 0;
        selectedWords.Clear();
        _word = string.Empty;
    }

    static int binarySearch(String[] arr, String x)
    {
        int l = 0, r = arr.Length - 1;

        // Loop to implement Binary Search
        while (l <= r)
        {
            // Calculatiing mid
            int m = l + (r - l) / 2;

            int res = x.CompareTo(arr[m]);

            // Check if x is present at mid
            if (res == 0)
                return m;

            // If x greater, ignore left half
            if (res > 0)
                l = m + 1;

            // If x is smaller, ignore right half
            else
                r = m - 1;
        }

        return -1;
    }

    private void DestroyCurrentWords()
    {
        foreach (var alphabet in selectedWords)
        {
            //    alphabet.isPartOfWord = true;
            _blocksMaker.DestroyAlphabetOfWordAt(alphabet.pos);
        }

        _blocksMaker.DecreaseRowCoRutin();
    }


    // Update is called once per frame
    void Update()
    {
    }


    static int PerformBinarySearch(List<string> list, string target)
    {
        int left = 0;
        int right = list.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int compareResult = String.Compare(list[mid], target);

            if (compareResult == 0)
            {
                return mid;
            }


            if (compareResult < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }


    private void CalculateScore()
    {
        score = (numOfTiles * sumOfPointOfLetters * 10) + score;
        
        ScoreText.text = $"{score}";
        Debug.Log("score is " + score);
        
    }


    /*int PerformBinarySearch(List<string> words , string target)
    {
        int left = 0;
        int right = words.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (words[mid] == target)
                return mid;

            if (words[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }*/
}