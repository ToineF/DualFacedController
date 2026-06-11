using Cattac.Interactables;
using UnityEngine;

namespace Cattac.Collectibles.Save
{
    public class BallDropZoneDataPersistence : DataPersistence
    {
        [SerializeField] private BallDropZone _dropZone;
        [SerializeField] private Transform _ball;
        [SerializeField] private Door _doorToOpen;

        protected override void StartInternal()
        {
            _dropZone.OnConditionMet.AddListener(Save);
        }

        private void OnDestroy()
        {
            _dropZone.OnConditionMet.RemoveListener(Save);
        }

        protected override void Load()
        {
            if (GetID() >= 1) // True
            {
                _dropZone.BallEnter(false);
                var targetPosition = _dropZone.transform.position;
                targetPosition.y = _ball.position.y;
                _ball.position = targetPosition;
                _doorToOpen.OpenNoFeedback();
            }
        }
    }
}