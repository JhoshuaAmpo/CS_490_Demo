using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Serializable]
// public class TempTuple {
//     public string time;
//     public float val;
//     public TempTuple (string t, float v) {
//         time = t;
//         val = v;
//     }
    
// }

[Serializable]
public class UpgradeData {
    [Tooltip("In Seconds")][Min(1)]
    public int TimeBetweenUpgrades = 1;
    [Tooltip("Default: 0\nFormula: baseVal * MultiplierValue + AddendValue")]
    
    public float AddendValue = 0f;
    [Tooltip("Default: 1\nFormula: baseVal * MultiplierValue + AddendValue")][Min(0f)]
    public float MultiplierValue = 1f;

    // private List<TempTuple> upgradeChart = new();
    public List<String> upgradeChart = new();
    public void SetUpUpgradeChart(float baseVal) {
        for(int time = 0;time <= 600; time += TimeBetweenUpgrades) {
            upgradeChart.Add(new($"{time/60:00}:{time%60:00} - {baseVal}"));
            baseVal = baseVal * MultiplierValue + AddendValue;
        }
    }
}

public class RamFishBehavior : EnemyBehavior
{
    [SerializeField]
    private float selfDmg = 10f;
    [SerializeField]
    UpgradeData swimSpeedUpgradeData;
    [SerializeField]
    UpgradeData delayBetweenSwimsUpgradeData;
    [SerializeField] 
    UpgradeData attackDamageUpgradeData;
    [SerializeField] 
    UpgradeData turnSpeedUpgradeData;
    [SerializeField] 
    UpgradeData expDropUpgradeData;
    [SerializeField] 
    UpgradeData maxHpUpgradeData;
    [SerializeField] 
    UpgradeData selfDmgUpgradeData;

    Timer swimSpeedUpgradeTimer;
    Timer delayBetweenSwimsUpgradeTimer;
    Timer attackDamageUpgradeTimer;
    Timer turnSpeedUpgradeTimer;
    Timer expDropUpgradeTimer;
    Timer maxHpUpgradeTimer;
    Timer selfDmgUpgradeTimer;


    protected override void Awake()
    {
        base.Awake();
        swimSpeedUpgradeTimer = this.gameObject.AddComponent<Timer>();
        delayBetweenSwimsUpgradeTimer = this.gameObject.AddComponent<Timer>();
        attackDamageUpgradeTimer = this.gameObject.AddComponent<Timer>();
        turnSpeedUpgradeTimer = this.gameObject.AddComponent<Timer>();
        expDropUpgradeTimer = this.gameObject.AddComponent<Timer>();
        maxHpUpgradeTimer = this.gameObject.AddComponent<Timer>();
        selfDmgUpgradeTimer = this.gameObject.AddComponent<Timer>();

        // while(this)
        // {
        //     UpgradeAll();
        // }
    }

    protected override void Start()
    {
        base.Start();
        SetUpAllUpgradeCharts();

    }

    private void SetUpAllUpgradeCharts()
    {
        swimSpeedUpgradeData.SetUpUpgradeChart(swimSpeed);
        attackDamageUpgradeData.SetUpUpgradeChart(attackDamage);
        delayBetweenSwimsUpgradeData.SetUpUpgradeChart(delayBetweenSwims);
        turnSpeedUpgradeData.SetUpUpgradeChart(turnSpeed);
        expDropUpgradeData.SetUpUpgradeChart(expDrop);
        maxHpUpgradeData.SetUpUpgradeChart(maxHp);
        selfDmgUpgradeData.SetUpUpgradeChart(selfDmg);
    }

    private void OnCollisionStay2D(Collision2D other) {
        DmgPlayer(other);
    }

    private void DmgPlayer(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHandler>().TakeDamage(attackDamage);
            DecreaseHealth(selfDmg);
        }
    }

    protected override void UpgradeAll(){
        swimSpeedUpgradeTimer.SetTimer(swimSpeedUpgradeData.TimeBetweenUpgrades, () => {swimSpeed = LinearUpgradeFormula(swimSpeed,swimSpeedUpgradeData);});
        attackDamageUpgradeTimer.SetTimer(attackDamageUpgradeData.TimeBetweenUpgrades, () => {attackDamage = LinearUpgradeFormula(attackDamage,attackDamageUpgradeData);});
        delayBetweenSwimsUpgradeTimer.SetTimer(delayBetweenSwimsUpgradeData.TimeBetweenUpgrades, () => {delayBetweenSwims = LinearUpgradeFormula(delayBetweenSwims,delayBetweenSwimsUpgradeData);});
        turnSpeedUpgradeTimer.SetTimer(turnSpeedUpgradeData.TimeBetweenUpgrades, () => {turnSpeed = LinearUpgradeFormula(turnSpeed,turnSpeedUpgradeData);});
        expDropUpgradeTimer.SetTimer(expDropUpgradeData.TimeBetweenUpgrades, () => {expDrop = LinearUpgradeFormula(expDrop,expDropUpgradeData);});
        maxHpUpgradeTimer.SetTimer(maxHpUpgradeData.TimeBetweenUpgrades, () => {maxHp = LinearUpgradeFormula(maxHp,maxHpUpgradeData);});
        selfDmgUpgradeTimer.SetTimer(selfDmgUpgradeData.TimeBetweenUpgrades, () => {selfDmg = LinearUpgradeFormula(selfDmg,selfDmgUpgradeData);});
        DebugLogEnemyStats();
    }

    private float LinearUpgradeFormula(float baseVal, UpgradeData upgradeData)
    {
        return baseVal * upgradeData.MultiplierValue + upgradeData.AddendValue;
    }

    private int LinearUpgradeFormula(int baseVal, UpgradeData upgradeData)
    {
        return Mathf.FloorToInt(baseVal * upgradeData.MultiplierValue) + Mathf.CeilToInt(upgradeData.AddendValue);
    }

    private void DebugLogEnemyStats()
    {
        Debug.Log("Enemy has upgraded!");
        Debug.Log("swimSpeed: "         + swimSpeed         );
        Debug.Log("attackDamage: "      + attackDamage      );
        Debug.Log("delayBetweenSwims: " + delayBetweenSwims );
        Debug.Log("turnSpeed: "         + turnSpeed         );
        Debug.Log("expDrop: "           + expDrop           );
        Debug.Log("maxHp: "             + maxHp             );
        Debug.Log("selfDmg: "           + selfDmg           );
    }
}
