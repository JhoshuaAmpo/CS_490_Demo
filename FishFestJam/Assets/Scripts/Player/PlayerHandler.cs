using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        Debug.Log("I died");
    }
}
