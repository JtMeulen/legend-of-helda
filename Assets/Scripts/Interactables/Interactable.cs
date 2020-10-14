using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public SignalSender contextSignal;
    public bool playerInRange = false;

    protected abstract void Interact();

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            Interact();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            contextSignal.Raise();
            playerInRange = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
            contextSignal.Raise();
        }
    }
}
