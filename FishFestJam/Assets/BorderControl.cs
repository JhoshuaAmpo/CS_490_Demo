using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : MonoBehaviour
{
    float width = 0;
    float height = 0;
    private void Awake() {
        this.GetComponent<SpriteRenderer>().enabled = false;
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
}
