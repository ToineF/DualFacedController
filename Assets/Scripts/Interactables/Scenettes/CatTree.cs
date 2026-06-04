using UnityEngine;

public class CatTree : MonoBehaviour
{
    [SerializeField] private GameObject _catsParent;
    [SerializeField] private Animator _treeAnimator;
    [SerializeField] private Rigidbody[] _cats;
    [SerializeField] private float _randomForce;

    public void Fall()
    {
        if (_treeAnimator != null) _treeAnimator.enabled = true;
        _catsParent.SetActive(true);
        
        for (int i = 0; i < _cats.Length; i++)
        {
            _cats[i].isKinematic = false;
            _cats[i].AddForce(Random.insideUnitSphere * _randomForce, ForceMode.Impulse);
        }
    }
}