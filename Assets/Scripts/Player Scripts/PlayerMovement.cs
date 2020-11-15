using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed = 1f;
    [SerializeField] FloatValue currentHealth;
    [SerializeField] SignalSender healthSignalSender;
    [SerializeField] Vector2Value startingPosition;

    [Header("Player Hit")]
    [SerializeField] private float flashDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private Collider2D myTriggerCollider;

    [Header("Items and inventory")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private SpriteRenderer recievedItemSprite;
    [SerializeField] private GameObject arrow;
    [SerializeField] SignalSender shootArrowSignal;
    [SerializeField] Item swordItem;
    [SerializeField] Item bowItem;

    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    public Animator myAnimator;
    private SpriteRenderer mySprite;
    private Vector3 movementChange;
    private SoundManager soundManager;

    private void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>();
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);
        transform.position = startingPosition.RuntimeValue;
    }

    private void Update()
    {
        if(currentState == PlayerState.interact)
        {
            return;
        }
        movementChange = Vector3.zero;
        movementChange.x = Input.GetAxisRaw("Horizontal");
        movementChange.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canAttack() )
        {
            if(inventory.CheckForItem(swordItem))
            {
                StartCoroutine(AttackCoroutine());
            }
        }
        else if(Input.GetKeyDown(KeyCode.Q) && canAttack() && inventory.arrows > 0)
        {
            if (inventory.CheckForItem(bowItem))
            {
                StartCoroutine(ShootCoroutine());
            }
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (movementChange != Vector3.zero)
        {
            MoveCharacter();
            myAnimator.SetBool("isWalking", true);
            myAnimator.SetFloat("moveX", movementChange.x);
            myAnimator.SetFloat("moveY", movementChange.y);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }

    private bool canAttack()
    {
        return currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.interact;
    }

    private void MoveCharacter()
    {
        movementChange.Normalize();
        myRigidbody.MovePosition(
              transform.position + movementChange * speed * Time.fixedDeltaTime
        );
    }

    private IEnumerator AttackCoroutine()
    {
        currentState = PlayerState.attack;
        myAnimator.SetTrigger("isAttacking");
        soundManager.PlaySound("SlashSword");
        soundManager.PlaySoundRandom(new string[4] { "PlayerAttackVoice1", "PlayerAttackVoice2", "PlayerAttackVoice3", "PlayerAttackVoice4" });
        yield return new WaitForSeconds(0.2f);
        currentState = PlayerState.walk;
    }

    private IEnumerator ShootCoroutine()
    {
        currentState = PlayerState.attack;
        inventory.arrows--;
        shootArrowSignal.Raise();
        myAnimator.SetTrigger("isShooting");
        yield return new WaitForSeconds(0.1f);

        GameObject arrowProjectile = Instantiate(arrow, transform.position, Quaternion.identity);
        arrowProjectile.GetComponent<Projectile>().Launch(
            new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY")),
            // calculate the rotation for the arrow direction
            new Vector3(0,0, Mathf.Atan2(myAnimator.GetFloat("moveY"), myAnimator.GetFloat("moveX"))*Mathf.Rad2Deg)
        );
        soundManager.PlaySound("ShootBow");

        yield return new WaitForSeconds(0.1f);
        currentState = PlayerState.walk;
    }

    public void StartKnockbackCo(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        soundManager.PlaySound("PlayerHit");
        healthSignalSender.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            soundManager.PlaySoundRandom(new string[3] { "PlayerHitVoice1", "PlayerHitVoice2", "PlayerHitVoice3" });
            StartCoroutine(KnockCoroutine(knockTime));
        }
        else
        {
            // TODO: soundManager.PlaySound("PlayerDeath")
            // TODO: animate playerDeath
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCoroutine(float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        myTriggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        myTriggerCollider.enabled = true;
    }


    public void RecieveItem()
    {
        if(inventory.currentItem != null)
        {
            if(currentState != PlayerState.interact) {
                myAnimator.SetBool("recieveItem", true);
                currentState = PlayerState.interact;
                recievedItemSprite.sprite = inventory.currentItem.itemSprite;
            }
            else
            {
                myAnimator.SetBool("recieveItem", false);
                currentState = PlayerState.idle;
                recievedItemSprite.sprite = null;
                inventory.currentItem = null;
            }
        }
    }

    // used in the walk animation
    public void PlayFootStepSound()
    {
        soundManager.PlayFootStep();
    }
}
