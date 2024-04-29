using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamFishBehavior : EnemyBehavior
{
    [SerializeField]
    private EnemyStat selfDmg;

    private void OnCollisionStay2D(Collision2D other) {
        DmgPlayer(other);
    }

    private void DmgPlayer(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHandler>().TakeDamage(attackDamage.Value);
            DecreaseHealth(selfDmg.Value);
        }
    }
    
    protected override void SwimPattern(GameObject t)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 newVel = rb.velocity;
        Vector2 dir = (target.transform.position - transform.position).normalized;
        if(rb.velocity.x * dir.x < 0) {newVel.x /= 2;}
        if(rb.velocity.y * dir.y < 0) {newVel.y /= 2;}
        rb.velocity = newVel;
        rb.AddForce(dir * swimSpeed.Value,ForceMode2D.Force);
    }

    protected override void InitializeAllStats()
    {
        base.InitializeAllStats();
        InitializeStat(selfDmg, "selfDmg");
    }
    protected override void UpgradeAllStats()
    {
        base.UpgradeAllStats();
        UpgradeStat(selfDmg);
    }

}
