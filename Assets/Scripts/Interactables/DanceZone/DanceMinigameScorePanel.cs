using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables
{
    public class DanceMinigameScorePanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MeshRenderer[] _winHighlights;
        [SerializeField] private MeshRenderer[] _loseHighlights;
        [SerializeField] private Material _winHighlightMaterial;

        [Header("Transition")]
        [SerializeField] private Transform _upPosition;
        [SerializeField] private Transform _downPosition;
        [SerializeField] private float _transitionTime;

        public void Appear(bool appear)
        {
            var initialValue = appear ? _upPosition.position.y : _downPosition.position.y;
            transform.position = new Vector3(transform.position.x, initialValue, transform.position.z);
            var endValue = appear ? _downPosition.position.y : _upPosition.position.y;
            transform.DOMoveY(endValue, _transitionTime);
        }

        public void SetHighlight(bool win, int index)
        {
            if (win) _winHighlights[index].material = _winHighlightMaterial;
            else _loseHighlights[index].material = _winHighlightMaterial;
        }
    }
}