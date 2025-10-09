using System.Collections;
using Cattac.Character;
using FeedbacksEditor;
using Interactables.Collectibles;
using UnityEngine;

namespace Cattac.Interactables.Collectibles
{
    public class CheeseCollectible : MagneticCollectible<CharacterHead>
    {
        [Header("Coin")]
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _uiWinDelay;
        [SerializeField] private GameObject _children;
        [SerializeField] private GameEvent _feedback;

        protected override void OnPickUp()
        {
            if (_owner == null) return;

            GameEventsManager.PlayEvent(_feedback, gameObject);
            MainGame.Instance.CollectiblesManager.CheeseCollectiblesManager.AddCheese();
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