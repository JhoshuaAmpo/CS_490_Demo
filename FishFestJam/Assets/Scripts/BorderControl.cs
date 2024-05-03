using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class BorderControl : MonoBehaviour
{
    public static BorderControl Instance { get; private set;}

    [TitleGroup("Virtual Camera Settings")]
    [ReadOnly]
    public float orthoSize;

    [MinMaxSlider(0,100, true)]
    [LabelText("Orthographic Size Range")]
    public Vector2 orthoSizeRange = new();

    [MinValue(0)]
    [LabelText("Time to reach max size in seconds")]
    [LabelWidth(200)]
    public int orthoSizeDuration;

    [EnumToggleButtons]
    public ScalingOptions SizeScale;

    public enum ScalingOptions { linear, smoothDamp, exponential }

    [MinValue(1f)]
    [TitleGroup("Exponential Scale Settings",VisibleIf = "@SizeScale == ScalingOptions.exponential")]
    public float exponentialBaseValue;

    [TitleGroup("Debug Info")]
    [ReadOnly]
    public float width = 0;
    [ReadOnly]
    public float height = 0;
    
    BoxCollider2D bc;
    CinemachineVirtualCamera virtualCamera;
    float linearSizeDelta;
    float smoothDampSizeDelta;
    float exponentialSizeDelta;
    float exponentialCurTime = 0;
    float smoothDampVel = 1;
    Vector3 ls;
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
        bc = GetComponent<BoxCollider2D>();
        ls = transform.localScale;

        width = bc.size.x * ls.x;
        height = bc.size.y * ls.y;

        orthoSize = orthoSizeRange.x;
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.OrthographicSize = orthoSize;

        SetBoxSizeToOrthoSize();
    }

    private void Update() {
        UpdateVirtualCameraOrthoSize();
        MatchLocalScaleToOrthoSize();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnPlayerExit(other);
    }

    public bool IsOutsideBorder(Vector2 pos){
        if (pos.x >= -width/2 && pos.x <= width/2 && pos.y <= height/2 && pos.y >= -height/2)
        {
            return false;
        }
        return true;
    }

    private void OnPlayerExit(Collider2D other)
    {
        if (!other.CompareTag("Player")) { return; }
        Vector3 playerPos = other.transform.position;
        Vector3 ls = transform.localScale;
        if (playerPos.x < -width / 2) { playerPos.x += width; }
        if (playerPos.x > width / 2) { playerPos.x -= width; }
        if (playerPos.y < -height / 2) { playerPos.y += height; }
        if (playerPos.y > height / 2) { playerPos.y -= height; }
        other.transform.position = playerPos;
    }

    private void UpdateVirtualCameraOrthoSize(){
        if (orthoSize >= orthoSizeRange.y) { return; }
        if (orthoSizeDuration == 0) {
            virtualCamera.m_Lens.OrthographicSize = orthoSizeRange.y;
            return;
        }
        if(SizeScale == ScalingOptions.linear)
        {
            linearSizeDelta = Time.deltaTime * (orthoSizeRange.y - orthoSizeRange.x) / orthoSizeDuration;
            orthoSize += linearSizeDelta;
        }
        if(SizeScale == ScalingOptions.smoothDamp)
        {
            smoothDampSizeDelta = Time.deltaTime * Mathf.SmoothDamp(orthoSize, orthoSizeRange.y, ref smoothDampVel, 1) / orthoSizeDuration;
            orthoSize += smoothDampSizeDelta;
        }
        if (SizeScale == ScalingOptions.exponential)
        {
            Debug.LogError("Exponential Size Scale doesn't work");
            // exponentialCurTime += Time.deltaTime;
            // orthoSize = Mathf.Pow(exponentialBaseValue, exponentialCurTime) / orthoSizeDuration + orthoSizeRange.x; 
        }
        Mathf.Clamp(orthoSize, orthoSizeRange.x, orthoSizeRange.y);
        virtualCamera.m_Lens.OrthographicSize = orthoSize;
    }

    private void SetBoxSizeToOrthoSize()
    {
        bc.size = new Vector2(Camera.main.aspect * 2 * orthoSize , 2 * orthoSize);
    }

    private void MatchLocalScaleToOrthoSize(){
        float OSRRatio = orthoSizeRange.y / orthoSizeRange.x;
        float scale = orthoSize / orthoSizeRange.y * OSRRatio;
        Mathf.Clamp(scale, 1, OSRRatio);
        ls = new (scale, scale, 1);
        transform.localScale = ls;
        width = bc.size.x * ls.x;
        height = bc.size.y * ls.y;
    }
}
