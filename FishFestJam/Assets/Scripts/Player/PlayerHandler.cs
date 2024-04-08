using System.Collections;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    public static PlayerHandler Instance { get; private set;}

    [SerializeField]
    private float maxHP = 100;
    [SerializeField]
    private HUDBarController hpBar;
    private float HealthPoints = 0f;

    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        FullRestoreHP();
    }

    public void FullRestoreHP(){
        HealthPoints = maxHP;
        hpBar.SetBarWidth(HealthPoints/maxHP);
    }

    public void TakeDamage(float dmg){
        HealthPoints -= dmg;
        hpBar.SetBarWidth(HealthPoints/maxHP);
        if(HealthPoints <= 0)
        {
            Death();
        }
    }

    private void Death(){
        Debug.Log("You lose!");
        // Pause game
        // Put HUD up
    }
}
