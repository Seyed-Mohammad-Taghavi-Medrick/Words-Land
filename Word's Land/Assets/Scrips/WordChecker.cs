using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    [SerializeField] private TextAsset database;

    private List<Alphabet> selectedWords = new List<Alphabet>();

    private string _word;
    // Start is called before the first frame update

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
        GameEvents.SelectSqaureMethod(position);
        _word += letter;
        selectedWords.Add(obj);
    }

    public void CheckWord()
    {
        var words = database.text.Split().ToList();
        var wordToFind = _word.ToLower();

        foreach (var serchingWord in words)
        {
            if (_word.ToLower() == serchingWord)
            {
                DestroyCurrentWords();
                Debug.Log(_word);
                return;
            }
        }
    }

    private void DestroyCurrentWords()
    {
        foreach (var alphabet in selectedWords)
            alphabet.Destory();

        selectedWords.Clear();
    }

    void Start()
    {
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

        return -1; // لغت مورد نظر پیدا نشد
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

        return -1; // عنصر مورد جستجو پیدا نشد
    }*/
}