using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Collectable
{
    [SerializeField] Inventory inventory;
    [SerializeField] Item bowItem;

    private void Awake()
    {
        // dont drop if the bow is not found
        if(!inventory.CheckForItem(bowItem))
        {
            Destroy(this.gameObject);
        }
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (inventory.arrows < 10)
            {
                inventory.arrows++;
            }

            base.OnTriggerEnter2D(other);
        }
    }
}
