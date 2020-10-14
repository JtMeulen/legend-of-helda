using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private BoolValue storedSeenCutscene;

    // TODO: Check if this is needed to move the char to start position
    [SerializeField] private Vector2 cutSceneStartPosition;

    private PlayableDirector director;
    private bool hasStartedPlaying;
    private PlayerMovement playerMovement;

    private void Start()
    {
        if (storedSeenCutscene.RuntimeValue == true)
        {
            gameObject.SetActive(false);
            return;
        }
        director = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if(!hasStartedPlaying)
        {
            return;
        }

        if(hasStartedPlaying && director.state != PlayState.Playing)
        {
            playerMovement.currentState = PlayerState.idle;
            Destroy(this.gameObject);
        } else if (hasStartedPlaying && director.state == PlayState.Playing)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(director == null)
        {
            return;
        }

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.currentState = PlayerState.interact;
            playerMovement.myAnimator.SetBool("isWalking", false);

            director.Play();
            hasStartedPlaying = true;
            storedSeenCutscene.RuntimeValue = true;
        }
    }

    public void PauseTimeLine()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
