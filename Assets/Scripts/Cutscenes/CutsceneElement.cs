using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Cattac.Cutscenes
{
    [Serializable]
    public class CutsceneElement { }
    
    public class CutsceneWaitForTime : CutsceneElement
    {
        [field: SerializeField] public float WaitTime { get; private set; }
    }
    
    public class CutsceneEvent : CutsceneElement
    {
        [field: SerializeField] public UnityEvent Event { get; private set; }
    }

    public class CutsceneTimeline : CutsceneElement
    {
        [field: SerializeField] public PlayableDirector Director { get; private set; }
    }

    [Serializable]
    public class CutsceneElementWrapper
    {
        [SerializeReference] public List<CutsceneElement> List;
    }
}