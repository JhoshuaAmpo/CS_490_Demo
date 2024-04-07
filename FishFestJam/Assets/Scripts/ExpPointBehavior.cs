using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpPointBehavior : MonoBehaviour
{
    [SerializeField]
    private int ExpValue;
    private Collider2D collide2D;
    private void Awake() {
        collide2D = GetComponent<Collider2D>();
        collide2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) { return; }
        other.GetComponent<PlayerExpHandler>().GainExp(ExpValue);
        gameObject.SetActive(false);
    }

    public void SetExpValue(int exp)
    {
        ExpValue = exp;     
    }
}
