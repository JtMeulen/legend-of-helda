using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private FloatValue damageValue;

    private float damage;

    private void Start()
    {
        damage = damageValue.initialValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<BreakableObject>().BreakObject();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            // Dont let enemies kill each other
            if (other.gameObject.CompareTag("enemy") && gameObject.CompareTag("enemy")) { return; };

            Rigidbody2D target = other.GetComponent<Rigidbody2D>();
            if (target != null)
            {
                Vector2 forceDirection = target.transform.position - transform.position;
                forceDirection = forceDirection.normalized * thrust;
                target.AddForce(forceDirection, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy"))
                {
                    EnemyController enemy = target.GetComponent<EnemyController>();
                    enemy.currentState = EnemyState.stagger;
                    enemy.StartKnockbackCo(target, knockTime, damage);
                }

                if(other.gameObject.CompareTag("Player"))
                {
                    PlayerMovement player = target.GetComponent<PlayerMovement>();
                    if(player.currentState != PlayerState.stagger)
                    {
                        player.currentState = PlayerState.stagger;
                        player.StartKnockbackCo(knockTime, damage);
                    }
                }

            }
        }
    }
}
