using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TextControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private TextControlBehaviour template = new TextControlBehaviour();

    public ClipCaps clipCaps
    {
        get
        {
            return ClipCaps.None;
        }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<TextControlBehaviour>.Create(graph, template);
    }
}
