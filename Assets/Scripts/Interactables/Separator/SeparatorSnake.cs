using System;
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
        public Action<GameObject> OnMouthSpit;
        
        [SerializeField] private BoxTriggerUnityEventPlayer[] _mouths;
        [SerializeField] private float _spitForce = 1;
        [SerializeField] private float _enabledWaitTime = 1f;
        
        [Header("Body Scale")]
        [SerializeField] private GameObject[] _bodyParts;
        [SerializeField] private float _waitTimeByBodyParts;
        [SerializeField] private float _bodyScaleTime;
        [SerializeField] private Vector3 _bodyScaleForce;
        [SerializeField] private Ease _bodyScaleEase;

        private int _bodyIndex = -1;
        
        private void Awake()
        {
            for (int i = 0; i < _mouths.Length; i++)
            {
                int index = i;
                _mouths[index].OnTriggerEnter.AddListener(c => StartCoroutine(Enter(c, index)));
                _mouths[index].gameObject.SetActive(index <= 0);
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
                _bodyIndex = otherMouthIndex;
                
                // Teleport Rigidbody
                player.TogetherRigidbody.transform.position = _mouths[mouthIndex].transform.position;
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
                part.transform.DOPunchScale(_bodyScaleForce, _bodyScaleTime).SetEase(_bodyScaleEase);
                player.CurrentRigidbody.transform.DOMove(part.transform.position, _waitTimeByBodyParts); // Move player along the body to ensure the camera follows
                yield return new WaitForSeconds(_waitTimeByBodyParts);
            }
            
            // Play event for mouth-spit
            OnMouthSpit?.Invoke(_mouths[otherMouthIndex].gameObject);
            
            // Teleport player
            player.CurrentRigidbody.transform.position = _mouths[otherMouthIndex].transform.position;
            
            // If separate and body, re-attach
            if (player.IsSeparated && _bodyIndex == mouthIndex)
            {
                player.TogetherRigidbody.isKinematic = false;
                player.SetSeparation(false);
                _bodyIndex = -1;
            }
            
            // Enable player
            player.CurrentRigidbody.gameObject.SetActive(true);
            player.CurrentRigidbody.angularVelocity = Vector3.zero;
            player.CurrentRigidbody.AddForce(_mouths[otherMouthIndex].transform.right * _spitForce, ForceMode.Impulse);
            
            // Enable other head
            yield return  new WaitForSeconds(_enabledWaitTime);
            _mouths[otherMouthIndex].gameObject.SetActive(true);
        }
    }
}