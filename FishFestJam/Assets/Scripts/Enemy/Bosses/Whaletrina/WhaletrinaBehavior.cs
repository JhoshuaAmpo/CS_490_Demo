using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WhaletrinaBehavior : EnemyBehavior
{
    [TitleGroup("Dev Buttons")]
    [Button]
    public void ActivateRunItDown(){
        DecreaseHealth(maxHp.Value - maxHp.Value * (RunItDownThreshold - 0.01f));
    }
    [SerializeField]
    [Tooltip("Activates at this percent of maxHp left")]
    [Range(0f,1f)]
    private float RunItDownThreshold;
    private EnemyWeaponsManager weapons;
    private RunItDown runItDownAbility;
    private bool runItDownAbilityUsed = false;
    protected override void Awake()
    {
        base.Awake();
        weapons = GetComponentInChildren<EnemyWeaponsManager>();
        runItDownAbility = GetComponentInChildren<RunItDown>();
    }
    protected override void Update()
    {
        // if(!runItDownAbilityUsed && HealthPoints <= maxHp.Value * RunItDownThreshold){
        //     runItDownAbility.UseAbility();
        //     runItDownAbilityUsed = true;
        //     weapons.gameObject.SetActive(false);
        // }
        // if(!runItDownAbility.IsRunning){
        //     if(!weapons.gameObject.activeSelf) { weapons.gameObject.SetActive(true); } 
        //     base.Update();
        // }
    }

    private void OnCollisionStay2D(Collision2D other) {
        // Damage player if it touches me
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHandler>().TakeDamage(attackDamage.Value);
        }
    }
}
