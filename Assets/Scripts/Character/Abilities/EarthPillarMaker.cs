using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Cattac.Character.Ability
{
    public class EarthPillarMaker : Ability
    {
        [SerializeField] private GameObject _pillarPrefab;
        [SerializeField] private string _pillarCondition;
        [SerializeField] private int _maxEarthPillarCount = 3;
        [SerializeField] private int _earthPillarPoolCount = 5;
        [SerializeField] private float _distanceFromUser = 1f;
        [SerializeField] private float _sphereCastRadius = 3f;
        [SerializeField] private float _upRaycastOffset;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _objectsLayer;

        [Header("Pillar Anim")] [SerializeField]
        private float _pillarInY = -4.1f;

        [SerializeField] private float _pillarInAnimTime = 1f;
        [SerializeField] private Ease _pillarInAnimEase;
        [SerializeField] private float _pillarOutY = 3.41f;
        [SerializeField] private float _pillarOutAnimTime = 1f;
        [SerializeField] private Ease _pillarOutAnimEase;

        private GameObject[] _earthPillars;
        private int _currentEarthPillarIndex;
        private int _currentPillars = 0;

        private void Start()
        {
            _earthPillars = new GameObject[_earthPillarPoolCount];
            for (int i = 0; i < _earthPillarPoolCount; i++)
            {
                var pillar = Instantiate(_pillarPrefab, null);
                pillar.gameObject.SetActive(false);
                _earthPillars[i] = pillar;
            }
        }

        public override void UseAbility(CharacterHead user)
        {
            var pillar = _earthPillars[_currentEarthPillarIndex];
            pillar.gameObject.SetActive(true);
            var lastDirection = user.LastNormalizedDirection.normalized;
            var targetPosition = user.transform.position +
                                 new Vector3(lastDirection.x, 0, lastDirection.y) * _distanceFromUser +
                                 Vector3.up * _upRaycastOffset;

            //Physics.SphereCast(targetPosition, _sphereCastRadius, Vector3.down, out RaycastHit objectHit, Mathf.Infinity, _objectsLayer);
            //if (objectHit.collider == null || objectHit.collider.gameObject.GetComponent<Rigidbody>() == null) return;
            //objectHit.collider.gameObject.SetActive(false);
            Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer);
            //objectHit.collider.gameObject.SetActive(true);
            if (hit.collider != null) pillar.transform.position = hit.point;
            var rb = pillar.GetComponent<Rigidbody>();
            if (rb == null) return;
            Debug.Log(hit.collider.gameObject);
            //objectHit.transform.DOMoveY(_pillarOutY, _pillarOutAnimTime).SetEase(_pillarOutAnimEase);
            if (hit.collider.GetComponent<EarthPillar>())
            {
                pillar.SetActive(false);
                hit.collider.gameObject.transform.DOMoveY(hit.point.y + _pillarInY, _pillarInAnimTime)
                    .SetEase(_pillarInAnimEase).OnUpdate(() => rb.MovePosition(rb.gameObject.transform.position))
                    .OnComplete(() => hit.collider.gameObject.SetActive(false));
                _currentPillars--;
            }
            else
            {
                pillar.transform.position = new Vector3(targetPosition.x, _pillarInY, targetPosition.z);
                pillar.transform.DOMoveY(hit.point.y + _pillarOutY, _pillarOutAnimTime).SetEase(_pillarOutAnimEase)
                    .OnUpdate(() => rb.MovePosition(pillar.transform.position));
                _currentPillars++;
            }

            if (_currentPillars > _maxEarthPillarCount)
            {
                _currentPillars--;
                var currentPillar =
                    _earthPillars[
                        (_currentEarthPillarIndex - _maxEarthPillarCount + _earthPillarPoolCount) %
                        _earthPillarPoolCount];
                currentPillar.transform.DOMoveY(hit.point.y + _pillarInY, _pillarInAnimTime).SetEase(_pillarInAnimEase)
                    .OnUpdate(() => rb.MovePosition(rb.gameObject.transform.position))
                    .OnComplete(() => currentPillar.SetActive(false));
            }

            _currentEarthPillarIndex++;
            _currentEarthPillarIndex %= _earthPillarPoolCount;
        }
    }
}