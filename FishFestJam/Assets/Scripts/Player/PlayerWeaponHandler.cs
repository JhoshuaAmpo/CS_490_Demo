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

      playerControls.Abilities.ToggleAttack.performed += ToggleAttack;
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

   public void ToggleAttack(InputAction.CallbackContext context) {
      if (!context.performed || PauseGame.Instance.isGamePaused) { return; }
      foreach (BaseWeapon weapon in weapons)
      {
         weapon.ToggleAttack();
      }
   }

   public void StopAllAttack() {
      foreach (BaseWeapon weapon in weapons)
      {
         weapon.StopAttack();
      }
   }

   public void StartAllAttack() {
      foreach (BaseWeapon weapon in weapons)
      {
         weapon.Attack();
      }
   }

   public void SetAllWeaponDamageMultipliers(float multiplier) {
      foreach (BaseWeapon weapon in weapons)
      {
         weapon.WeaponMultiplier = multiplier;
      }
   }
   
   public void AddBonusBPS(float bonus) {
      foreach (BaseWeapon weapon in weapons)
      {
         var mainModule = weapon.GetComponent<ParticleSystem>().main;
         mainModule.simulationSpeed = weapon.AttackSpeed + bonus;
      }
   }

   // public bool TryAddWeapon(GameObject weapon)
   // {
   //    if(!weapon.TryGetComponent<BaseWeapon>(out var bw)) { return false; }
   //    weapons.Add(bw);
   //    return true;
   // }
}
