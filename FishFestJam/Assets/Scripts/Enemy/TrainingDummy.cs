using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : EnemyBehavior
{
    private Timer DPStimer;
    float damageCount = 0f;
    protected override void Awake()
    {
        Debug.Log("Child Awake called");
        DPStimer = GetComponent<Timer>();
        DPStimer.SetTimer(1, () => {DisplayDPS();});
    }
    protected override void Update()
    {
        DPStimer.SetTimer(1, () => {DisplayDPS();});
    }
    protected override void OnParticleCollision(GameObject other) {
        damageCount += other.GetComponent<BaseWeapon>().BaseDamage * other.GetComponent<BaseWeapon>().WeaponMultiplier;
    }
    private void DisplayDPS(){
        Debug.Log($"DPS: {damageCount}");
        damageCount = 0;
    }
}
