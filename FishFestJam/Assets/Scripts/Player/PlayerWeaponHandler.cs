using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
   public List<BaseWeapon> weapons;
   PlayerControls playerControls;

   private void Awake() {
      playerControls = new();
      playerControls.Enable();

      this.GetComponentsInChildren(weapons);
   }

   private void Update() {
      foreach (BaseWeapon weapon in weapons)
      {
         if(playerControls.Abilities.Attack.IsPressed())
         {
            weapon.GetComponent<BaseWeapon>().Attack();
         }
         else if(playerControls.Abilities.Attack.WasReleasedThisFrame())
         {
            weapon.GetComponent<BaseWeapon>().StopAttack();
         }
      }
   }

   // public bool TryAddWeapon(GameObject weapon)
   // {
   //    if(!weapon.TryGetComponent<BaseWeapon>(out var bw)) { return false; }
   //    weapons.Add(bw);
   //    return true;
   // }
}
