using System.Collections;
using Cattac.Character;
using FeedbacksEditor;
using Interactables.Collectibles;
using UnityEngine;

namespace Cattac.Interactables.Collectibles
{
    public class CheeseCollectible : MagneticCollectible<CharacterHead>
    {
        public System.Action OnPickUpEvent;
        
        [Header("Coin")]
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _uiWinDelay;
        [SerializeField] private GameObject _children;
        [SerializeField] private GameEvent _feedback;
        [SerializeField] private bool _addToCount = true;

        private bool _isTriggered;

        protected override void OnPickUp()
        {
            if (_owner == null || _isTriggered) return;

            _isTriggered = true;
            GameEventsManager.PlayEvent(_feedback, gameObject);
            OnPickUpEvent?.Invoke();
            
            if (_addToCount) MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager.AddCheese(_owner.IsLeftHead);
            StartCoroutine(AddCoinToCount());
        }

        private void Update()
        {
            transform.eulerAngles += _turnSpeed * Time.deltaTime * Vector3.up;
            LerpTowardsPlayer();
        }


        private IEnumerator AddCoinToCount()
        {
            yield return new WaitForSeconds(_uiWinDelay);
            Destroy(gameObject);
        }
    }
}