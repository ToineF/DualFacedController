using UnityEngine;
using UnityEngine.Audio;


public class SFXPlayer : MonoBehaviour
    {
        [SerializeField] AudioResource _clip;
        [SerializeField] bool _playOnAwake;
        
        private void Start()
        {
            if (_playOnAwake) Play();
        }

        public void Play() => AudioManager.Instance.PlayResource(_clip);
    }