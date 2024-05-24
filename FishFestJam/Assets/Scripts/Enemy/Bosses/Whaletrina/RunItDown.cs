using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunItDown: MonoBehaviour, IEnemyAbilities
{
    [SerializeField]
    private RunItDownPattern runItDownPattern;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float marginOfError;
    public string Name { get; set;}
    public bool IsRunning { get; private set; } 
    private List<Vector3> path;
    private Transform baseTransform;
    private Vector3 moveDir;

    private void Start() {
        path = new List<Vector3>();
        path = runItDownPattern.Path;
        baseTransform = GetComponentsInParent<Transform>()[^1];
        IsRunning = false;
    }

    public void UseAbility(){
        baseTransform.position = path[0];
        IsRunning = true;
        // Moves out of screen
        StartCoroutine(ProcessAbility(0, 1));
    }

    private IEnumerator ProcessAbility(int start, int end){
        while(Vector3.Distance(baseTransform.position, path[end]) >= marginOfError)
        {
            moveDir = (path[end] - baseTransform.position).normalized;
            Vector3 moveVel = moveSpeed * Time.deltaTime * moveDir;
            baseTransform.Translate(moveVel,Space.World);
            baseTransform.up = -(path[end] - baseTransform.position);
            yield return new WaitForEndOfFrame();
        }
        if (end+1 < path.Count) {
            StartCoroutine(ProcessAbility(start+1,end+1));
        }
        else {
            IsRunning = false;
            baseTransform.position = new(0,20,0);
        }
    }
}
