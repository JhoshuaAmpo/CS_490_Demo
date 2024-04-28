using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class EnemyStat
{
    [BoxGroup("Upgrade Info")]
    [HorizontalGroup("Upgrade Info/Row1",marginRight: 20)]
    public float Value = 0f;

    [BoxGroup("Upgrade Info")]
    [HorizontalGroup("Upgrade Info/Row1")]
    public bool IsInt = false;

    [MinValue(0)]
    [BoxGroup("Upgrade Info")]
    [HorizontalGroup("Upgrade Info/Row2")]
    [Tooltip("In Seconds")]
    [LabelWidth(150f)]
    public int TimeBetweenUpgrades = 1;

    [MinValue(0f)]
    [BoxGroup("Upgrade Formula")]
    [HorizontalGroup("Upgrade Formula/Formula")]
    [LabelWidth(100f)]
    [LabelText("value = value * ")]
    public float MultiplierValue = 1f;

    [BoxGroup("Upgrade Formula")]
    [HorizontalGroup("Upgrade Formula/Formula")]
    [LabelWidth(30f)]
    [LabelText(" + ")]
    public float AddendValue = 0f;
    
    public enum ClampOptions{min, max, none}

    [HorizontalGroup("clamp")]
    [Tooltip("Max: Adds a max value\nMin: Adds a min value\nNone: no limits")]
    public ClampOptions IsClamp = ClampOptions.none;

    [HorizontalGroup("clamp")]
    [HideIf("IsClamp", ClampOptions.none)]
    [LabelText("@IsClamp == ClampOptions.max ? \"Max:\" : \"Min:\"")]
    public float ClampVal = 0;

    [HideInEditorMode]
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
