using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummy : EnemyBehavior
{
    private Timer DPStimer;
    private TextMeshPro text;
    float damageCount = 0f;
    protected override void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
        DPStimer = GetComponent<Timer>();
        DPStimer.SetTimer(1, () => {ResetDamageCount();});
    }
    protected override void Update()
    {
        DPStimer.SetTimer(1, () => {ResetDamageCount();});
    }
    protected override void OnParticleCollision(GameObject other) {
        damageCount += other.GetComponent<BaseWeapon>().BaseDamage * other.GetComponent<BaseWeapon>().WeaponMultiplier;
    }
    private void ResetDamageCount(){
        text.text = $"DPS: {damageCount}";
        damageCount = 0;
    }
}
