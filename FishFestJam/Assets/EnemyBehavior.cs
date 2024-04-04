using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float HealthPoints = 0f;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        if(HealthPoints <= 0f)
        {
            Debug.LogError("Enemy initialized with no HP");
        }
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log($"I, {this.gameObject.name}, have collided with {other.name}");
        DecreaseHealth(other.GetComponent<BaseWeapon>().BaseDamage);
    }

    public void DecreaseHealth(float dmg)
    {
        HealthPoints -= dmg;
        Debug.Log($"{this.name} hp: {HealthPoints}");
        if(HealthPoints <= 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("I died");
        Destroy(this);
    }
}
