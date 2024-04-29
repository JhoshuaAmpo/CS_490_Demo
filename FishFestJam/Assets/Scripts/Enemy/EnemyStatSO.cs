using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemy", menuName = "Enemy", order = 1)]
public class EnemyStatSO : SerializedScriptableObject
{
    public Sprite sprite;
    [Header("Base Stats")]
    public EnemyStat SwimSpeed;
    public EnemyStat DelayBetweenSwims;
    public EnemyStat AttackDamage;
    public EnemyStat TurnSpeed;
    public EnemyStat ExpDrop;
    public EnemyStat MaxHp;
}
