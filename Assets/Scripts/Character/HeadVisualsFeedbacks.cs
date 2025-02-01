using UnityEngine;

namespace Cattac.Character.Visuals
{
    public class HeadVisualsFeedbacks : MonoBehaviour
    {
        [SerializeField] private CharacterHead _characterHead;
        [SerializeField] private ParticleSystem _glitterVFX;
        [SerializeField] private GameObject _fakeHead;
        [SerializeField] private Transform _headHat;
        [SerializeField] private Transform _separatedParent;
        [SerializeField] private Transform _connectedParent;

        private void Awake()
        {
            _characterHead.OnConnect += OnConnect;
            _characterHead.OnSeparate += OnSeparate;
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
}