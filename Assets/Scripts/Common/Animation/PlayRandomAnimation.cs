using AntoineFoucault.Utilities;
using UnityEngine;

public class PlayRandomAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip[] _clips;
    [SerializeField] private bool _playOnStart = false;

    private void Start()
    {
        if (_playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        _animator.Play(_clips.GetRandomItem().name);
    }
}