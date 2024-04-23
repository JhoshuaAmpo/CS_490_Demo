using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField]
    private GameObject upgradeScreen;
    [SerializeField]
    private HUDBarController expBar;

    private List<int> levelThresholds;
    public int Level {get;private set;} = 0;
    private int CurrentExp = 0;

    private void Awake() {
        levelThresholds = new();
        for(int i = 0; i < maxLevel; i++)
        {
            levelThresholds.Add((int)Math.Ceiling(Math.Pow(levelScale,i + initialLevelThresholdBoost)));
        }
        if(upgradeScreen == null) { 
            Debug.LogError("No upgrade screen");
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
        // Debug.Log($"CurExp: {CurrentExp} / lvlThresh: {levelThresholds[Level]} = {CurrentExp/levelThresholds[Level]}");
        // if(Level != 0) {
        //     expBar.SetBarWidth(CurrentExp/(float)levelThresholds[Level]);
        // }
        // else
        // {

        // }
        float barValue = (Level == 0) ? CurrentExp/(float)levelThresholds[Level] : (CurrentExp - levelThresholds[Level-1])/((float)levelThresholds[Level] - levelThresholds[Level-1]);
        expBar.SetBarWidth(barValue);
        if(CurrentExp >= levelThresholds[Level])
        {
            LevelUp();
        }
    }

    public void CheatLevelUp(){
        LevelUp();
    }

    private void LevelUp()
    {
        Level += 1;
        expBar.SetBarWidth(0f);
        GetComponent<PlayerWeaponHandler>().StopAllAttack();
        PauseGame.Instance.Pause();
        upgradeScreen.SetActive(true);
    }
}
