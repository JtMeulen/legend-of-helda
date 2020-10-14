using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCountManager : MonoBehaviour
{
    [SerializeField] Image[] arrows;
    [SerializeField] Inventory inventory;
    [SerializeField] Item bowItem;

    private void Start()
    {
        ToggleArrowsUIElement();
        UpdateArrowDisplay();
    }

    public void ToggleArrowsUIElement()
    {
        GetComponent<CanvasGroup>().alpha = inventory.CheckForItem(bowItem) ? 1f : 0f;
    }

    public void UpdateArrowDisplay()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (i < inventory.arrows)
            {
                arrows[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                arrows[i].color = new Color(1f, 1f, 1f, .4f);
            }
        }
    }
    
}
