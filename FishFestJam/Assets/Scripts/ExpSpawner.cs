using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class ExpSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public static ExpSpawner Instance { get; private set;}
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
        objectPooler = GetComponent<ObjectPooler>();
    }

    public void SpawnExp(int expDrop, Vector3 spawnPos){
        GameObject exp = objectPooler.GetPooledObject(); 
        if (exp == null) { return; }
        exp.SetActive(true);
        exp.transform.position = spawnPos;
        exp.GetComponent<ExpPointBehavior>().SetExpValue(expDrop);
    }
}
