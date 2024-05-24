using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerInCollider : MonoBehaviour
{
    public bool IsPlayerInCollider { get; private set; } = false;
    public GameObject PlayerObj { get; private set; }

    private void Awake() {
        PlayerObj = null;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) { return; }
        IsPlayerInCollider = true;
        PlayerObj = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        IsPlayerInCollider = false;
        PlayerObj = null;
    }
}
