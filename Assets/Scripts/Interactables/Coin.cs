using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] Inventory inventory;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if(inventory.coins < 999)
            {
                inventory.coins++;
            }

            base.OnTriggerEnter2D(other);
        }
    }
}
