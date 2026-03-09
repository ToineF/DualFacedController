using UnityEngine;

public class ZoomFadeManager : MonoBehaviour
{
    [SerializeField] private ZoomFadeImage[] _images;
    [SerializeField] private Camera _renderingCamera;

    private int _tweensCount = 0;

    private void Start()
    {
        OnTweenFinish();
    }

    public void Play()
    {
        _renderingCamera.gameObject.SetActive(true);
        _tweensCount = _images.Length;
        foreach (var image in _images)
        {
            image.Play();
            image.OnTweenFinish += OnTweenFinish;
        }
    }

    private void OnTweenFinish()
    {
        _tweensCount--;
        if (_tweensCount <= 0) _renderingCamera.gameObject.SetActive(false);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space)) Play();
    // }
}