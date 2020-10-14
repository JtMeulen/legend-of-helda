using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] LootTable lootTable;
    [SerializeField] string soundToPlay;
    private Animator myAnimator;
    private BoxCollider2D myBoxCollider;
    private SoundManager soundManager;

    private void OnEnable()
    {
        SetupComponents();
        myBoxCollider.enabled = true;
        myAnimator.SetBool("onBreak", false);
    }

    private void Start()
    {
        SetupComponents();
    }

    private void SetupComponents()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void BreakObject()
    {
        myAnimator.SetBool("onBreak", true);
        soundManager.PlaySound(soundToPlay);
        if (lootTable != null)
        {
            lootTable.InstantiateLoot(transform.position);
        }
        myBoxCollider.enabled = false;
    }

}
