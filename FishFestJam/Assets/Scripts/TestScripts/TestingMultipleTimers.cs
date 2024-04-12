using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingMultipleTimers : MonoBehaviour
{
    Timer timer1;
    Timer timer2;
    Timer timer3;
    private void Awake() {
        timer1 = this.AddComponent<Timer>();
        timer2 = this.AddComponent<Timer>();
        timer3 = this.AddComponent<Timer>();
    }

    private void Start() {
        Debug.Log("Timer 1 has started!");
        timer1.SetTimer(5f, ()=>{Debug.Log("1st Timer has finished"); });
        Debug.Log("Timer 2 has started!");
        timer2.SetTimer(10f, ()=>{Debug.Log("2nd Timer has finished"); });
        Debug.Log("Timer 3 has started!");
        timer3.SetTimer(20f, ()=>{Debug.Log("3rd Timer has finished"); });
    }
}
