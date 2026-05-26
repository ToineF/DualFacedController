using System.Collections;
using UnityEngine;

public class ReactionListener : MonoBehaviour
{
    [SerializeField] private ReactionType _type;
    [SerializeField] private GameObject _selfGameObject;
    [SerializeField] private bool _waitForCoroutineCompletion = false;

    [field: BF_SubclassList.SubclassList(typeof(ReactionEvent)), SerializeField]
    public ReactionEventWrapper ReactionEvents { get; private set; }

    private bool _isCoroutineRunning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ReactionEmitter emitter))
        {
            emitter.OnReaction.AddListener(OnEmission);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ReactionEmitter emitter))
        {
            emitter.OnReaction.RemoveListener(OnEmission);
        }
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
    }
}