using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Character.Credits
{
    public class CreditsTextDoScale : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = Vector3.zero;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}