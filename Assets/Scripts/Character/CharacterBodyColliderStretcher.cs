using UnityEngine;

namespace Cattac.Character
{
    public class CharacterBodyColliderStretcher : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider[] _bodyParts;
        [SerializeField] private float _minHeight = 2;
        [SerializeField] private float _maxHeight = 4;
        [SerializeField] private float _minDistance = 0;
        [SerializeField] private float _maxDistance = 10;

        private void Update()
        {
            for (int i = 0; i < _bodyParts.Length; i++)
            {
                UpdateDistance(i);
            }
        }

        private void UpdateDistance(int index)
        {
            var currentBodyPart = _bodyParts[index];

            var previousBodyPart = index > 0 ? _bodyParts[index - 1] : null;
            var nextBodyPart = (index < _bodyParts.Length - 1) ? _bodyParts[index + 1] :  null;
            
            var previousDistance = Mathf.Infinity;
            var nextDistance = Mathf.Infinity;
            if (previousBodyPart != null) previousDistance = Vector3.Distance(previousBodyPart.transform.position, currentBodyPart.transform.position);
            if (nextBodyPart != null) nextDistance = Vector3.Distance(nextBodyPart.transform.position, currentBodyPart.transform.position);
            
            var targetDistance = Mathf.Min(previousDistance, nextDistance);
            
            currentBodyPart.height = Mathf.Lerp(_minHeight, _maxHeight, Mathf.Clamp01((targetDistance-_minDistance)/(_maxDistance-_minDistance)));
        }
    }
}