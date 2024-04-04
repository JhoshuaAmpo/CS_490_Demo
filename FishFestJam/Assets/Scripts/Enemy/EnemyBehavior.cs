using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D),typeof(Timer))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    protected float SwimSpeed = 0f;
    [SerializeField]
    protected GameObject target;
    [SerializeField]
    protected float delayBetweenSwims = 0f;
    public float HealthPoints = 0f;
    private Timer timer;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        if(HealthPoints <= 0f)
        {
            Debug.LogError("Enemy initialized with no HP");
        }
        timer = GetComponent<Timer>();
        timer.SetTimer(delayBetweenSwims, () => {SwimTo(target);});
    }

    void Update(){
        if(timer.IsTimerComplete())
        {
            timer.SetTimer(delayBetweenSwims, () => {SwimTo(target);});
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

    private void SwimTo(GameObject t)
    {
        Vector2 dir = (t.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(dir,rb.velocity.normalized);
        Debug.Log($"Target: {t.name} is at {t.transform.position}\nSwimming towards: {dir}");
        Debug.Log($"Angle between Direction: {dir} and Cur Velocity: {rb.velocity} is {angle}");
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * SwimSpeed,ForceMode2D.Force);
    }

    private void OnDrawGizmos() {
        if(rb != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized * SwimSpeed;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.transform.position + dir);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(this.rb.velocity.x,this.rb.velocity.y,0));
        }
    }
}
