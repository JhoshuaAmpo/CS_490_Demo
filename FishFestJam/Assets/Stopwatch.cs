using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI deathScreenTimerText;
    float timer;
    void Awake()
    {
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerText();
    }

    public string GetStringTime(){
        return timerText.text;
    }

    void UpdateTimerText(){
        int mins = Mathf.FloorToInt(timer / 60);
        int secs = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{mins}:{secs:00}";
        deathScreenTimerText.text = $"{mins}:{secs:00} minutes!!!";
    }
}
