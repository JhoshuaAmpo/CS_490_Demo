using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParticleSystem))]
public class BubbleGun : BaseWeapon
{
    ParticleSystem ps;
    private void Awake() {
        ps = GetComponent<ParticleSystem>();      
    }
    public override void Attack()
    {
        if(!ps.isPlaying) {
            Debug.Log("Bubbles enabled");
            ps.Play(); 
        }
    }
    public override void StopAttack()
    {
        Debug.Log("Done firing!");
        ps.Stop();
    }
}
