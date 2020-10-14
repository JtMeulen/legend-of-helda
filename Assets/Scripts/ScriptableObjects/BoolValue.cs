using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    [SerializeField] public bool RuntimeValue;

    [SerializeField] public bool initialValue;
}
