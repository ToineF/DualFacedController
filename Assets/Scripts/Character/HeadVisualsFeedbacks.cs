using UnityEngine;


public class HeadVisualsFeedbacks : MonoBehaviour
{
    [SerializeField] private Head _head;
    [SerializeField] private ParticleSystem _glitterVFX;
    [SerializeField] private GameObject _fakeHead;
    [SerializeField] private Transform _headHat;
    [SerializeField] private Transform _separatedParent;
    [SerializeField] private Transform _connectedParent;

    private void Awake()
    {
        _head.OnConnect += OnConnect;
        _head.OnSeparate += OnSeparate;
    }

    private void OnConnect()
    {
        _glitterVFX.Play();
        _fakeHead.SetActive(false);
        _headHat.SetParent(_connectedParent);
        _headHat.localPosition = Vector3.zero;
        _headHat.localEulerAngles = Vector3.zero;
        _headHat.localScale = Vector3.one;
    }
    private void OnSeparate()
    {
        _glitterVFX.Stop();
        _fakeHead.SetActive(true);
        _headHat.SetParent(_separatedParent);
        _headHat.localPosition = Vector3.zero;
        _headHat.localEulerAngles = Vector3.zero;
        _headHat.localScale = Vector3.one;
    }
}