using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class FishSpray : MonoBehaviour, IEnemyWeapon
{
    [TitleGroup("BubbleGun Settings")]
         
    public float AttackSpeed;

    public float BaseDamage;

    public float WeaponMultiplier;

    public float AttackSize;   

    public float BubbleDuration;

    [TitleGroup("Phase Settings")]
    [EnumToggleButtons]
    public Phases CurrentPhase;
    [Button]
    public void ForceChangePhase() {
        ChangeGunLayout(CurrentPhase);
    }

    public string Name { get; set;}
    public bool IsActive { get; private set;}
    public enum Phases {Phase1, Phase2, Phase3}

    List<BubbleGun> bubbleGuns = new();

    private void Awake() {
        InitializeAllBubbleGuns();
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        ChangeGunLayout(CurrentPhase);
    }

    private void InitializeAllBubbleGuns() {
        float[] statsArr = new float[5];
        statsArr[0] = AttackSpeed;
        statsArr[1] = BaseDamage;
        statsArr[2] = WeaponMultiplier;
        statsArr[3] = AttackSize;
        statsArr[4] = BubbleDuration;
        List<BubbleGun> allBubbleGuns = new();
        GetComponentsInChildren<BubbleGun>(allBubbleGuns);
        foreach(BubbleGun bbg in allBubbleGuns) {
            bbg.InitiliazeStats(statsArr);
        }
    }

    public void Attack()
    {
        ActivateAttack(true);
    }

    public void StopAttack()
    {
        ActivateAttack(false);
    }

    public void SetPhase(Phases phase) {
        CurrentPhase = phase;
    }

    public void ActivateAttack(bool b){
        foreach (BubbleGun bbg in bubbleGuns)
        {
            if(b) {
                bbg.Attack();
            } else {
                bbg.StopAttack();
            }
        }
    }

    private void ChangeGunLayout(Phases p) {
        Transform child = transform;
        switch (p)
        {
            case Phases.Phase1:
                child = transform.GetChild(0);
                break;
            case Phases.Phase2:
                child = transform.GetChild(1);
                break;
            case Phases.Phase3:
                child = transform.GetChild(2);
                break;
            default:
                Debug.LogWarning("Changed to inaccessible gun!");
                break;
        }
        foreach(Transform c in transform) {
            c.gameObject.SetActive(false);
        }
        child.gameObject.SetActive(true);
        bubbleGuns.Clear();
        child.GetComponentsInChildren<BubbleGun>(bubbleGuns);
    }
}
