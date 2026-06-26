using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionListener : MonoBehaviour
{
    [SerializeField] private ReactionType _type;
    [SerializeField] private GameObject _selfGameObject;
    [SerializeField] private bool _waitForCoroutineCompletion = false;

    [field: BF_SubclassList.SubclassList(typeof(ReactionEvent)), SerializeField]
    public ReactionEventWrapper ReactionEvents { get; private set; }

    private bool _isCoroutineRunning;
    private List<ReactionEmitter> _emitters = new();
    private static readonly Type ReactionEmitterType = typeof(ReactionEmitter);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(ReactionEmitterType, out Component component)
            && component is ReactionEmitter emitter)
        {
            emitter.OnReaction.AddListener(OnEmission);
            _emitters.Add(emitter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ReactionEmitter emitter))
        {
            emitter.OnReaction.RemoveListener(OnEmission);
            _emitters.Remove(emitter);
        }
    }

    private void OnDisable()
    {
        foreach (var emitter in _emitters)
        {
            emitter.OnReaction.RemoveListener(OnEmission);
        }
        _emitters.Clear();
    }

    private void OnEmission(GameObject target, ReactionType type)
    {
        if (type != _type) return;
        if (_waitForCoroutineCompletion && _isCoroutineRunning) return;
        
        StopAllCoroutines();
        StartCoroutine(Execute(target));
    }

    private IEnumerator Execute(GameObject target)
    {
        _isCoroutineRunning = true;

        foreach (var reactionEvent in ReactionEvents.List)
        {
            yield return reactionEvent.Execute(_selfGameObject, target);
        }

        _isCoroutineRunning = false;
        
        foreach (var reactionEvent in ReactionEvents.List)
        {
            reactionEvent.OnReactionEnd(_selfGameObject, target);
        }
    }
}