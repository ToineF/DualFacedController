using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables.NPC.Movement
{
    public class NPC_Points_Follow : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _moveDuration;
        [SerializeField] private Ease _ease;
        
        private int _currentIndex;

        private void Start()
        {
            transform.position = _points[_currentIndex].position;
            NextStep();
        }

        private void NextStep()
        {
            _currentIndex++;
            _currentIndex %= _points.Length;
            transform.DOMove(_points[_currentIndex].transform.position, _moveDuration).SetEase(_ease).OnComplete(NextStep);
            transform.GetChild(0).LookAt(_points[_currentIndex].position);
        }
    }
}