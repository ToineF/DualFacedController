using AntoineFoucault.Utilities;
using UnityEngine;

public class PlayRandomAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip[] _clips;

    public void Play()
    {
        _animator.Play(_clips.GetRandomItem().name);
    }
}