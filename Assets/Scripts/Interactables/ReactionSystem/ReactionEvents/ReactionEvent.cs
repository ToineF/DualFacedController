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

    public virtual void OnReactionEnd(GameObject self, GameObject target)
    {
    }
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
    [SerializeField] private bool _useLookAt = false;
    [SerializeField] private float _lookAtLerp;

    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        if (_useLookAt)
        {
            var rotation = Quaternion.LookRotation(target.transform.position - self.transform.position);
            self.transform.rotation = Quaternion.Slerp(self.transform.rotation, rotation, Time.deltaTime * _lookAtLerp);
            yield return null;
        }
        else
        {
            var oldLookAt = self.transform.position + self.transform.forward;
            float startTurnTime = Random.Range(StartTurnTime.x, StartTurnTime.y);
            float waitTime = Random.Range(WaitTime.x, WaitTime.y);
            float endTurnTime = Random.Range(EndTurnTime.x, EndTurnTime.y);
            if (TurnBack == false) endTurnTime = 0;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(self.transform.DOLookAt(target.transform.position, startTurnTime, AxisConstraint,
                Vector3.up));
            sequence.AppendInterval(waitTime);
            if (TurnBack) sequence.Append(self.transform.DOLookAt(oldLookAt, endTurnTime, AxisConstraint, Vector3.up));
            sequence.Play();

            yield return new WaitForSeconds(startTurnTime + waitTime + endTurnTime);
        }
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

public class ReactionAnimation : ReactionEvent
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private bool _waitForCompletion = false;

    private AnimationClip _previousAnimation;

    public override IEnumerator Execute(GameObject self, GameObject target)
    {
        var clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
        if (_previousAnimation == null) _previousAnimation = clipInfo[0].clip;
        var animationLength = clipInfo.LongLength;
        _animator.Play(_animationClip.name);

        if (_waitForCompletion)
        {
            yield return new WaitForSeconds(animationLength);
            _animator.Play(_previousAnimation.name);
        }
    }

    public override void OnReactionEnd(GameObject self, GameObject target)
    {
        if (_waitForCompletion == false)
        {
            _animator.Play(_previousAnimation.name);
        }
    }
}

[System.Serializable]
public class ReactionEventWrapper
{
    [SerializeReference] public List<ReactionEvent> List;
}