using DG.Tweening;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private GameObject _vfxPrefab;
    [SerializeField] private Vector3 _scaleAmount;
    [SerializeField] private float _scaleTime;
    [SerializeField] private Ease _easeType;
    
    public void Enter()
    {
        transform.DOKill();
        transform.DOPunchScale(_scaleAmount, _scaleTime).SetEase(_easeType);
        Instantiate(_vfxPrefab, transform);
    }
}