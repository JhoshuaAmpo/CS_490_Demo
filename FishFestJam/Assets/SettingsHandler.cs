using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    public void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Debug.Log($"MusicVolume: {PlayerPrefs.GetFloat("MusicVolume")}");
    }

    public void SaveSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        Debug.Log($"SFXVolume: {PlayerPrefs.GetFloat("SFXVolume")}");
    }

    public void SaveAltControl(bool altCtrl)
    {
        int temp = altCtrl ? 1 : 0;
        PlayerPrefs.SetInt("AltCtrl", temp);
        Debug.Log($"AltCtrl: {PlayerPrefs.GetInt("AltCtrl")}");
    }
}
