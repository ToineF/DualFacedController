using UnityEngine;

public class SoloMouseHole : MonoBehaviour
{
    [SerializeField] private GameObject _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterHead head) == false) return;
        
        _collider.SetActive(head.IsSeparated == false);
    }
}