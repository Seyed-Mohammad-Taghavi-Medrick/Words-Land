using System;
using UnityEngine;

public static class GameEvents
{
    
    public static event Action OnEnableSqaureSelection;

    public static void EnableSqaureSelectionMethod()
    {
        OnEnableSqaureSelection?.Invoke();
    }

    //********************************************************
    public static event Action OnDisableSqaureSelection;

    public static void DisableSqaureSelectionMethod()
    {
        OnDisableSqaureSelection?.Invoke();
    }

    //********************************************************
    public static event Action<Vector3> OnSelectSqaure;

    public static void SelectSqaureMethod(Vector3 position)
    {
        OnSelectSqaure?.Invoke(position);
    }

    //********************************************************
    public static event Action<Alphabet, string, Vector3, int> OnCheckSqaure;

    public static void CheckSqaureMethod(Alphabet obj, string letter, Vector3 sqaurePosition, int sqaureIndex)
    {
        OnCheckSqaure?.Invoke(obj,letter, sqaurePosition, sqaureIndex);
    }

    //*******************************************************
    public static event Action OnClearSelection;

    public static void ClearSelectionMethod()
    {
        OnClearSelection?.Invoke();
    }
}