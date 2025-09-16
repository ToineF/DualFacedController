using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Character.Credits
{
    public class CreditsTextDoScale : MonoBehaviour
    {
        [SerializeField] private float _maxTime;
        
        [Header("Tweens")]
        [SerializeField] private float _writeTimeBetweenChars = 0.15f;
        [SerializeField] private float _writeApparitionDuration = 0.5f;
        [SerializeField] private Ease _writeEase = Ease.OutBack;
        [SerializeField] private float _eraseTimeBetweenChars = 0.04f;
        [SerializeField] private float _eraseDisparitionDuration = 0.5f;
        [SerializeField] private Ease _eraseEase = Ease.OutBack;
        
        private void Start()
        {
            StartCoroutine(Spawn());
            StartCoroutine(Erase());
        }

        private IEnumerator Spawn()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = Vector3.zero;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).DOScale(Vector3.one, _writeApparitionDuration).SetEase(_writeEase);
                yield return new WaitForSeconds(_writeTimeBetweenChars);
            }
        }
        
        private IEnumerator Erase()
        {
            yield return new WaitForSeconds(_maxTime);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).DOScale(Vector3.zero, _eraseDisparitionDuration).SetEase(_eraseEase);
                yield return new WaitForSeconds(_eraseTimeBetweenChars);
            }
        }
    }
}