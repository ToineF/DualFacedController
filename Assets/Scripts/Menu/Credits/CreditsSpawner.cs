using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Cattac.Character.Credits
{
    public class CreditsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _players;
        [SerializeField] private float _waitTime = 6f;
        [SerializeField] private Transform _middlePoint;
        [SerializeField] private Transform[] _spawnParents;
        [SerializeField] private GameObject[] _credits;
        [SerializeField] private UnityEvent _onCreditsEnd;
        [SerializeField] private UnityEvent[] _onChildAppear;

        private int _currentCreditsIndex;

        private void Start()
        {
            StartCoroutine(Spawn());
        }


        private Transform GetSpawnParent()
        {
            var position = Vector3.zero;
            foreach (var player in _players)
            {
                position += player.transform.position;
            }

            position /= _players.Length;

            int spawnUp = (position.z > _middlePoint.position.z) ? 0 : 1;
            return _spawnParents[spawnUp];
        }

        private IEnumerator Spawn()
        {
            for (int j = 0; j < _credits.Length; j++)
            {
                var credit = _credits[j];
                credit.gameObject.SetActive(true);
                //credit.transform.SetParent(GetSpawnParent());
                credit.transform.localPosition = GetSpawnParent().position;
                for (var i = 0; i < credit.transform.childCount; i++)
                {
                    credit.transform.GetChild(i).GetComponent<CreditsTextDoScale>().Initialize();
                }

                _onChildAppear[j]?.Invoke();

                yield return new WaitForSeconds(_waitTime);
            }

            _onCreditsEnd?.Invoke();
        }
    }
}