using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(ParticleSystem))]
public class BubbleGun : BaseWeapon
{
    ParticleSystem ps;
    [Min(0)]
    public float BubbleDuration;
    [Min(0)]
    public float BubbleSpeed;
    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    private void Start() {
        var psMain = ps.main;
        psMain.simulationSpeed = AttackSpeed;
        psMain.startSize = AttackSize;
        psMain.startLifetime = BubbleDuration;
        psMain.startSpeed = BubbleSpeed;
    }

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
    ///     <item>
    ///         <term>[4]</term>
    ///         <description>BubbleDuration</description>
    ///     </item>
    ///     <item>
    ///         <term>[5]</term>
    ///         <description>BubbleSpeed</description>
    ///     </item>
    /// </list>
    /// </summary>
    public override void InitiliazeStats(params float[] stats) {
      base.InitiliazeStats(stats);
      BubbleDuration = stats[4];
      BubbleSpeed = stats[5];
    }

    public override void Attack()
    {
        if(!ps.emission.enabled) {
            // Debug.Log("Bubbles enabled");
            var emissionModule = ps.emission;
            ps.Play();
            emissionModule.enabled = true;
        }
    }

    public override void StopAttack()
    {
        // Debug.Log("Done firing!");
        var emissionModule = ps.emission;
        emissionModule.enabled = false;
        ps.Stop();
    }

    public override void ToggleAttack()
    {
        var emissionModule = ps.emission;
        emissionModule.enabled = !emissionModule.enabled;
        if(emissionModule.enabled)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();   
        }
    }

    
}
