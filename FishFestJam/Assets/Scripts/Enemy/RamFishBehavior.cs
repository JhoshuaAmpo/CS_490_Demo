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

    protected override void UpgradeFish(){
        Debug.Log("Enemy Upgrade!");
        UpdateAllStats(swimSpeed+5,delayBetweenSwims-0.1f,attackDamage+1,turnSpeed+45,expDrop,maxHp+5, selfDmg - 1);
    }

    private void UpdateAllStats(float newSwimSpeed, float newDelayBetweenSwims, float newAttackDamage, float newTurnSpeed, int newExpDrop, float newMaxHp, float newSelfDmg){
        swimSpeed = newSwimSpeed;
        if(delayBetweenSwims > 0.1f) { delayBetweenSwims = newDelayBetweenSwims; }
        attackDamage = newAttackDamage;
        if(turnSpeed < 360){ turnSpeed = newTurnSpeed; }
        expDrop = newExpDrop;
        maxHp = newMaxHp;
        if(selfDmg > 1) {selfDmg = newSelfDmg;}
    }
}
