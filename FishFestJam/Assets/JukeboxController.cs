using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxController : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateMusicvolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
