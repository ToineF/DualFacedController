using UnityEngine;
using DG.Tweening;


public class DoScaleComponent : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    public void DoScale(float endValue) => transform.DOScale(endValue, _duration).SetEase(_ease);
}