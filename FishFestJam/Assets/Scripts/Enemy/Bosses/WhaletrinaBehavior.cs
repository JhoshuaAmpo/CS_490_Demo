using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WhaletrinaBehavior : EnemyBehavior
{
    [TitleGroup("Attack Commands")]
    public int temp;

    private EnemyWeaponsManager weapons;
    protected override void Awake()
    {
        base.Awake();
        weapons = GetComponentInChildren<EnemyWeaponsManager>();
    }
    protected override void Update()
    {
        base.Update();
    }
}
