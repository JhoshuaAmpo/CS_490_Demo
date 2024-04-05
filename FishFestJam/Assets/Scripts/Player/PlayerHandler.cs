using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed = 90f;
    public static PlayerHandler Instance { get; private set;}
    public float HealthPoints;

    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Update() {
        Vector3 screenMousePos = Mouse.current.position.ReadValue();
        screenMousePos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePos);
        Debug.Log($"MousePos: {mousePosition}");
        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, direction);
        // Debug.Log($"Angle: {angle}");

        Vector3 targetRotation = new(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed * Time.deltaTime);
    }

    public void TakeDamage(float dmg){
        HealthPoints -= dmg;
        Debug.Log($"Ouch! I took {dmg} dmg");
        if(HealthPoints <= 0)
        {
            Death();
        }
    }

    private void Death(){
        Debug.Log("I died");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 screenMousePos = Mouse.current.position.ReadValue();
        screenMousePos.z = -10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePos);
        // Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Gizmos.DrawLine(transform.position,mousePosition);
        // Debug.Log("Pos: " + pos);
    }
}
