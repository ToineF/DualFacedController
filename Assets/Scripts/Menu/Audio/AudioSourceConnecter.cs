using UnityEngine;


/// <summary>
/// Links a lone AudioSource to the VolumeManager 
/// </summary>
public class AudioSourceConnecter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        AudioManager.Instance.VolumeManager.AddSFXSource(_audioSource);
    }
}