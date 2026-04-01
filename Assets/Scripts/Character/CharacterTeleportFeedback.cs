using System.Collections;
using UnityEngine;

namespace Cattac.Character
{
    public class CharacterTeleportFeedback : MonoBehaviour
    {
        [SerializeField] private CharacterHead _head;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private float _deactivationTime = 0.3f;

        private void OnEnable()
        {
            _head.CharacterManager.OnTeleport += OnTeleport;
        }

        private void OnDisable()
        {
            _head.CharacterManager.OnTeleport -= OnTeleport;
        }
        private void OnTeleport()
        {
            StartCoroutine(TeleportWait());
        }

        private IEnumerator TeleportWait()
        {
            _trail.enabled = false;
            yield return new WaitForSeconds(_deactivationTime);
            _trail.Clear();
            _trail.enabled = true;
        }
    }
}
