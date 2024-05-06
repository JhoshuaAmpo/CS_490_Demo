using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemyWeapon
{
    public string Name { get; set;}
    public void Attack();
}
