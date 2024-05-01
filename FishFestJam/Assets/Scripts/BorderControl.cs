using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class BorderControl : MonoBehaviour
{
    public static BorderControl Instance { get; private set;}

    [MinMaxSlider(0,100, true)]
    [TitleGroup("Virtual Camera Settings")]
    [LabelText("Orthographic Size Range")]
    public Vector2 orthoSizeRange = new();

    [MinValue(0)]
    [LabelText("Time to reach max size in seconds")]
    [LabelWidth(200)]
    public int orthoSizeDuration;

    [EnumToggleButtons]
    public ScalingOptions SizeScale;

    public enum ScalingOptions { linear, exponential }

    float width = 0;
    float height = 0;
    BoxCollider2D bc;
    CinemachineVirtualCamera virtualCamera;
    float orthoSize;
    float linearSizeDelta;
    float exponentialSizeDelta;
    float smoothDampVel = 1;
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;
        bc = GetComponent<BoxCollider2D>();
        width = bc.size.x;
        height = bc.size.y;

        orthoSize = orthoSizeRange.x;
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.OrthographicSize = orthoSize;
    }

    private void Update() {
        UpdateVirtualCameraOrthoSize();
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
        if (playerPos.x < -width / 2) { playerPos.x += width; }
        if (playerPos.x > width / 2) { playerPos.x -= width; }
        if (playerPos.y < -height / 2) { playerPos.y += height; }
        if (playerPos.y > height / 2) { playerPos.y -= height; }
        other.transform.position = playerPos;
    }

    private void UpdateVirtualCameraOrthoSize(){
        if (orthoSize >= orthoSizeRange.y) { return; }
        if (orthoSizeDuration == 0) {
            virtualCamera.m_Lens.OrthographicSize = 0;
            return;
        }
        if(SizeScale == ScalingOptions.linear)
        {
            linearSizeDelta = Time.deltaTime * (orthoSizeRange.y - orthoSizeRange.x) / orthoSizeDuration;
            orthoSize += linearSizeDelta;
        }
        if(SizeScale == ScalingOptions.exponential)
        {
            exponentialSizeDelta = Time.deltaTime * Mathf.SmoothDamp(orthoSize, orthoSizeRange.y, ref smoothDampVel, 1) / orthoSizeDuration;
            orthoSize += exponentialSizeDelta;
        }
        Mathf.Clamp(orthoSize, orthoSizeRange.x, orthoSizeRange.y);
        virtualCamera.m_Lens.OrthographicSize = orthoSize;
    }
}
