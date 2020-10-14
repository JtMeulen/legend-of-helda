using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class EnemyController : MonoBehaviour
{
    [Header("Stats and Effects")]
    [SerializeField] private string enemyName;
    [SerializeField] private FloatValue maxHealth;
    [SerializeField] private int baseAttack;
    [SerializeField] public float moveSpeed;
    [SerializeField] GameObject deathEffect;
    [SerializeField] LootTable lootTable;

    [Header("Chase and Attack")]
    [SerializeField] public float chaseRadius;
    [SerializeField] public float attackRadius;
    [SerializeField] public Collider2D enemyBoundary;

    [Header("Signals Senders")]
    [SerializeField] private SignalSender enemyDeathSignal;

    [HideInInspector] public Vector2 homePosition;
    [HideInInspector] public EnemyState currentState;
    [HideInInspector] public Rigidbody2D myRigidbody;
    [HideInInspector] public Animator myAnimator;
    [HideInInspector] public Transform target;
    [HideInInspector] public float health;

    public SoundManager soundManager;

    private void Awake()
    {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();

        target = GameObject.FindWithTag("Player").transform;
        ChangeState(EnemyState.idle);
    }

    private void OnEnable()
    {
        health = maxHealth.initialValue;
        transform.position = homePosition;
        ChangeState(EnemyState.idle);
    }

    private void FixedUpdate()
    {
        CheckDistanceAndMove();
    }

    public virtual void CheckDistanceAndMove()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= chaseRadius && enemyBoundary.bounds.Contains(target.transform.position) && canWalkState())
        {
            if(distance > attackRadius)
            {
                StartChaseMode();
            }
            else
            {
                StartAttack();
            }
            
        }
        else if (distance > chaseRadius || !enemyBoundary.bounds.Contains(target.transform.position))
        {
            EndChaseMode();
        }
    }

    public virtual void StartChaseMode() {}
    public virtual void EndChaseMode() {}
    public virtual void StartAttack() {}

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void changeAnimDirection(Vector2 direction)
    {
        direction = direction.normalized;
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
    }

    private bool canWalkState()
    {
        return currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        soundManager.PlaySound("EnemyHit");
        if (health <= 0)
        {
            // TODO: soundManager.PlaySound("EnemyDeath");

            DeathEffect();
            CheckLootTable();
            enemyDeathSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }

    private void CheckLootTable()
    {
        if(lootTable != null)
        {
            lootTable.InstantiateLoot(transform.position);
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(effect, 0.6f);
        }
    }

    public void StartKnockbackCo(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        if (health > 0)
        {
            StartCoroutine(KnockCoroutine(myRigidbody, knockTime));
            TakeDamage(damage);
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D myRigidbody, float knockTime)
    {
        if(myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            ChangeState(EnemyState.idle);
        }
    }
}
