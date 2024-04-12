using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeHandler : MonoBehaviour
{
    [SerializeField]
    private float damageMultiplier = 1f;
    [SerializeField]
    private float BPSAdditive = 1f;
    PlayerWeaponHandler playerWeaponHandler;
    PlayerHandler playerHandler;
    private void Awake() {
        playerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        playerHandler = GetComponent<PlayerHandler>();
    }

    public void ProcessUpgrade(string upgrade){
        switch(upgrade){
            case "Damage":
                UpgradeDamage();
                break;
            case "B.P.S.":
                UpgradeBPS();
                break;
            case "Recovery":
                UpgradeRecovery();
                break;
            default:
                Debug.LogError($"{upgrade} is not an available upgrade");
                break;
        }
    }

    private void UpgradeDamage()
    {
        damageMultiplier *= damageMultiplier;
        playerWeaponHandler.SetAllWeaponDamageMultipliers(damageMultiplier);
    }

    private void UpgradeBPS()
    {
        BPSAdditive += BPSAdditive;
        playerWeaponHandler.AddBonusBPS(BPSAdditive);
    }

    private void UpgradeRecovery()
    {
        playerHandler.FullRestoreHP();
    }
}
