using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStat : MonoBehaviour
{
    public string StatName = "";
    public float StatValue = 0;

    [Tooltip("In Seconds")][Min(1)]
    public int TimeBetweenUpgrades = 1;
    [Tooltip("Default: 0\nFormula: baseVal * MultiplierValue + AddendValue")]
    
    public float AddendValue = 0f;
    [Tooltip("Default: 1\nFormula: baseVal * MultiplierValue + AddendValue")][Min(0f)]
    public float MultiplierValue = 1f;
    public enum ClampOptions{min, max, none}
    public ClampOptions IsClamp = ClampOptions.none;
    public float ClampVal = 0;

    private Timer upgradeTimer;

    private void Awake() {
        gameObject.name = StatName;
        upgradeTimer = this.gameObject.AddComponent<Timer>();
    }

    private void Update() {
        upgradeTimer.SetTimer(TimeBetweenUpgrades,() => {StatValue = LinearUpgradeFormula(StatValue);});
    }

    private float LinearUpgradeFormula(float baseVal)
    {
        float product = baseVal * MultiplierValue + AddendValue;
        if(IsClamp == ClampOptions.max)
        {
            return MathF.Min(product, ClampVal);
        }
        else if(IsClamp == ClampOptions.min)
        {
            return MathF.Max(product, ClampVal);
        }
        DebugLogEnemyStats();
        return product;
    }

    private int LinearUpgradeFormula(int baseVal)
    {
        int product = Mathf.FloorToInt(baseVal * MultiplierValue) + Mathf.CeilToInt(AddendValue);
        if(IsClamp == ClampOptions.max)
        {
            return Math.Min(product, (int)ClampVal);
        }
        else if(IsClamp == ClampOptions.min)
        {
            return Math.Max(product, (int)ClampVal);
        }
        return product;
    }

    private void DebugLogEnemyStats()
    {
        Debug.Log($"{gameObject.name} : {StatValue}");
    }
}
