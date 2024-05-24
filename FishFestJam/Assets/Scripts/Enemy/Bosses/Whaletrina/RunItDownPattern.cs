using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RunItDownPattern : MonoBehaviour
{
    public List<Vector3> Path {get; private set;}
    public bool ShowPathInPlayMode = false;
    private List<Transform> pathMarkers;
    private LineRenderer lineRenderer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        SetPathMarkers();
        Path = new();
        foreach(Transform t in transform) {
            t.GetComponent<SpriteRenderer>().enabled = true;
        }
        lineRenderer.widthMultiplier = 1;
    }

    private void Start(){
        if(!ShowPathInPlayMode && Application.IsPlaying(this)){
            foreach(Transform t in transform) {
                t.GetComponent<SpriteRenderer>().enabled = false;
            }
            lineRenderer.widthMultiplier = 0;
        }
    }

    private void Update() {
        UpdatePath();
    }
    private void UpdatePath()
    {
        SetPathMarkers();
        lineRenderer = lineRenderer != null ? lineRenderer : GetComponent<LineRenderer>();
        Path ??= new();
        Path.Clear();
        lineRenderer.positionCount = pathMarkers.Count;
        foreach (Transform t in pathMarkers)
        {
            Path.Add(t.position);
        }
        lineRenderer.SetPositions(Path.ToArray());
    }

    private void SetPathMarkers()
    {
        if(pathMarkers != null) { return; }
        pathMarkers = new();
        GetComponentsInChildren<Transform>(pathMarkers);
        pathMarkers.RemoveAt(0);
    }
}
