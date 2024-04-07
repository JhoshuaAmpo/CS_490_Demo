using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D),typeof(Timer))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    protected float SwimSpeed = 0f;
    [SerializeField]
    protected float delayBetweenSwims = 0f;
    [SerializeField]
    protected float AttackDamage;
    [SerializeField]
    protected float turnSpeed = 90f;
    [SerializeField]
    protected int expDrop = 1;

    public float HealthPoints = 0f;
    protected GameObject target;
    private Timer timer;
    private Vector2 dir;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
        if(HealthPoints <= 0f) { Debug.LogError("Enemy initialized with no HP"); }
        timer = GetComponent<Timer>();
        timer.SetTimer(delayBetweenSwims, () => {SwimTo(target);});
    }

    private void Start() {
        target = PlayerHandler.Instance.gameObject;
    }

    void Update(){
        if(PauseGame.Instance.isGamePaused) { return; }
        dir = (target.transform.position - transform.position).normalized;
        if(timer.IsTimerComplete())
        {
            timer.SetTimer(delayBetweenSwims, () => {SwimTo(target);});
        }
        // transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,dir.y,0),Vector3.forward);
        LookAt(target.transform.position);
    }

    private void OnParticleCollision(GameObject other) {
        // Debug.Log($"I, {this.gameObject.name}, have collided with {other.name}");
        DecreaseHealth(other.GetComponent<BaseWeapon>().BaseDamage * other.GetComponent<BaseWeapon>().WeaponMultiplier);
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
        Debug.Log($"{this.name} died");
        
        ExpSpawner.Instance.SpawnExp(expDrop, transform.position);
        gameObject.SetActive(false);
    }


    private void SwimTo(GameObject t)
    {
        // Debug.Log($"Target: {t.name} is at {t.transform.position}\nSwimming towards: {dir}");
        // Debug.Log($"Angle between Direction: {dir} and Cur Velocity: {rb.velocity} is {angle}");
        Vector2 newVel = rb.velocity;
        if(rb.velocity.x * dir.x < 0) {newVel.x /= 2;}
        if(rb.velocity.y * dir.y < 0) {newVel.y /= 2;}
        rb.velocity = newVel;
        rb.AddForce(dir * SwimSpeed,ForceMode2D.Force);
    }

    protected void LookAt(Vector2 point){
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        var targetRotation = Quaternion.Euler (new Vector3(0f,0f,angle));
        // transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        if(rb != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.transform.position + dir);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(this.rb.velocity.x,this.rb.velocity.y,0));
        }
    }
}
