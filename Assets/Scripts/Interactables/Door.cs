using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    [SerializeField] private DoorType doorType;
    [SerializeField] private SpriteRenderer doorSprite;
    [SerializeField] private BoxCollider2D myBoxCollider;
    [SerializeField] private BoxCollider2D myTriggerArea;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private BoolValue storedValue;

    [Header("Dialog Text")]
    [SerializeField] private GameObject dialogBoxElem;
    [SerializeField] private Text dialogTextElem;
    [TextArea(4, 100)] [SerializeField] private string dialogText;

    private bool open;

    private void Start()
    {
        if (storedValue != null)
        {
            open = storedValue.RuntimeValue;
            if (open)
            {
                OpenDoor();
            }
        }

        if(doorType == DoorType.enemy)
        {
            OpenDoor();
        }
    }

    protected override void Interact()
    {
        if (dialogBoxElem.activeInHierarchy)
        {
            dialogBoxElem.SetActive(false);
        }
        else if (doorType == DoorType.key)
        {
            HandleKeyDoor();
        }
        else if (doorType == DoorType.button || doorType == DoorType.enemy)
        {
            ShowText();
        }
    }

    public void HandleKeyDoor()
    {
        if (playerInventory.numberOfKeys > 0)
        {
            playerInventory.numberOfKeys--;
            OpenDoor();
        }
        else
        {
            ShowText();
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogBoxElem.SetActive(false);

            base.OnTriggerExit2D(other);
        }
    }

    public void OpenDoor()
    {
        doorSprite.enabled = false;
        myBoxCollider.enabled = false;
        myTriggerArea.enabled = false;
        open = true;

        if (storedValue != null)
        {
            storedValue.RuntimeValue = true;
        }
    }

    public void CloseDoor()
    {
        doorSprite.enabled = true;
        myBoxCollider.enabled = true;
        myTriggerArea.enabled = true;
        open = false;

        if (storedValue != null)
        {
            storedValue.RuntimeValue = false;
        }
    }

    private void ShowText()
    {
        dialogBoxElem.SetActive(true);
        dialogTextElem.text = dialogText;
    }
}
