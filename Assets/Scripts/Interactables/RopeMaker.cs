using AntoineFoucault.Utilities;
using UnityEngine;

namespace Cattac.Interactables
{
    public class RopeMaker : MonoBehaviour
    {
        [SerializeField] private int _pointsAmount;
        [SerializeField] private Vector3 _pointsOffset;
        [SerializeField] private Joint _jointReference;

        [ContextMenu("Make Rope")]
        public void MakeRope()
        {
            transform.ClearImmediate();

            var direction = Quaternion.LookRotation(_pointsOffset);
            Rigidbody lastRb = null;
            for (int i = 0; i < _pointsAmount + 2; i++)
            {
                Joint joint = Instantiate(_jointReference, transform.position + _pointsOffset * i, direction,
                    transform);

                if (lastRb != null) joint.connectedBody = lastRb;

                lastRb = joint.GetComponent<Rigidbody>();
                if (i == 0 || i == _pointsAmount + 1)
                {
                    lastRb.isKinematic = true;
                    if (lastRb.TryGetComponent(out MeshRenderer meshRenderer)) DestroyImmediate(meshRenderer);
                    if (lastRb.TryGetComponent(out MeshFilter meshFilter)) DestroyImmediate(meshFilter);
                    if (lastRb.TryGetComponent(out Collider collider)) DestroyImmediate(collider);
                }
            }
        }
    }
}