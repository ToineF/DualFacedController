using UnityEngine;

    public class EarthPillarMaker : Ability
    {
        [SerializeField] private Animator _pillarPrefab;
        [SerializeField] private string _pillarCondition;
        [SerializeField] private int _maxEarthPillarCount = 3;
        [SerializeField] private int _earthPillarPoolCount = 5;
        [SerializeField] private float _distanceFromUser = 1f;
        [SerializeField] private LayerMask _groundLayer;

        private Animator[] _earthPillars;
        private int _currentEarthPillarIndex;
        private int _currentPillars = 0;
        private void Start()
        {
            _earthPillars = new Animator[_earthPillarPoolCount];
            for (int i = 0; i < _earthPillarPoolCount; i++)
            {
                var pillar = Instantiate(_pillarPrefab, null);
                pillar.gameObject.SetActive(false);
                _earthPillars[i] = pillar;
            }
        }

        public override void UseAbility(Head user)
        {
            var pillar = _earthPillars[_currentEarthPillarIndex];
            pillar.gameObject.SetActive(true);
            var lastDirection = user.LastDirection.normalized;
            pillar.transform.position = user.transform.position + new Vector3(lastDirection.x, 0, lastDirection.y) * _distanceFromUser;
            Physics.Raycast(pillar.transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer);
            if (hit.collider != null) pillar.transform.position = hit.point;
            pillar.SetBool(_pillarCondition, true);
            
            _currentPillars++;
            if (_currentPillars > _maxEarthPillarCount)
            {
                _currentPillars--;
                _earthPillars[(_currentEarthPillarIndex-_maxEarthPillarCount+_earthPillarPoolCount)%_earthPillarPoolCount].SetBool(_pillarCondition, false);
            }
            
            _currentEarthPillarIndex++;
            _currentEarthPillarIndex %= _earthPillarPoolCount;
        }
    }