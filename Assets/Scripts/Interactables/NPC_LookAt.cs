using UnityEngine;

public class NPC_LookAt : MonoBehaviour
{
    public GameObject _target;

    private void Update()
    {
        UpdateLook();
    }

    private void UpdateLook()
    {
        if (_target == null) return;

        transform.LookAt(_target.transform);
    }
}
