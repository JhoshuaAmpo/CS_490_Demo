using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
   public List<GameObject> weapons;
   PlayerControls playerControls;

   private void Awake() {
      playerControls = new();
      playerControls.Enable();

      playerControls.Abilities.Attack.performed += PerformAttack;
   }

   /// <summary>
   /// Will try to add the weapon to the weapons list
   /// Returns true if it does, false if it can't
   /// </summary>
   public bool TryAddWeapon(GameObject weapon)
   {
      if(weapon.GetComponent<IWeapon>() == null) { return false; }
      weapons.Add(weapon);
      return true;
   } 
   
   private void PerformAttack(InputAction.CallbackContext c)
   {
      if(!c.performed) { return; }
      foreach (GameObject weapon in weapons)
      {
         weapon.GetComponent<IWeapon>().Attack();
      }
   }
}
