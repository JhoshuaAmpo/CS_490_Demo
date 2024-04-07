using System.Collections;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
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

    

    

    public void TakeDamage(float dmg){
        HealthPoints -= dmg;
        Debug.Log($"Ouch! I took {dmg} dmg");
        if(HealthPoints <= 0)
        {
            Death();
        }
    }

    

    private void Death(){
        
    }
}
