using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    [SerializeField] public float RuntimeValue;

    [SerializeField] public float initialValue;

}
