using UnityEngine;

namespace Cattac.Character.Visuals
{
    public class BodyRenderer : MonoBehaviour
    {
        [field: SerializeField] public Transform[] OriginalPoints { get; set; }
        [field: SerializeField] public GameObject[] Bones { get; set; }
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private void Update()
        {
            for (int i = 0; i < Bones.Length; i++)
            {
                    var boneTransform = Bones[i].transform;
                    boneTransform.position = OriginalPoints[i].position + _positionOffset;
                if (i + 1 >= Bones.Length)
                {
                    boneTransform.rotation = Bones[i-1].transform.rotation;

                }
                else
                {
                    boneTransform.LookAt(OriginalPoints[i+1].position + _positionOffset);
                    boneTransform.Rotate(_rotationOffset);
                }

            }
        }
    }
}