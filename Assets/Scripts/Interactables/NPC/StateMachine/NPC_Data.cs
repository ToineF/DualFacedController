using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables.NPC
{
    [CreateAssetMenu(menuName = "Cattac/NPC_Data")]
    public class NPC_Data : ScriptableObject
    {
        [field: SerializeField] public float ChaseMouseDuration { get; private set; } = 1f;
        [field: SerializeField] public Ease ChaseMouseEase { get; private set; } = Ease.Linear;
    }
}