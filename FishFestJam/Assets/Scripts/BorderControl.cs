using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : MonoBehaviour
{
    public static BorderControl Instance { get; private set;}
    float width = 0;
    float height = 0;
    private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        }
        Instance = this;

        width = transform.localScale.x;
        height = transform.localScale.y;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) { return; }
        Vector3 playerPos = other.transform.position;
        if(playerPos.x < -width/2) { playerPos.x += width; }
        if(playerPos.x > width/2) { playerPos.x -= width; }
        if(playerPos.y < -height/2) { playerPos.y += height; }
        if(playerPos.y > height/2) { playerPos.y -= height; }
        other.transform.position = playerPos;
    }

    public bool IsOutsideBorder(Vector2 pos){
        if (pos.x >= -width/2 && pos.x <= width/2 && pos.y <= height/2 && pos.y >= -height/2)
        {
            return false;
        }
        return true;
    }
}
