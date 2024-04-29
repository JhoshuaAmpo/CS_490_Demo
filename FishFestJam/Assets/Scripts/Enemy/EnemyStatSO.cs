using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemy", menuName = "Enemy", order = 1)]
public class EnemyStatSO : SerializedScriptableObject
{
    public Sprite sprite;
    [DictionaryDrawerSettings(KeyLabel = "Stat Name: ", ValueLabel = "Stat Info", DisplayMode = DictionaryDisplayOptions.CollapsedFoldout)]
    public Dictionary<string,EnemyStat> EnemyStats;
}
