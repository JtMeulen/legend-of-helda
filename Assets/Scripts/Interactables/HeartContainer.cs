using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Collectable
{
    [SerializeField] FloatValue currentHealth;
    [SerializeField] FloatValue healthContainers;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // increase heart container
            healthContainers.RuntimeValue += 1;
            // give full health
            currentHealth.RuntimeValue = healthContainers.RuntimeValue * 4;

            base.OnTriggerEnter2D(other);
        }
    }
}
