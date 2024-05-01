using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D),typeof(Timer))]
public abstract class EnemyBehavior : MonoBehaviour
{
    public EnemyStatSO enemyStatSO;
    protected EnemyStat swimSpeed;
    protected EnemyStat delayBetweenSwims;
    protected EnemyStat attackDamage;
    protected EnemyStat turnSpeed;
    protected EnemyStat expDrop;
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

        swimSpeed = enemyStatSO.SwimSpeed;
        delayBetweenSwims = enemyStatSO.DelayBetweenSwims;
        attackDamage = enemyStatSO.AttackDamage;
        turnSpeed = enemyStatSO.TurnSpeed;
        maxHp = enemyStatSO.MaxHp;
        expDrop = enemyStatSO.ExpDrop;

        InitializeAllStats();

        swimTimer = GetComponent<Timer>();
        swimTimer.SetTimer(delayBetweenSwims.Value, () => { SwimPattern(target); });
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

    protected virtual void InitializeAllStats()
    {
        InitializeStat(swimSpeed, "swimSpeed");
        InitializeStat(delayBetweenSwims, "delayBetweenSwims");
        InitializeStat(attackDamage, "attackDamage");
        InitializeStat(turnSpeed, "turnSpeed");
        InitializeStat(maxHp, "maxHP");
        InitializeStat(expDrop, "expDrop");
    }

    protected void InitializeStat(EnemyStat es, string name)
    {
        es.Initialize(name, gameObject.AddComponent<Timer>());
    }

    protected virtual void UpgradeAllStats(){
        UpgradeStat(swimSpeed        );
        UpgradeStat(delayBetweenSwims);
        UpgradeStat(attackDamage     );
        UpgradeStat(turnSpeed        );
        UpgradeStat(maxHp            );
        UpgradeStat(expDrop          );
    }

    protected void UpgradeStat(EnemyStat es)
    {
        es.statTimer.SetTimer(es.TimeBetweenUpgrades, () => {es.UpgradeStat();});
    }

    private void Death()
    {
        ExpSpawner.Instance.SpawnExp((int)expDrop.Value, transform.position);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        if(rb != null)
        {
            GameObject t = PlayerHandler.Instance.gameObject;
            Vector3 dir = (t.transform.position - transform.position).normalized;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.transform.position + dir);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(this.rb.velocity.x,this.rb.velocity.y,0));
        }
    }
}
