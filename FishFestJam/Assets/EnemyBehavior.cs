using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log($"I, {this.gameObject.name}, have collided with {other.gameObject.name}");
    }
}
