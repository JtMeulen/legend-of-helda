using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public string itemDescription;
    [SerializeField] public bool isKey;
}
