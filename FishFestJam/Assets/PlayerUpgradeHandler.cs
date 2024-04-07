using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeHandler : MonoBehaviour
{
    [SerializeField]
    private float damageMultiplier = 1f;
    [SerializeField]
    private float BPSMultiplier = 1f;
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
        playerWeaponHandler.SetAllWeaponMultipliers(damageMultiplier);
    }

    private void UpgradeBPS()
    {
        playerWeaponHandler.SetAllBPS(BPSMultiplier);
    }

    private void UpgradeRecovery()
    {
        playerHandler.FullRestoreHP();
    }
}
