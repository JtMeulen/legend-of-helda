using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    [SerializeField] FloatValue currentHealth;
    [SerializeField] FloatValue healthContainers;
    [SerializeField] float healthIncrease;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            currentHealth.RuntimeValue += healthIncrease;
            if(currentHealth.RuntimeValue > healthContainers.RuntimeValue * 4)
            {
                currentHealth.RuntimeValue = healthContainers.RuntimeValue * 4;
            }

            base.OnTriggerEnter2D(other);
        }
    }
}
