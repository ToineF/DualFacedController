using UnityEngine;


public class Eatable : MonoBehaviour, IGrabbable
{
    [SerializeField] private GameObject _children;
    [SerializeField] private GameObject _objectToDestroy;

    public void OnGrab(Head head)
    {
        head.CurrentGrabbable = null;
        _children.SetActive(true);
        _children.transform.SetParent(_objectToDestroy.transform.parent);
        Destroy(_objectToDestroy);
    }

    public void OnUngrab(Head head)
    {
    }

    public void AddForce(Vector3 force)
    {
    }
}