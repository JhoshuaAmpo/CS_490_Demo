using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float AttackSpeed;
    public float BaseDamage;
    public Vector2 AttackSize;

    // Run at the end of awake
    protected void InitiliazationTest() {
        if(BaseDamage <= 0)
         {
            Debug.LogError($"{this.name} does {BaseDamage} dmg");
         }
         if(AttackSpeed <= 0)
         {
            Debug.LogError($"{this.name} has {AttackSpeed} atk spd");
         }
    }
    abstract public void Attack();

    abstract public void StopAttack();
}
