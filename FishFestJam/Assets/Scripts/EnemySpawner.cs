using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler),typeof(Timer))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenSpawns = 1f;
    [SerializeField]
    private GameObject spawnPointsParent;
    private List<Transform> spawnPoints;
    private Timer timer;

    ObjectPooler objectPooler;
    private void Awake() {
        objectPooler = GetComponent<ObjectPooler>();
        List<Transform> tempList = new();
        spawnPointsParent.GetComponentsInChildren<Transform>(tempList);
        spawnPoints = tempList;
        spawnPoints.RemoveAt(0);
        foreach (var sp in spawnPoints)
        {
            sp.GetComponent<SpriteRenderer>().enabled = false;
        }
        timer = GetComponent<Timer>();
        timer.SetTimer(timeBetweenSpawns, () => {SpawnEnemy();});
    }
    void FixedUpdate()
    {
        if(timer.IsTimerComplete())
        {
            timer.SetTimer(timeBetweenSpawns, () => {SpawnEnemy();});
        }
    }

    void SpawnEnemy(){
        GameObject enemy = objectPooler.GetPooledObject(); 
        if (enemy != null) {
            enemy.SetActive(true);
            int randNum = Random.Range(0,spawnPoints.Count);
            enemy.transform.position = spawnPoints[randNum].position;
            // Debug.Log($"{enemy.name} has spawned on point {randNum}");
        }
    }
}
