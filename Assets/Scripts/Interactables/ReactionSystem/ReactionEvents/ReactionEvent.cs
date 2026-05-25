using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// The parent class for all ReactionEvents.
/// </summary>
[System.Serializable]
public abstract class ReactionEvent
{
    public abstract IEnumerator Execute(GameObject self, GameObject target);
}

public class ReactionWaitForTime : ReactionEvent
{
    [field: SerializeField] public Vector2 WaitTime { get; private set; }
    
    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        yield return new WaitForSeconds(Random.Range(WaitTime.x, WaitTime.y));
    }
}

public class ReactionUnityEvent : ReactionEvent
 {
     [field: SerializeField] public UnityEvent Event { get; private set; }
     public override IEnumerator Execute(GameObject self, GameObject target)
     {
         Event?.Invoke();
         yield break;
     }
 }

public class ReactionLookAt : ReactionEvent
{
    [field: SerializeField] public Vector2 StartTurnTime { get; private set; }
    [field: SerializeField] public Vector2 WaitTime { get; private set; }
    [field: SerializeField] public Vector2 EndTurnTime { get; private set; }
    [field: SerializeField] public bool TurnBack { get; private set; } = true;
    [field: SerializeField] public AxisConstraint AxisConstraint { get; private set; } = AxisConstraint.Y;
    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        var oldLookAt = self.transform.position + self.transform.forward;
        float startTurnTime = Random.Range(StartTurnTime.x, StartTurnTime.y);
        float waitTime = Random.Range(WaitTime.x, WaitTime.y);
        float endTurnTime = Random.Range(EndTurnTime.x, EndTurnTime.y);
        if (TurnBack == false) endTurnTime = 0;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(self.transform.DOLookAt(target.transform.position, startTurnTime, AxisConstraint, Vector3.up));
        sequence.AppendInterval(waitTime);
        if (TurnBack) sequence.Append(self.transform.DOLookAt(oldLookAt,endTurnTime , AxisConstraint, Vector3.up));
        sequence.Play();
        
        yield return new WaitForSeconds(startTurnTime + waitTime + endTurnTime);
    }
}

public class ReactionFeedback : ReactionEvent
{
    [SerializeField] private FeedbacksEditor.GameEvent _gameEvent;
    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        FeedbacksEditor.GameEventsManager.PlayEvent(_gameEvent, self);
        yield break;
    }
}

[System.Serializable]
public class ReactionEventWrapper
{
    [SerializeReference] public List<ReactionEvent> List;
}