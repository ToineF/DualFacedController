using Cinemachine;
using UnityEngine;

public class SubMenuCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private SubMenu _subMenu;

    private void OnEnable()
    {
        _subMenu.OnOpen += OnOpen;
        _subMenu.OnClose += OnClose;
    }
    
    private void OnDisable()
    {
        _subMenu.OnOpen += OnOpen;
        _subMenu.OnClose += OnClose;
    }

    private void OnOpen()
    {
        Debug.Log("Open : " + _subMenu.name);
        _virtualCamera.gameObject.SetActive(true);
    }
    
    private void OnClose()
    {
        Debug.Log("Close : " + _subMenu.name);
        _virtualCamera.gameObject.SetActive(false);
    }
}