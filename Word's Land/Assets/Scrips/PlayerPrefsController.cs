using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";


    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;


    public static void SetMasterVolume(float volume)
    {
        InitMasterVolume();

        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            Debug.Log("Master volume set to " + volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }

    public static float GetMasterVolume()
    {
        InitMasterVolume();
        
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    private static void InitMasterVolume()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, 1);
    }
}