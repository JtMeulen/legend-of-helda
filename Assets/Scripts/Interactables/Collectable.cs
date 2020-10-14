using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public SignalSender signalSender;
    [SerializeField] public bool hasDespawn;
    [SerializeField] private string soundToPlay;
    private SpriteRenderer mySprite;
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        mySprite = GetComponent<SpriteRenderer>();
        if (hasDespawn)
        {
            StartCoroutine(DestroyLootCoroutine());
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            soundManager.PlaySound(soundToPlay);
            signalSender.Raise();
            Destroy(this.gameObject);
        }
    }

    // a little flashing before they are destroyed
    private IEnumerator DestroyLootCoroutine()
    {
        yield return new WaitForSeconds(4f);
        mySprite.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(.1f);
        mySprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(.1f);
        mySprite.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(.1f);
        mySprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(.1f);
        Destroy(this.gameObject);
    }
}
