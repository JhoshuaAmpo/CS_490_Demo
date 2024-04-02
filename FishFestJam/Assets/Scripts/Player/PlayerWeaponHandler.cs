using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
   public List<IWeapon> weapons;
   PlayerControls playerControls;

   private void Awake() {
      playerControls = new();
      playerControls.Enable();

      playerControls.Abilities.Attack.performed += PerformAttack;
   }

   private void PerformAttack(InputAction.CallbackContext c)
   {
      if(!c.performed) { return; }
      foreach (IWeapon weapon in weapons)
      {
         weapon.Attack();
      }
   }
}
