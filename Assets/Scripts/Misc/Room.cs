using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private GameObject virtualCamera;

    [Header("Room Name")]
    [SerializeField] private bool showRoomName;
    [SerializeField] private string roomName;
    [SerializeField] private GameObject textElement;
    [SerializeField] private string songName;

    [Header("Reset values")]
    [SerializeField] public EnemyController[] enemies;
    [SerializeField] public BreakableObject[] breakAbles;

    private Coroutine roomNameCoroutine;
    private SoundManager soundManager;

    protected virtual void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for(int i = 0; i < breakAbles.Length; i++)
            {
                ChangeActivation(breakAbles[i], true);
            }
            virtualCamera.SetActive(true);
            ShowRoomName();
            if(songName != null)
            {
                soundManager.PlayMusic(songName);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < breakAbles.Length; i++)
            {
                ChangeActivation(breakAbles[i], false);
            }
            virtualCamera.SetActive(false);
        }
    }

    private void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

    private void ShowRoomName()
    {
        if (showRoomName && textElement)
        {
            if (roomNameCoroutine != null)
            {
                textElement.SetActive(false);
                StopCoroutine(roomNameCoroutine);
            };
            roomNameCoroutine = StartCoroutine(ShowRoomNameCoroutine());
        }
    }

    private IEnumerator ShowRoomNameCoroutine()
    {
        textElement.SetActive(true);
        textElement.GetComponent<Text>().text = roomName;
        yield return new WaitForSeconds(3f);
        textElement.GetComponent<Text>().CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(1f);
        textElement.SetActive(false);
    }
}
