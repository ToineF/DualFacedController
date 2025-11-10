using System.Collections;
using Cattac.Character;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables
{
    /// <summary>
    /// Two Separators linked together, separating a CharacterHead from its body, and moving it from one separator to the other
    /// </summary>
    public class SeparatorSnake : MonoBehaviour
    {
        [SerializeField] private BoxTriggerUnityEventPlayer[] _mouths;
        [SerializeField] private GameObject[] _bodyParts;
        [SerializeField] private float _waitTimeByBodyParts;
        [SerializeField] private Vector3 _bodyPartScale;
        [SerializeField] private float _spitForce = 1;
        
        private void Awake()
        {
            for (int i = 0; i < _mouths.Length; i++)
            {
                int index = i;
                _mouths[index].OnTriggerEnter.AddListener(c => StartCoroutine(Enter(c, index)));
            }
        }

        private IEnumerator Enter(CharacterHead player, int mouthIndex)
        {
            int otherMouthIndex = (mouthIndex + 1) % _mouths.Length;
            
            // If not separated, separate
            if (player.IsSeparated == false)
            {
                player.TogetherRigidbody.isKinematic = true;
                player.SetSeparation(true);
            }
            
            // Disable current head
            _mouths[mouthIndex].gameObject.SetActive(false);
            
            // Disable player
            player.CurrentRigidbody.gameObject.SetActive(false);
            
            // Play body animation
            for (int i = 0; i < _bodyParts.Length; i++)
            {
                var part = mouthIndex == 1 ? _bodyParts[_bodyParts.Length - 1 - i] :  _bodyParts[i];
                part.transform.DOKill();
                part.transform.DOPunchScale(_bodyPartScale, _waitTimeByBodyParts);
                yield return new WaitForSeconds(_waitTimeByBodyParts);
            }
            
            // Teleport player
            player.CurrentRigidbody.transform.position = _mouths[otherMouthIndex].transform.position;
            
            // Enable player
            player.CurrentRigidbody.gameObject.SetActive(true);
            player.CurrentRigidbody.angularVelocity = Vector3.zero;
            player.CurrentRigidbody.AddForce(_mouths[otherMouthIndex].transform.forward * _spitForce);
            
            // Enable other head
            yield return  new WaitForSeconds(1.5f);
            _mouths[otherMouthIndex].gameObject.SetActive(true);
        }
    }
}