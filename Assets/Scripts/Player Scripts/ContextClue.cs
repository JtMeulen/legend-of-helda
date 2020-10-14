using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    [SerializeField] private GameObject contextClue;
    private bool contextClueActive = false;

    public void ChangeContext()
    {
        contextClueActive = !contextClueActive;
        contextClue.SetActive(contextClueActive);
    }
}
