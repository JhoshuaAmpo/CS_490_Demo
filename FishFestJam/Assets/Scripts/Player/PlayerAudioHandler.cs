using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioHandler : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void UpdateSFXVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public bool AudioIsPlaying(){
        return audioSource.isPlaying;
    }
}
