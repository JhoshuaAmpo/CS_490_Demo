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
        psMain.startSize = AttackSize;

        InitiliazationTest();
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
