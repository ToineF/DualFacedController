using System.Collections;
using Cattac.Character;
using FeedbacksEditor;
using MaskTransitions;
using UnityEngine;

namespace Cattac.Interactables.Death
{
    public class RespawnEvent : MonoBehaviour
    {
        [Header("Respawn Timings")]
        [SerializeField] private float _transitionTime = 1f;
        [SerializeField] private float _teleportatingDelay = .4f;
        [SerializeField] private float _startDelay = .4f;
        [SerializeField] private GameEvent _respawnFeedback;

        public void Respawn()
        {
            Vector3? position = CheckpointsManager.CurrentCheckpoint?.transform.position;
            var head = MainGame.Instance.PlayerController.GetComponentInChildren<CharacterHead>();
            if (head && position.HasValue) StartCoroutine(RespawnCoroutine(head, position.Value));
        }
        
        private IEnumerator RespawnCoroutine(CharacterHead characterHead, Vector3 position)
        {
            GameEventsManager.PlayEvent(_respawnFeedback, characterHead.gameObject);

            yield return new WaitForSeconds(_startDelay);
        
            TransitionManager.Instance.PlayTransition(_transitionTime);
        
            yield return new WaitForSeconds(_teleportatingDelay);

            characterHead.CharacterManager.TeleportPlayer(position, false);
        
        }
    }
}