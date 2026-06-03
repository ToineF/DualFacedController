using System;
using AntoineFoucault.Utilities;
using FeedbacksEditor;
using UnityEngine;

public class PlayRandomAnimationFeedback : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationFeedbackPair[] _pairs;

    public void Play()
    {
        var randomItem = _pairs.GetRandomItem();
        _animator.Play(randomItem.Clip.name);
        GameEventsManager.PlayEvent(randomItem.Feedback, _animator.gameObject);
    }

    [Serializable]
    private struct AnimationFeedbackPair
    {
        [field:SerializeField] public AnimationClip Clip { get; private set; }
        [field:SerializeField] public GameEvent Feedback { get; private set; }
    }
}