using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D),typeof(Timer))]
public abstract class EnemyBehavior : MonoBehaviour
{
    // [SerializeField]
    // protected float swimSpeed = 0f;
    [Header("Base Stats")]
    [SerializeField]
    protected EnemyStat swimSpeed;
    [SerializeField]
    protected EnemyStat delayBetweenSwims;
    [SerializeField]
    protected EnemyStat attackDamage;
    [SerializeField]
    protected EnemyStat turnSpeed;
    [SerializeField]
    protected EnemyStat expDrop;
    [SerializeField]
    protected EnemyStat maxHp;
    protected GameObject target;
    private float HealthPoints;
    private Timer swimTimer;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        HealthPoints = maxHp.Value;
        swimTimer = GetComponent<Timer>();
        swimTimer.SetTimer(delayBetweenSwims.Value, () => {SwimPattern(target);});

        swimSpeed        .Initialize("swimSpeed", gameObject.AddComponent<Timer>());
        delayBetweenSwims.Initialize("delayBetweenSwims", gameObject.AddComponent<Timer>());
        attackDamage     .Initialize("attackDamage", gameObject.AddComponent<Timer>());
        turnSpeed        .Initialize("turnSpeed", gameObject.AddComponent<Timer>());
        maxHp            .Initialize("maxHP", gameObject.AddComponent<Timer>());
        expDrop          .Initialize("expDrop", gameObject.AddComponent<Timer>());
    }

    protected virtual void Start() {
        target = PlayerHandler.Instance.gameObject;
    }

    protected virtual void Update()
    {
        if (PauseGame.Instance.isGamePaused) { return; }
        Move();
        LookAt(target.transform.position);
        UpgradeAllStats();
    }
    protected virtual void OnParticleCollision(GameObject other) {
        // Debug.Log($"I, {this.gameObject.name}, have collided with {other.name}");
        DecreaseHealth(other.GetComponent<BaseWeapon>().BaseDamage * other.GetComponent<BaseWeapon>().WeaponMultiplier);
    }

    public void DecreaseHealth(float dmg)
    {
        HealthPoints -= dmg;
        if(HealthPoints <= 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        ExpSpawner.Instance.SpawnExp((int)expDrop.Value, transform.position);
        gameObject.SetActive(false);
    }
    protected void Move()
    {
        swimTimer.SetTimer(delayBetweenSwims.Value, () => { SwimPattern(target); });
    }
    protected virtual void SwimPattern(GameObject t) { return; }

    protected void LookAt(Vector2 point){
        Vector2 dir = (target.transform.position - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        var targetRotation = Quaternion.Euler (new Vector3(0f,0f,angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed.Value * Time.deltaTime);
    }

    protected virtual void UpgradeAllStats(){
        swimSpeed        .statTimer.SetTimer(swimSpeed.TimeBetweenUpgrades, () => {swimSpeed.UpgradeStat();});      
        delayBetweenSwims.statTimer.SetTimer(delayBetweenSwims.TimeBetweenUpgrades, () => {delayBetweenSwims.UpgradeStat();});  
        attackDamage     .statTimer.SetTimer(attackDamage.TimeBetweenUpgrades, () => {attackDamage.UpgradeStat();});  
        turnSpeed        .statTimer.SetTimer(turnSpeed.TimeBetweenUpgrades, () => {turnSpeed.UpgradeStat();});  
        maxHp            .statTimer.SetTimer(maxHp.TimeBetweenUpgrades, () => {maxHp.UpgradeStat();});
        expDrop          .statTimer.SetTimer(expDrop.TimeBetweenUpgrades, () => {expDrop.UpgradeStat();});
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
