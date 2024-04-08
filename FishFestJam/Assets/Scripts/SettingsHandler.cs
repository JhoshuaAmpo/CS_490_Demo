using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Toggle altCtrlToggle;
    
    private void Awake() {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        altCtrlToggle.isOn = PlayerPrefs.GetInt("AltCtrl") == 1;
    }

    public void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        // Debug.Log($"MusicVolume: {PlayerPrefs.GetFloat("MusicVolume")}");
    }

    public void SaveSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // Debug.Log($"SFXVolume: {PlayerPrefs.GetFloat("SFXVolume")}");
    }

    public void SaveAltControl(bool altCtrl)
    {
        int temp = altCtrl ? 1 : 0;
        PlayerPrefs.SetInt("AltCtrl", temp);
        // Debug.Log($"AltCtrl: {PlayerPrefs.GetInt("AltCtrl")}");
    }
}
