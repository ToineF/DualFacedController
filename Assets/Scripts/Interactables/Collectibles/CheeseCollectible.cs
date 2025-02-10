using System.Collections;
using Cattac.Character;
using Interactables.Collectibles;
using UnityEngine;

namespace Cattac.Interactables.Collectibles
{
    public class CheeseCollectible : MagneticCollectible<CharacterHead>
    {
        [Header("Coin")]
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _uiWinDelay;
        [SerializeField] private bool _muteAudioFeedback;
        [SerializeField] private GameObject _children;

        protected override void OnPickUp()
        {
            if (_owner == null) return;

            _children.SetActive(true);
            _children.transform.SetParent(transform.parent);
            //manager.Collectibles.AddCoinPreview();
            //.Feedbacks.PlayFeedback(manager.Data.FeedbacksData.CoinPreviewFeedback, transform.position, Quaternion.identity, null, _muteAudioFeedback);

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
            //_owner.Manager.Collectibles.AddCoin();
            //manager.Feedbacks.PlayFeedback(manager.Data.FeedbacksData.CoinFeedback, transform.position, Quaternion.identity, null, _muteAudioFeedback);
            Destroy(gameObject);
        }
    }
}