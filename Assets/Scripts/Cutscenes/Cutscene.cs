using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Cutscenes
{
    public class Cutscene : MonoBehaviour
    {
        public System.Action OnEnd;
        public bool HasPlayed { get; set; }
        [field: BF_SubclassList.SubclassList(typeof(CutsceneElement)), SerializeField] public CutsceneElementWrapper SequenceElements { get; private set; }

        [field: SerializeField] public bool StopPlayerMovements { get; private set; }
        [field: SerializeField] public List<UnityEvent> OnSkipEvents { get; private set; }

        public void Play() => CutsceneManager.Instance.StartNewSequence(this);
    }
}