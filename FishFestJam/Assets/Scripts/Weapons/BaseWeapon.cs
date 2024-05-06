using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float AttackSpeed;
    public float BaseDamage;
    public float WeaponMultiplier;
    public float AttackSize;

   /// <summary>
    /// <list type="element|stat name">
    ///     <item>
    ///         <term>[0]</term>
    ///         <description>AttackSpeed</description>
    ///     </item>
    ///     <item>
    ///         <term>[1]</term>
    ///         <description>BaseDamage</description>
    ///     </item>
    ///     <item>
    ///         <term>[2]</term>
    ///         <description>WeaponMultiplier</description>
    ///     </item>
    ///     <item>
    ///         <term>[3]</term>
    ///         <description>AttackSize</description>
    ///     </item>
    /// </list>
    /// </summary>
    public virtual void InitiliazeStats(params float[] stats) {
      AttackSpeed = stats[0];
      BaseDamage = stats[1];
      WeaponMultiplier = stats[2];
      AttackSize = stats[3];
    }
    abstract public void Attack();

    abstract public void StopAttack();

    abstract public void ToggleAttack();
}
