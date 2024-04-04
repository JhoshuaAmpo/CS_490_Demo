using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private Action timerCallBack;
    public void SetTimer(float time, Action timerCallBack) {
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
