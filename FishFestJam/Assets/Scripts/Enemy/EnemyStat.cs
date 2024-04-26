using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class EnemyStat
{
    public float Value = 0f;
    public bool IsInt = false;
    [Header("Upgrade values")]

    [Tooltip("In Seconds")][Min(1)]
    public int TimeBetweenUpgrades = 1;
    [Tooltip("Default: 0\nFormula: baseVal * MultiplierValue + AddendValue")]
    public float AddendValue = 0f;
    [Tooltip("Default: 1\nFormula: baseVal * MultiplierValue + AddendValue")][Min(0f)]
    public float MultiplierValue = 1f;
    public enum ClampOptions{min, max, none}
    public ClampOptions IsClamp = ClampOptions.none;
    public float ClampVal = 0;
    public Timer statTimer;
    private List<String> upgradeChart = new();
    private string name;

    public void UpgradeStat(){
        Value = (IsInt) ? LinearUpgradeFormula((int)Value) : LinearUpgradeFormula(Value);
        Debug.Log($"{name} upgraded: {Value}");
    }

    public void UpgradeStat(ref float val){
        val = (IsInt) ? LinearUpgradeFormula((int)val) : LinearUpgradeFormula(val);
    }

    public void Initialize(string n, Timer t){
        SetUpUpgradeChart();
        statTimer = t;
        name = n;
    }
    public void SetUpUpgradeChart() {
        float tempVal = Value;
        int time = 0;
        upgradeChart.Add(new($"{time/60:00}:{time%60:00} => {tempVal}"));
        for(;time <= 600; time += TimeBetweenUpgrades) {
            UpgradeStat(ref tempVal);
            upgradeChart.Add(new($"{time/60:00}:{time%60:00} => {tempVal}"));
        }
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
}
