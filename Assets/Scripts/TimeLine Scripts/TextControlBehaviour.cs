using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[Serializable]
public class TextControlBehaviour : PlayableBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private SignalSender dialogueSignal;

    private int framesActive = 0;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var textElement = playerData as Text;

        if(textElement == null)
        {
            return;
        }
    
        textElement.text = text;

        framesActive++;
        // make sure the element has come into view
        if (framesActive == 5)
        {
            dialogueSignal.Raise();
        }
    }
}
