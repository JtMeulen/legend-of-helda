using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Collectable item;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [SerializeField] Loot[] lootTable;

    public void InstantiateLoot(Vector3 location)
    {
        for (int i = 0; i < lootTable.Length; i++)
        {
            if (Random.Range(0, 100) <= lootTable[i].lootChance)
            {
                location.x += Random.Range(-.3f, .3f);
                location.y += Random.Range(-.3f, .3f);
                Instantiate(lootTable[i].item, location, Quaternion.identity);
            }
        }
    }
}
