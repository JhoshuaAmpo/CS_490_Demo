using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

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
    [HideIf("StatName", "")]
    [HideIf("IsClamp", ClampOptions.none)]
    [LabelText("@IsClamp == ClampOptions.max ? \"Max:\" : \"Min:\"")]
    public float ClampVal = 0;

    private bool toggle = false;
    [Button("Toggle Upgrade Chart")]
    public void ToggleUpgradeChart(){
        toggle = !toggle;
    }

    [Min(0F)]
    [BoxGroup("UpgradeChart",false)]
    [HorizontalGroup("UpgradeChart/Fields", VisibleIf = "toggle")]
    public int MaxTime;

    [BoxGroup("UpgradeChart")]
    [HorizontalGroup("UpgradeChart/Chart", VisibleIf = "toggle")]
    [ListDrawerSettings(IsReadOnly = true, NumberOfItemsPerPage = 6,OnTitleBarGUI = "DrawGenerateChartButton")]
    public List<String> upgradeChart = new();

    [HideInEditorMode]
    public Timer statTimer;

    
    private void DrawGenerateChartButton() {
        if(SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh)){
            GenerateChart();
        }
    }

    public string Name {get; private set;}

    public void UpgradeStat(){
        Value = (IsInt) ? LinearUpgradeFormula((int)Value) : LinearUpgradeFormula(Value);
        // Debug.Log($"{Name} upgraded: {Value}");
    }

    public void UpgradeStat(ref float val){
        val = (IsInt) ? LinearUpgradeFormula((int)val) : LinearUpgradeFormula(val);
    }

    public void Initialize(string n, Timer t){
        GenerateChart();
        statTimer = t;
        Name = n;
    }

    private void GenerateChart() {
        upgradeChart.Clear();
        float tempVal = Value;
        for(int time = 0;time <= MaxTime; time += TimeBetweenUpgrades) {
            upgradeChart.Add(new($"{time/60:00}:{time%60:00} => {tempVal}"));
            UpgradeStat(ref tempVal);
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
