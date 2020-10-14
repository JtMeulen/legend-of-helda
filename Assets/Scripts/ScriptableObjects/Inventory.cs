using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    [SerializeField] public Item currentItem;
    [SerializeField] public List<Item> items = new List<Item>();
    [SerializeField] public int numberOfKeys;
    [SerializeField] public int coins;
    [SerializeField] public int arrows;

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

    public bool CheckForItem(Item item)
    {
        return items.Contains(item);
    }
}
