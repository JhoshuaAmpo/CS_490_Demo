using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(ParticleSystem))]
public class BubbleGun : BaseWeapon
{
    ParticleSystem ps;
    private void Awake() {
        ps = GetComponent<ParticleSystem>();
        var psMain = ps.main;
        psMain.simulationSpeed = AttackSpeed;

        InitiliazationTest();
    }
    public override void Attack()
    {
        if(!ps.isPlaying) {
            Debug.Log("Bubbles enabled");
            float tinyStep = 0.000001f;
            ps.Simulate(tinyStep, true, true, false);
            ps.Play(); 
        }
    }
    public override void StopAttack()
    {
        Debug.Log("Done firing!");
        ps.Stop();
    }
}
