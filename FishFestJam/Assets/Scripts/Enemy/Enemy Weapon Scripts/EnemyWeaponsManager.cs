using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

public class EnemyWeaponsManager : MonoBehaviour
{
    public int WeaponNum;
    private List<IEnemyWeapon> weapons = new();
    void Awake()
    {
        GetComponentsInChildren<IEnemyWeapon>(weapons);
    }

    void Update(){
        UseWeapon(WeaponNum); 
    }

    public void UseWeapon(int i){
        weapons[i].Attack();
    }
}
