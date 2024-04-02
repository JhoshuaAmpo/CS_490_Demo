using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour, IWeapon
{
    void IWeapon.Attack()
    {
        Debug.Log(gameObject.name + " attack!");
    }
}
