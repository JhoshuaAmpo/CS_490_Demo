using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public static Stopwatch Instance { get; private set;}
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI deathScreenTimerText;
    float timer;
    int mins;
    int secs;
    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
        timer = 0;
        mins = 0;
        secs = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        mins = Mathf.FloorToInt(timer / 60);
        secs = Mathf.FloorToInt(timer % 60);
        UpdateTimerText();
    }

    public int GetSeconds(){
        // Debug.Log("Secs: " + secs);
        return secs;
    }

    public int GetMinute(){
        return mins;
    }
    public string GetStringTime(){
        return timerText.text;
    }

    void UpdateTimerText(){
        timerText.text = $"{mins}:{secs:00}";
        deathScreenTimerText.text = $"{mins}:{secs:00} minutes!!!";
    }
}
