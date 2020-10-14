using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    [SerializeField] private Item content;
    [SerializeField] private Inventory inventory;
    [SerializeField] BoolValue storeOpenedValue;

    [Header("Signals and Dailog")]
    [SerializeField] SignalSender recieveItemSignal;
    [SerializeField] private GameObject dialogBoxElem;
    [SerializeField] private Text dialogTextElem;

    public SoundManager soundManager;
    private Animator myAnimator;
    private bool isOpened;

    private void Start()
    {
        isOpened = storeOpenedValue.RuntimeValue;
        soundManager = FindObjectOfType<SoundManager>();
        myAnimator = GetComponent<Animator>();
        if (isOpened)
        {
            myAnimator.SetBool("open", true);
        }
    }

    protected override void Interact()
    {
        if (!isOpened)
        {
            OpenChest();
        }
        else
        {
            EndInteractionWithChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        dialogBoxElem.SetActive(true);
        dialogTextElem.text = content.itemDescription;

        myAnimator.SetBool("open", true);
        soundManager.PlaySound("OpenChest");

        inventory.AddItem(content);
        inventory.currentItem = content; // used to show the correct item sprite

        recieveItemSignal.Raise(); // start recieveItem Anim 
        contextSignal.Raise(); // removing the ?
        storeOpenedValue.RuntimeValue = isOpened;
    }

    private void EndInteractionWithChest()
    {
        dialogBoxElem.SetActive(false);
        recieveItemSignal.Raise();
        playerInRange = false;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            contextSignal.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpened)
        {
            playerInRange = false;
            contextSignal.Raise();
        }
    }
}
