using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Vector2Value : ScriptableObject
{
    [SerializeField] public Vector2 RuntimeValue;

    [SerializeField] public Vector2 initialValue;
}
