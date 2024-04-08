using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamFishBehavior : EnemyBehavior
{
    [SerializeField]
    private float selfDmg = 5f;
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     DmgPlayer(other);
    // }

    private void OnCollisionStay2D(Collision2D other) {
        DmgPlayer(other);
    }

    private void DmgPlayer(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHandler>().TakeDamage(AttackDamage);
            DecreaseHealth(selfDmg);
        }
    }
}
