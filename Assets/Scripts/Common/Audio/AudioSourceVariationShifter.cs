using UnityEngine;

public class AudioSourceVariationShifter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _audioSource.volume = Random.Range(0.7f, 1f);
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.Play();
    }
    
}
