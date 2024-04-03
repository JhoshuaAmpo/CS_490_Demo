using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float AttackSpeed;
    public float BaseDamage;
    public Vector2 AttackSize;
    abstract public void Attack();

    abstract public void StopAttack();
}
