using FMODUnity;
using UnityEngine;

public class SFXFMODPlayer : MonoBehaviour
{
    [SerializeField] EventReference _eventPath;
    [SerializeField] bool _playOnAwake;
        
    private void Start()
    {
        if (_playOnAwake) Play();
    }

    public void Play() => FMODAudioManager.Instance.PlayClip(_eventPath, transform.position);
}