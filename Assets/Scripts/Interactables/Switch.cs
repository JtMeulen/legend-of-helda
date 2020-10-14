using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] Door doorToOpen;
    [SerializeField] BoolValue storedValue;

    private SpriteRenderer mySprite;
    private bool activated;

    private void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();

        if (storedValue)
        {
            activated = storedValue.RuntimeValue;
            if (activated)
            {
                ActivateSwitch();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ActivateSwitch();
        }
    }

    private void ActivateSwitch()
    {
        if (storedValue)
        {
            storedValue.RuntimeValue = true;
        }

        activated = true;
        doorToOpen.OpenDoor();
        mySprite.sprite = activeSprite;
    }
}
