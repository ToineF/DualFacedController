using UnityEngine;
using DG.Tweening;

public class DoTweenComponent : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    public void DoScaleY(float endValue) => transform.DOScaleY(endValue, _duration).SetEase(_ease);
}
