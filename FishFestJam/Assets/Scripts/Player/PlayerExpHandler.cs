using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpHandler : MonoBehaviour
{
    [SerializeField]
    private int maxLevel = 1;
    [SerializeField]
    [Min(0f)]
    private float levelScale = 1;
    [SerializeField]
    private float initialLevelThresholdBoost = 1f;
    // At level X, this number is how many Exp they need to level up
    private List<int> levelThresholds;
    public int Level {get;private set;} = 0;
    public int CurrentExp {get;private set;} = 0;

    private void Awake() {
        levelThresholds = new();
        for(int i = 0; i < maxLevel; i++)
        {
            levelThresholds.Add((int)Math.Ceiling(Math.Pow(levelScale,i + initialLevelThresholdBoost)));
        }
    }

    private void Update()
    {
        for(int i = 0; i < maxLevel; i++)
        {
            levelThresholds[i] = (int)Math.Ceiling(Math.Pow(levelScale,i + initialLevelThresholdBoost));   
        }
    }

    public void GainExp(int exp){
        CurrentExp += exp;
        if(CurrentExp >= levelThresholds[Level])
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Debug.Log("Congrats players has leveled up!");
    }
}
