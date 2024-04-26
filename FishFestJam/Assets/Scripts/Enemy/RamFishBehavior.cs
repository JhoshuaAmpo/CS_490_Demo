using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamFishBehavior : EnemyBehavior
{
    [SerializeField]
    private float selfDmg = 10f;

    private void OnCollisionStay2D(Collision2D other) {
        DmgPlayer(other);
    }

    private void DmgPlayer(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHandler>().TakeDamage(attackDamage);
            DecreaseHealth(selfDmg);
        }
    }
}
