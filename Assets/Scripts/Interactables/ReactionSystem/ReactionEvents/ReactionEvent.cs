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
    [field: SerializeField] public AxisConstraint AxisConstraint { get; private set; } = AxisConstraint.Y;
    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        var oldLookAt = self.transform.position + self.transform.forward;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(self.transform.DOLookAt(target.transform.position, Random.Range(StartTurnTime.x, StartTurnTime.y), AxisConstraint, Vector3.up));
        sequence.AppendInterval(Random.Range(WaitTime.x, WaitTime.y));
        sequence.Append(self.transform.DOLookAt(oldLookAt, Random.Range(EndTurnTime.x, EndTurnTime.y), AxisConstraint, Vector3.up));
        sequence.Play();
        yield break;
    }
}

[System.Serializable]
public class ReactionEventWrapper
{
    [SerializeReference] public List<ReactionEvent> List;
}