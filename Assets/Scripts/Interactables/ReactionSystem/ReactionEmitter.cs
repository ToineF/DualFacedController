using UnityEngine;
using UnityEngine.Events;

public class ReactionEmitter : MonoBehaviour
{
    public UnityEvent<GameObject, ReactionType> OnReaction { get; set; } = new();
    
    [SerializeField] private ReactionType _type;

    public void Emit()
    {
        OnReaction?.Invoke(gameObject, _type);
    }
}