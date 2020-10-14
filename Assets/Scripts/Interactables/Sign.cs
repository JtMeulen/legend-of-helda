using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    [SerializeField] private GameObject dialogBoxElem;
    [SerializeField] private Text dialogTextElem;
    [TextArea(4, 100)] [SerializeField] private string dialogText;

    protected override void Interact()
    {
        if (dialogBoxElem.activeInHierarchy)
        {
            dialogBoxElem.SetActive(false);
        }
        else
        {
            dialogBoxElem.SetActive(true);
            dialogTextElem.text = dialogText;
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
}
