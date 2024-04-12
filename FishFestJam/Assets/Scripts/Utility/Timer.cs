using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To have multiple timers in one gameObject create a time variable, set it to AddComponent<Timer>();
/// </summary>
public class Timer : MonoBehaviour
{
    private float time;
    private Action timerCallBack;
    public void SetTimer(float time, Action timerCallBack) {
        if(!IsTimerComplete()) { return;}
        this.time = time;
        this.timerCallBack = timerCallBack;
    }

    private void Update() {
        if (time > 0f)
        {
            time -= Time.deltaTime;

            if (IsTimerComplete())
            {
                timerCallBack();
            }
        }
    }

    public bool IsTimerComplete(){
        return time <= 0f;
    }
}
